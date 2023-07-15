using Noon.Core.Entities.Order_Aggregate;

namespace Noon.Api.Dtos
{
    public class OrderToReturnDto
    {
        public string UserEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } 

        public string Status { get; set; }


        public Address shipToAddress { get; set; }

        public string DeliveryMethod { get; set; }
        public int DeliveryMethodCost { get; set; }

        public ICollection<OrderItemDto> OrderItems { get; set; }


        public decimal SubTotal { get; set; }


        public string PaymentIntentId { get; set; }

        public decimal Total { get; set; }  
    }
}
