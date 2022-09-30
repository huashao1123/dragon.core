using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions
{
    public static class MiddlewareHelpers
    {
        /// <summary>
        /// 请求响应中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseReuestResponseLog(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequRespLogMilddleware>();
        }

        /// <summary>
        /// IP请求中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseIPLogMildd(this IApplicationBuilder app)
        {
            return app.UseMiddleware<IPLogMilddleware>();
        }

        /// <summary>
        /// 用户访问中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRecordAccessLogsMildd(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RecordAccessLogsMilddleware>();
        }
    }
}
