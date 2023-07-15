namespace Noon.Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }

        public List<BasketItem> Items { get; set; } = new();

        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }

        public decimal ShippingPrice { get; set; }



        public CustomerBasket()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}
