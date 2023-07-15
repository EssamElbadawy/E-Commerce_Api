
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Noon.Core.Entities;
using Noon.Core.Entities.Order_Aggregate;

namespace Noon.Repository.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new ProductConfig());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> Brands { get; set; }

        public DbSet<ProductType> Types { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }    

    }
}
