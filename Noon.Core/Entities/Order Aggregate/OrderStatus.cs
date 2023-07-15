using System.Text.Json.Serialization;

namespace Noon.Core.Entities.Order_Aggregate
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        Pending,
        PaymentReceived,
        PaymentFailed
    }
}
