using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dragon.Core.WebApi.Filter;
using Dragon.Core.Common;
using Dragon.Core.Data;
using Dragon.Core.Extensions;
using Dragon.Core.Extensions.ServiceExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;
using System.Text;

namespace Dragon.Core.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
           .Enrich.FromLogContext()
           .WriteTo.Console()
           .CreateBootstrapLogger();

            try
            {
                Log.Information("Starting web host");

                var builder = WebApplication.CreateBuilder(args);
                

                //配置host与容器
                builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutofacModuleRegister());
                    builder.RegisterModule<AutofacPropertityModuleReg>();
                }).UseSerilog((context, services, configuration) =>
                {
                    configuration.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(services) .Enrich.FromLogContext().WriteTo.Console();
                }).ConfigureLogging((hostingContext, builder) =>
                {
                    // 1.过滤掉系统默认的一些日志
                    builder.AddFilter("System", LogLevel.Error);
                    builder.AddFilter("Microsoft", LogLevel.Error);

                    // 2.也可以在appsettings.json中配置，LogLevel节点

                    // 3.统一设置
                    builder.SetMinimumLevel(LogLevel.Error);

                });//.ConfigureAppConfiguration((hostingContext, config) =>
                   //{
                   //    config.Sources.Clear();
                   //    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
                   //    config.AddConfigurationApollo("appsettings.apollo.json");
                   //});
                // Add services to the container.
                builder.Services.AddSingleton(new Appsettings(builder.Configuration));
                builder.Services.AddMemoryCacheSetup();//注入缓存服务
                builder.Services.AddRedisCacgeSetup(); //添加redis服务
                builder.Services.AddAutoMapperSetup(); //注入自动映射服务
                builder.Services.AddCorsSetup();  //注入跨域服务
                builder.Services.AddEfCoreSetup();//注入ef链接服务
                builder.Services.AddDbSetup(); //注入db服务
                builder.Services.AddHttpContextSetup(); //注入 HttpContext 相关服务
                builder.Services.AddAuthorizationSetup();//注入权限服务
                builder.Services.AddAuthenticationSetup();//注入认证服务
                builder.Services.AddSignalR().AddNewtonsoftJsonProtocol();//添加signalR服务，使用json
                builder.Services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                        .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);//设置同步读取方式，读取body内容。默认是异步

                builder.Services.AddDistributedMemoryCache(); //注入分布式内存缓存
                builder.Services.AddSession();
                builder.Services.AddIpPolicyRateLimitSetup(builder.Configuration);//添加限流服务
                
                builder.Services.AddControllers(o =>
                {
                    o.Filters.Add(typeof(ResultFilterAttribute));
                    o.Filters.Add(typeof(TransactionScopeFilter));//事务过滤器
                                                                  // 全局异常过滤
                    o.Filters.Add(typeof(GlobalExceptionsFilter));
                    // 全局路由权限公约
                    //o.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());
                    // 全局路由前缀，统一修改路由
                    o.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
                })// 这种写法也可以
                  //.AddJsonOptions(options =>
                  //{
                  //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                  //})
                  //MVC全局配置Json序列化处理
                .AddNewtonsoftJson(options =>
                {
                    //忽略循环引用
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //不使用驼峰样式的key
                    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //设置时间格式
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    //忽略Model中为null的属性
                    //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    //设置本地时间而非UTC时间
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                });
                builder.Services.AddCustomModelStateSetup();
                builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());//使控制器可以采用属性注入
                //支持编码大全 例如:支持 System.Text.Encoding.GetEncoding("GB2312")  System.Text.Encoding.GetEncoding("GB18030") 
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerSetup();

                var app = builder.Build();
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    // 在开发环境中，使用异常页面，这样可以暴露错误堆栈信息，所以不要放在生产环境。net6默认已经配置好了
                    //app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        string apiName = Appsettings.app(new string[] { "Startup", "ApiName" });
                        c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{apiName} V1");
                        //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉，如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
                        c.RoutePrefix = ""; //默认会添加swagger路由前缀，这里去掉的话，launchSetting.json里面的launchUrl也要去掉
                    });
                }
                else
                {
                    app.UseExceptionHandler("/Error");
                    // 在非开发环境中，使用HTTP严格安全传输(or HSTS) 对于保护web安全是非常重要的。
                    // 强制实施 HTTPS 在 ASP.NET Core，配合 app.UseHttpsRedirection
                    //app.UseHsts();
                }
                

                // Ip限流,尽量放管道外层
                app.UseIpLimitMildd();
                // 记录请求与返回数据 
                app.UseReuestResponseLog();
                // 用户访问记录(必须放到外层，不然如果遇到异常，会报错，因为不能返回流)
                app.UseRecordAccessLogsMildd();
                // 记录ip请求
                app.UseIPLogMildd();
                app.UseSession();

                // ↓↓↓↓↓↓ 注意下边这些中间件的顺序，很重要 ↓↓↓↓↓↓
                // CORS跨域
                app.UseCors(Appsettings.app(new string[] { "Startup", "Cors", "PolicyName" }));
                // 跳转https
                //app.UseHttpsRedirection();
                // 使用静态文件
                app.UseStaticFiles();
                // 使用cookie
                app.UseCookiePolicy();
                // 返回错误码
                app.UseStatusCodePages();
                // Routing  net6框架默认已经配置好了路由
                //app.UseRouting();

                // 先开启认证
                app.UseAuthentication();
                //鉴权管道
                app.UseAuthorization();
                
                //路由终结点
                app.MapControllers();

                var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<EfDbContext>();
                //var tasksQzServices = scope.ServiceProvider.GetRequiredService<ITasksQzServices>();
                //var schedulerCenter = scope.ServiceProvider.GetRequiredService<ISchedulerCenter>();
                //var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
                // 生成种子数据
                app.UseSeedDataMildd(builder.Environment.WebRootPath, dbContext);
                // 开启QuartzNetJob调度服务
                //app.UseQuartzJobMildd(tasksQzServices, schedulerCenter);
                // 服务注册
                //app.UseConsulMildd(Configuration, lifetime);
                // 事件总线，订阅服务
                //app.ConfigureEventBus();
                
                app.Run();//运行
                
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}