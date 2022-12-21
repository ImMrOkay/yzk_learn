using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp1
{
    public class OrgUnitConfig:IEntityTypeConfiguration<OrgUnit>
    {
        public void Configure(EntityTypeBuilder<OrgUnit> builder)
        {
            builder.ToTable("T_OrgUnit");
            builder.Property(o => o.Name).IsUnicode().IsRequired().HasMaxLength(50);

            //根节点是没有Parent的因此，这个关系不能修饰为“不可为空”
            builder.HasOne<OrgUnit>(o=>o.Parent).WithMany(o=>o.Children)/*.IsRequired()*/;
        }
    }
}
