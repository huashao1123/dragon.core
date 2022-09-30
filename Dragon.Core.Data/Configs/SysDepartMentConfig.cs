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
    public class SysDepartMentConfig : IEntityTypeConfiguration<SysDepartMent>
    {
        public void Configure(EntityTypeBuilder<SysDepartMent> builder)
        {
            builder.Property(d=>d.Name).HasMaxLength(200).IsRequired();
            builder.HasIndex(d => d.Name);
            builder.Property(d => d.Code).HasMaxLength(50);
            builder.Ignore(d => d.Children);
        }
    }
}
