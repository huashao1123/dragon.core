using Dragon.Core.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions
{
    /// <summary>
    /// Swagger 启动服务
    /// </summary>
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }


            var basePath = AppContext.BaseDirectory;  //api的bin文件夹
            string apiName = Appsettings.app(new string[] { "Startup", "ApiName" });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "v0.1.0",
                    Title = $"{apiName} 接口文档——Net 6",
                    Description = $"{apiName} HTTP API V1",
                    Contact = new OpenApiContact { Email = "longwang289@foxmail.com", Name = apiName, Url = new Uri("https://www.baidu.com") },
                    License = new OpenApiLicense { Name = $"{apiName} 官方文档", Url = new Uri("https://www.baidu.com") }
                });
                c.OrderActionsBy(p => p.RelativePath);

                try
                {
                    //就是这里！！！！！！！！！
                    var xmlPath = Path.Combine(basePath, "Dragon.Core.Api.xml");//这个就是刚刚配置的xml文件名
                    c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                    var xmlModelPath = Path.Combine(basePath, "Dragon.Entity.xml");//这个就是Model层的xml文件名
                    c.IncludeXmlComments(xmlModelPath);
                }
                catch (Exception ex)
                {
                    LogServer.WriteErrorLog("xml文件", "Dragon.Entity.xml 丢失，请检查并拷贝。\n", ex);
                    //_logger.Error(ex, "Drogan.Core.Drogan.Core.Model.xml 丢失，请检查并拷贝。\n");
                }


                // 在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();


                // 必须是 oauth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });


            });
        }
    }
}
