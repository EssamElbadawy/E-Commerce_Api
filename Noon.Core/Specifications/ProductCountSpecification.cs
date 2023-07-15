using Noon.Core.Entities;

namespace Noon.Core.Specifications
{
    public class ProductCountSpecification : BaseSpecification<Product>
    {
        public ProductCountSpecification(ProductSpecParam specPrams)
        : base(p =>
            (string.IsNullOrWhiteSpace(specPrams.Search) || p.Name.ToLower().Contains(specPrams.Search)) &&
            (!specPrams.BrandId.HasValue || p.ProductBrandId == specPrams.BrandId) &&
            (!specPrams.TypeId.HasValue || p.ProductTypeId == specPrams.TypeId)
            )
        {

        }
    }
}
