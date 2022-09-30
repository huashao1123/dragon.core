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
    public class SysMenuConfig : IEntityTypeConfiguration<SysMenu>
    {
        public void Configure(EntityTypeBuilder<SysMenu> builder)
        {
            builder.Property(d => d.Name).HasMaxLength(200).IsRequired();
            builder.Property(d => d.Component).HasMaxLength(200);
            builder.Property(d=>d.CurrentActiveMenu).HasMaxLength(200);
            builder.Property(d=>d.FrameSrc).HasMaxLength(500);
            builder.Property(d => d.Icon).HasMaxLength(200);
            builder.Property(d=>d.path).HasMaxLength(500);
            builder.Property(d=>d.Permission).HasMaxLength(200);
            builder.Property(d => d.Redirect).HasMaxLength(200);
            builder.Property(d=>d.Title).HasMaxLength(200);
            builder.Ignore(d => d.Children);
        }
    }
}
