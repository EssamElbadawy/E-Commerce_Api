using Noon.Core.Entities.Order_Aggregate;

namespace Noon.Core.Specifications.OrderSpec
{
    public class OrderSpecification :BaseSpecification<Order>
    {
        public OrderSpecification(string userEmail)
        :base(e=> e.UserEmail == userEmail)
        {
            IncludesExpressions.Add(o=> o.DeliveryMethod);
            IncludesExpressions.Add(o=> o.OrderItems);
        } 
        
        public OrderSpecification(string userEmail , int id)
        :base(
            e=> e.UserEmail == userEmail && 
                e.Id == id
        )
        {
            IncludesExpressions.Add(o=> o.DeliveryMethod);
            IncludesExpressions.Add(o=> o.OrderItems);
        }
    }
}
