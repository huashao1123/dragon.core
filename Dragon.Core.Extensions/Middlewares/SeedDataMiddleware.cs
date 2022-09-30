using Dragon.Core.Common;
using Dragon.Core.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions 
{ 
    public static class SeedDataMiddleware
    {
        public static void UseSeedDataMildd(this IApplicationBuilder app, string webRootPath, EfDbContext dbContext)
        {
            //ILogger _logger = loggerFactory.CreateLogger(nameof(SeedDataMildd));
            if (app == null) throw new ArgumentNullException(nameof(app));

            try
            {
                if (Appsettings.app("AppSettings", "SeedDBEnabled").ObjToBool() || Appsettings.app("AppSettings", "SeedDBDataEnabled").ObjToBool())
                {
                    DBSeed.SeedAsync(dbContext, webRootPath).Wait();
                }
            }
            catch (Exception e)
            {
                LogServer.WriteErrorLog($"{nameof(SeedDataMiddleware)}",$"Error occured seeding the Database.",e);
                throw;
            }
        }
    }
}
