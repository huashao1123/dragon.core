using AspNetCoreRateLimit;
using Dragon.Core.Common;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions
{
    /// <summary>
    /// 限流中间件
    /// </summary>
    public static class IpLimitMilddleware
    {
        public static void UseIpLimitMildd(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            try
            {
                if (Appsettings.app("Middleware", "IpRateLimit", "Enabled").ObjToBool())
                {
                    app.UseIpRateLimiting();
                }
            }
            catch (Exception e)
            {
                LogServer.WriteErrorLog("限流异常", $"Error occured limiting ip rate.\n{e.Message}", e);
                throw;
            }
        }
    }
}
