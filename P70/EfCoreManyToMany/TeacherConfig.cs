using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreManyToMany;

public class TeacherConfig:IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("T_Teacher");
        builder.Property(t => t.Name).IsUnicode().HasMaxLength(20);
    }
}