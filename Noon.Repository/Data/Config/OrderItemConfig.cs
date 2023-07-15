using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noon.Core.Entities.Order_Aggregate;

namespace Noon.Repository.Data.Config
{
    public class OrderItemConfig :IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(o => o.Product, p => p.WithOwner());

            builder.Property(o => o.Cost).HasColumnType("decimal(18,2)");
        }
    }
}
