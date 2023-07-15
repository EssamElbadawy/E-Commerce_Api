using Noon.Core;
using Noon.Core.Entities;
using Noon.Core.Entities.Order_Aggregate;
using Noon.Core.Repositories;
using Noon.Core.Services;
using Noon.Core.Specifications.OrderSpec;

namespace Noon.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
            _paymentService = paymentService;
        }
        public async Task<Order?> CreateOrderAsync(string userEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {

            var basket = await _basketRepository.GetBasketAsync(basketId);

            var orderItems = new List<OrderItem>();
            if (basket is not null)
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);

                    orderItems.Add(orderItem);
                }
            else
                return null;

            var subTotal = orderItems.Sum(i => i.Cost * i.Quantity);

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);
            var spec = new OrderWithPaymentIntentSpecification(basket.PaymentIntentId);

            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);
            if (existingOrder != null)
            {

                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);

            }

            var order = new Order(userEmail, shippingAddress, deliveryMethod, orderItems, subTotal, basket.PaymentIntentId);

            await _unitOfWork.Repository<Order>().Add(order);

            await _unitOfWork.Complete();

            return order;
        }









        public async Task<IReadOnlyList<Order>> GetOrdersForUSerAsync(string userEmail)
        {
            var spec = new OrderSpecification(userEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            return orders;
        }

        public async Task<Order> GetOrderByIdForUserAsync(string userEmail, int orderId)
        {
            var spec = new OrderSpecification(userEmail, orderId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
            => await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
    }
}
