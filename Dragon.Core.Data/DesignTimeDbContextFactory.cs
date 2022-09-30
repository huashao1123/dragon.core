using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EfDbContext>
    {
        public EfDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EfDbContext>();
            optionsBuilder.UseSqlite("Data Source=D:\\DragonCore.db");
            //optionsBuilder.UseSqlServer($"Data Source=192.168.100.75;Initial Catalog=DragonCore;User ID=SA;Password=");
            return new EfDbContext(optionsBuilder.Options);
        }
    }
}
