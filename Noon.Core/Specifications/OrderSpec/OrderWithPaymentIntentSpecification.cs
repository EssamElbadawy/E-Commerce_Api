using Noon.Core.Entities.Order_Aggregate;

namespace Noon.Core.Specifications.OrderSpec
{
    public class OrderWithPaymentIntentSpecification : BaseSpecification<Order>
    {
        public OrderWithPaymentIntentSpecification(string paymentIntentId)
        : base(order => order.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
