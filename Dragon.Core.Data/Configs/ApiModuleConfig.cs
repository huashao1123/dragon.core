using Dragon.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Data.Configs
{
    public class ApiModuleConfig : IEntityTypeConfiguration<ApiModule>
    {
        public void Configure(EntityTypeBuilder<ApiModule> builder)
        {
            builder.Property(d => d.ApiName).HasMaxLength(500);
            builder.Property(d=>d.ApiUrl).HasMaxLength(500).IsRequired();
        }
    }
}
