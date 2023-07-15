using AutoMapper;
using Noon.Api.Dtos;
using Noon.Core.Entities;
using Noon.Core.Entities.Order_Aggregate;
using Address = Noon.Core.Entities.Identity.Address;

namespace Noon.Api.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPicUrlResolver>())
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name));

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto, Core.Entities.Order_Aggregate.Address>();



            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost))
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.shipToAddress, o => o.MapFrom(s => s.ShippingAddress))
                .ForMember(d=> d.Status ,o=> o.MapFrom(s=> s.OrderStatus));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPicUrlResolver>())
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName));
        }

    }
}
