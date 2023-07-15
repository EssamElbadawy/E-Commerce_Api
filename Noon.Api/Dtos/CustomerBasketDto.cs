using System.ComponentModel.DataAnnotations;

namespace Noon.Api.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }

        public decimal ShippingPrice { get; set; }
        [Required]
        public List<BasketItemDto> Items { get; set; } = new();
    }
}
