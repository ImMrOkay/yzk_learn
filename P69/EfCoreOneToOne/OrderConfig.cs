using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreOneToOne;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("T_Orders");
        builder.HasOne<Delivery>(o => o.Delivery).WithOne(d => d.Order).HasForeignKey<Delivery>(d => d.OrderId);
    }
}