using AutoMapper;
using Noon.Api.Dtos;
using Noon.Core.Entities.Order_Aggregate;

namespace Noon.Api.Helper
{
    public class OrderItemPicUrlResolver :IValueResolver<OrderItem, OrderItemDto,string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPicUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
            => !string.IsNullOrWhiteSpace(source.Product.PictureUrl)
                ? $"{_configuration["BaseUrl"]}{source.Product.PictureUrl}"
                : string.Empty;
    }
}
