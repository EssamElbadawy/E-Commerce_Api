using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noon.Core.Entities.Order_Aggregate;

namespace Noon.Repository.Data.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, s => s.WithOwner());

            builder.Property(o => o.OrderStatus).HasConversion(
                o => o.ToString(),
                o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)

                );
          

            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
        }
    }
}
