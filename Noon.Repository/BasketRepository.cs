using System.Text.Json;
using Noon.Core.Entities;
using Noon.Core.Repositories;
using StackExchange.Redis;

namespace Noon.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);

            return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var updatedOrCreatedBasket =
                await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(1));

            return !updatedOrCreatedBasket ? null : await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string id)
            => await _database.KeyDeleteAsync(id);
    }
}
