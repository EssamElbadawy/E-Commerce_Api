using Noon.Core.Entities.Order_Aggregate;

namespace Noon.Core.Services
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string userEmail, string basketId, int deliveryMethodId, Address shippingAddress);

        Task<IReadOnlyList<Order>> GetOrdersForUSerAsync(string userEmail);

        Task<Order> GetOrderByIdForUserAsync(string userEmail, int orderId);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
