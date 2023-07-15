using System.Text.Json;
using Noon.Core.Entities;
using Noon.Core.Entities.Order_Aggregate;

namespace Noon.Repository.Data
{
    public static class ProductDataSeeding
    {
        public static async Task SeedDataAsync(DataContext context)
        {

            if (!context.Types.Any())
            {
                var typesData = await File.ReadAllTextAsync("../Noon.Repository/Data/DataSeed/types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                if (types is { Count: > 0 })
                    foreach (var type in types)
                        await context.Types.AddAsync(type);

                await context.SaveChangesAsync();

            }

            if (!context.Brands.Any())
            {
                var brandsData = await File.ReadAllTextAsync("../Noon.Repository/Data/DataSeed/brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands is { Count: > 0 })
                    foreach (var brand in brands)
                        await context.Brands.AddAsync(brand);

                await context.SaveChangesAsync();

            }

            if (!context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Noon.Repository/Data/DataSeed/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products is { Count: > 0 })
                    foreach (var product in products)
                        await context.Products.AddAsync(product);

                await context.SaveChangesAsync();
            }


            if (!context.DeliveryMethods.Any())
            {
                var deliveryMethodsData = await File.ReadAllTextAsync("../Noon.Repository/Data/DataSeed/delivery.json");

                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                if (deliveryMethods is { Count: > 0 })
                    foreach (var method in deliveryMethods)
                        await context.DeliveryMethods.AddAsync(method);

                await context.SaveChangesAsync();
            }



        }
    }
}
