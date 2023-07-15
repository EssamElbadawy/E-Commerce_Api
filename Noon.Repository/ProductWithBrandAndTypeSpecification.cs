using Noon.Core.Entities;
using Noon.Core.Specifications;

namespace Noon.Repository
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {

        public ProductWithBrandAndTypeSpecification(int id )
        :base(i=> i.Id == id)
        {
            IncludesExpressions.Add(p=> p.ProductBrand);
            IncludesExpressions.Add(p=> p.ProductType);
        }


        public ProductWithBrandAndTypeSpecification()
        {

            IncludesExpressions.Add(p => p.ProductBrand);
            IncludesExpressions.Add(p => p.ProductType);
        }
    }
}
