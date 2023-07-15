using AutoMapper;
using Noon.Api.Dtos;
using Noon.Core.Entities;

namespace Noon.Api.Helper
{
    public class ProductPicUrlResolver : IValueResolver<Product,ProductToReturnDto,string>
    {
        private readonly IConfiguration _configuration;

        public ProductPicUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember,
            ResolutionContext context)
            => !string.IsNullOrWhiteSpace(source.PictureUrl)
                ? $"{_configuration["BaseUrl"]}{source.PictureUrl}"
                : string.Empty;
    }
}
