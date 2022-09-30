using Microsoft.Extensions.DependencyInjection;
using WebApiClient.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions
{
    /// <summary>
    /// WebApiClientSetup 启动服务
    /// </summary>
    public static class WebApiClientSetup
    {
        /// <summary>
        /// 注册WebApiClient接口
        /// </summary>
        /// <param name="services"></param>
        public static void AddHttpApi(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //services.AddHttpApi<IBlogApi>().ConfigureHttpApiConfig(c =>
            //{
            //    c.HttpHost = new Uri("");
            //    c.FormatOptions.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            //});
            //services.AddHttpApi<IDoubanApi>().ConfigureHttpApiConfig(c =>
            //{
            //    c.HttpHost = new Uri("");
            //    c.FormatOptions.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            //});
        }
    }
}
