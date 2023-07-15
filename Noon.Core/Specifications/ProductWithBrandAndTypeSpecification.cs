using Noon.Core.Entities;

namespace Noon.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {

        public ProductWithBrandAndTypeSpecification(int id)
        : base(i => i.Id == id)
        {
            IncludesExpressions.Add(p => p.ProductBrand);
            IncludesExpressions.Add(p => p.ProductType);
        }


        public ProductWithBrandAndTypeSpecification(ProductSpecParam spec)
        : base(p =>
                (string.IsNullOrWhiteSpace(spec.Search) || p.Name.ToLower().Contains(spec.Search)) &&
                (!spec.BrandId.HasValue || p.ProductBrandId == spec.BrandId) &&
                (!spec.TypeId.HasValue || p.ProductTypeId == spec.TypeId)

            )
        {

            IncludesExpressions.Add(p => p.ProductBrand);
            IncludesExpressions.Add(p => p.ProductType);

            if (spec.Sort is null)
            {
                AddOrderBy(p => p.Name);
            }
            else
            {
                switch (spec.Sort.ToLower())
                {
                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }


            ApplyPagination(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);
        }
    }
}
