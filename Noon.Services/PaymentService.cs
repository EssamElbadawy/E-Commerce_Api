using Microsoft.Extensions.Configuration;
using Noon.Core;
using Noon.Core.Entities;
using Noon.Core.Entities.Order_Aggregate;
using Noon.Core.Repositories;
using Noon.Core.Services;
using Stripe;
using Product = Noon.Core.Entities.Product;

namespace Noon.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;

        public PaymentService(IUnitOfWork unitOfWork, IConfiguration configuration, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _basketRepository = basketRepository;
        }

        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripSetting:SecretKey"];
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) return null;
            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);

                basket.ShippingPrice = deliveryMethod.Cost;

                shippingPrice = deliveryMethod.Cost;
            }

            if (basket.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);

                    if (item.Price != product.Price)
                        item.Price = product.Price;


                }
            }

            var service = new PaymentIntentService();

            if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" },
                    Amount = (long)basket.Items.Sum(i => i.Price * i.Quantity * 100) + (long)shippingPrice
                };

                var paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(i => i.Price * i.Quantity * 100) + (long)shippingPrice
                };


                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepository.UpdateBasketAsync(basket);

            return basket;
        }
    }
}
