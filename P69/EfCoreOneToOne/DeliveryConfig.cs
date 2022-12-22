using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreOneToOne;

public class DeliveryConfig:IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.ToTable("T_Deliveries");
        builder.Property(d => d.CompanyName).IsUnicode().HasMaxLength(10);
        builder.Property(d => d.Number).HasMaxLength(50);
    }
}