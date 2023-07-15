namespace Noon.Core.Entities.Order_Aggregate
{
    public class Order :BaseEntity
    {
        public Order(string userEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> orderItems, decimal subTotal, string paymentIntentId)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
            
        }

        public Order()
        {
            
        }

        public string UserEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;


        public Address ShippingAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }


        public decimal SubTotal { get; set; }

        public decimal Total()
            => DeliveryMethod.Cost + SubTotal;

        public string PaymentIntentId { get; set; } 



    }
}
