using Dragon.Core.Common;
using Dragon.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions.ServiceExtensions
{
    public static class EfCoreSetup
    {
        public static void AddEfCoreSetup(this IServiceCollection services)
        {
            if(services == null) throw new ArgumentNullException(nameof(services));
            string dbConType = Appsettings.app(new string[] { "ConnectionStrings", "DBConnction" });
            string conStr = Appsettings.app(new string[] { "ConnectionStrings", dbConType });
            string dbType = dbConType.Split('-')[0];
            switch (dbType)
            {
                case "Sqlite":
                    services.AddDbContext<EfDbContext>(options => options.UseSqlite(conStr, o => o.MigrationsAssembly("DragonSea.Core.Data")));
                    break;
                default:
                    services.AddDbContext<EfDbContext>(options => options.UseSqlServer(conStr, o => o.MigrationsAssembly("DragonSea.Core.Data")));
                    break;
            }
        }
    }
}
