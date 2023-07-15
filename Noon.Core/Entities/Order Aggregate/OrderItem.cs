namespace Noon.Core.Entities.Order_Aggregate
{
    public class OrderItem :BaseEntity
    {
        public OrderItem(ProductItemOrdered product, decimal cost, int quantity)
        {
            Product = product;
            Cost = cost;
            Quantity = quantity;
        }

        public OrderItem()
        {
            
        }
        public ProductItemOrdered Product { get; set; }
        public decimal Cost { get; set; }

        public int Quantity { get; set; }
    }
}
