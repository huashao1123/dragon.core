using Dragon.Core.Data.Configs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragon.Core.Entity;
using System.Reflection;

namespace Dragon.Core.Data
{
    public class EfDbContext : DbContext  
    {
        public EfDbContext(DbContextOptions<EfDbContext> options):base(options)
        {

        }

        //public DbSet<SysUser> SysUsers { get; set; }

        //public DbSet<SysRole> SysRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //批量配置实体
            //Assembly.Load("Dragon.Core.Entity").GetTypes()
            var entityTypes = typeof(BaseEntity).Assembly.GetTypes().Where(type => !String.IsNullOrEmpty(type.Namespace) && type.IsClass && !type.IsGenericType && type.GetTypeInfo().BaseType != null && !type.IsAbstract)
                 .Where(type => type.BaseType!.Name.StartsWith(nameof(BaseEntity)));
            //.Where(type =>  (type.GetTypeInfo().BaseType == typeof(BaseEntity)||type.BaseType.GetGenericTypeDefinition().IsAssignableFrom(typeof(BaseEntity<>))));

            foreach (Type type in entityTypes)
            {
                modelBuilder.Model.AddEntityType(type);
            }
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SysUserConfig).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=DragonCore.db");
            //optionsBuilder.UseSqlServer($"Data Source=192.168.100.75;Initial Catalog=DragonCore;User ID=SA;Password=");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
