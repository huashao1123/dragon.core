using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragon.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dragon.Core.Data.Configs
{
    public class SysRoleConfig : IEntityTypeConfiguration<SysRole>
    {
        public void Configure(EntityTypeBuilder<SysRole> builder)
        {
            builder.HasIndex(d => d.Name);
            builder.Property(d=>d.Name).HasMaxLength(256);
            builder.Property(d => d.Code).HasMaxLength(200);
            builder.Property(d=>d.Remark).HasMaxLength(200);
        }
    }
}
