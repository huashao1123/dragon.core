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
    public class SysUserConfig : IEntityTypeConfiguration<SysUser>
    {
        public void Configure(EntityTypeBuilder<SysUser> builder)
        {
            builder.HasIndex(d => d.Name);
            builder.Property(d => d.Account).HasMaxLength(200);
            builder.Property(d => d.Password).HasMaxLength(200);
            builder.Property(d=>d.Avater).HasMaxLength(200);
            builder.Property(d=>d.Email).HasMaxLength(200);
            builder.Property(d=>d.JobName).HasMaxLength(200);
            builder.Property(d=>d.Name).HasMaxLength(200).IsRequired();
            builder.Property(d => d.Email).HasMaxLength(200);
            builder.Property(d=>d.HistroyPsw).HasMaxLength(200);
            builder.Property(d=>d.Mobile).HasMaxLength(200);
            builder.Property(d=>d.WeChat).HasMaxLength(200);
            builder.Property(d=>d.Remark).HasMaxLength(200);
        }
    }
}
