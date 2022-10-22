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
                

                //����host������
                builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutofacModuleRegister());
                    builder.RegisterModule<AutofacPropertityModuleReg>();
                }).UseSerilog((context, services, configuration) =>
                {
                    configuration.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(services) .Enrich.FromLogContext().WriteTo.Console();
                }).ConfigureLogging((hostingContext, builder) =>
                {
                    // 1.���˵�ϵͳĬ�ϵ�һЩ��־
                    builder.AddFilter("System", LogLevel.Error);
                    builder.AddFilter("Microsoft", LogLevel.Error);

                    // 2.Ҳ������appsettings.json�����ã�LogLevel�ڵ�

                    // 3.ͳһ����
                    builder.SetMinimumLevel(LogLevel.Error);

                });//.ConfigureAppConfiguration((hostingContext, config) =>
                   //{
                   //    config.Sources.Clear();
                   //    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
                   //    config.AddConfigurationApollo("appsettings.apollo.json");
                   //});
                // Add services to the container.
                builder.Services.AddSingleton(new Appsettings(builder.Configuration));
                builder.Services.AddMemoryCacheSetup();//ע�뻺�����
                builder.Services.AddRedisCacgeSetup(); //���redis����
                builder.Services.AddAutoMapperSetup(); //ע���Զ�ӳ�����
                builder.Services.AddCorsSetup();  //ע��������
                builder.Services.AddEfCoreSetup();//ע��ef���ӷ���
                builder.Services.AddDbSetup(); //ע��db����
                builder.Services.AddHttpContextSetup(); //ע�� HttpContext ��ط���
                builder.Services.AddAuthorizationSetup();//ע��Ȩ�޷���
                builder.Services.AddAuthenticationSetup();//ע����֤����
                builder.Services.AddSignalR().AddNewtonsoftJsonProtocol();//���signalR����ʹ��json
                builder.Services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                        .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);//����ͬ����ȡ��ʽ����ȡbody���ݡ�Ĭ�����첽

                builder.Services.AddDistributedMemoryCache(); //ע��ֲ�ʽ�ڴ滺��
                builder.Services.AddSession();
                builder.Services.AddIpPolicyRateLimitSetup(builder.Configuration);//�����������
                
                builder.Services.AddControllers(o =>
                {
                    o.Filters.Add(typeof(ResultFilterAttribute));
                    o.Filters.Add(typeof(TransactionScopeFilter));//���������
                                                                  // ȫ���쳣����
                    o.Filters.Add(typeof(GlobalExceptionsFilter));
                    // ȫ��·��Ȩ�޹�Լ
                    //o.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());
                    // ȫ��·��ǰ׺��ͳһ�޸�·��
                    o.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
                })// ����д��Ҳ����
                  //.AddJsonOptions(options =>
                  //{
                  //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                  //})
                  //MVCȫ������Json���л�����
                .AddNewtonsoftJson(options =>
                {
                    //����ѭ������
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //��ʹ���շ���ʽ��key
                    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //����ʱ���ʽ
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    //����Model��Ϊnull������
                    //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    //���ñ���ʱ�����UTCʱ��
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                });
                builder.Services.AddCustomModelStateSetup();
                builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());//ʹ���������Բ�������ע��
                //֧�ֱ����ȫ ����:֧�� System.Text.Encoding.GetEncoding("GB2312")  System.Text.Encoding.GetEncoding("GB18030") 
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerSetup();

                var app = builder.Build();
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    // �ڿ��������У�ʹ���쳣ҳ�棬�������Ա�¶�����ջ��Ϣ�����Բ�Ҫ��������������net6Ĭ���Ѿ����ú���
                    //app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        string apiName = Appsettings.app(new string[] { "Startup", "ApiName" });
                        c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{apiName} V1");
                        //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ����������뻻һ��·����ֱ��д���ּ��ɣ�����ֱ��дc.RoutePrefix = "doc";
                        c.RoutePrefix = ""; //Ĭ�ϻ����swagger·��ǰ׺������ȥ���Ļ���launchSetting.json�����launchUrlҲҪȥ��
                    });
                }
                else
                {
                    app.UseExceptionHandler("/Error");
                    // �ڷǿ��������У�ʹ��HTTP�ϸ�ȫ����(or HSTS) ���ڱ���web��ȫ�Ƿǳ���Ҫ�ġ�
                    // ǿ��ʵʩ HTTPS �� ASP.NET Core����� app.UseHttpsRedirection
                    //app.UseHsts();
                }
                

                // Ip����,�����Źܵ����
                app.UseIpLimitMildd();
                // ��¼�����뷵������ 
                app.UseReuestResponseLog();
                // �û����ʼ�¼(����ŵ���㣬��Ȼ��������쳣���ᱨ����Ϊ���ܷ�����)
                app.UseRecordAccessLogsMildd();
                // ��¼ip����
                app.UseIPLogMildd();
                app.UseSession();

                // ������������ ע���±���Щ�м����˳�򣬺���Ҫ ������������
                // CORS����
                app.UseCors(Appsettings.app(new string[] { "Startup", "Cors", "PolicyName" }));
                // ��תhttps
                //app.UseHttpsRedirection();
                // ʹ�þ�̬�ļ�
                app.UseStaticFiles();
                // ʹ��cookie
                app.UseCookiePolicy();
                // ���ش�����
                app.UseStatusCodePages();
                // Routing  net6���Ĭ���Ѿ����ú���·��
                //app.UseRouting();

                // �ȿ�����֤
                app.UseAuthentication();
                //��Ȩ�ܵ�
                app.UseAuthorization();
                
                //·���ս��
                app.MapControllers();

                var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<EfDbContext>();
                //var tasksQzServices = scope.ServiceProvider.GetRequiredService<ITasksQzServices>();
                //var schedulerCenter = scope.ServiceProvider.GetRequiredService<ISchedulerCenter>();
                //var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
                // ������������
                app.UseSeedDataMildd(builder.Environment.WebRootPath, dbContext);
                // ����QuartzNetJob���ȷ���
                //app.UseQuartzJobMildd(tasksQzServices, schedulerCenter);
                // ����ע��
                //app.UseConsulMildd(Configuration, lifetime);
                // �¼����ߣ����ķ���
                //app.ConfigureEventBus();
                
                app.Run();//����
                
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