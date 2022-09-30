using Dragon.Core.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions
{
    /// <summary>
    /// DB启动服务
    /// </summary>
    public static class DbSetup
    {
        public static void AddDbSetup(this IServiceCollection services)
        {
            if (services==null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddScoped<EfDbContext>();
            services.AddScoped<DBSeed>();
        }
    }
}
