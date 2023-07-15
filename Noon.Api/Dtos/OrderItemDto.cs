namespace Noon.Api.Dtos
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }
        public decimal Cost { get; set; }

        public int Quantity { get; set; }
    }
}
