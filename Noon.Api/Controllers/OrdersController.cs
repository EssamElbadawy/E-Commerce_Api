using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noon.Api.Dtos;
using Noon.Api.Errors;
using Noon.Core.Entities.Order_Aggregate;
using Noon.Core.Services;

namespace Noon.Api.Controllers
{
    [Authorize]
    public class OrdersController : GenericController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {


            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var address = _mapper.Map<AddressDto, Address>(orderDto.shipToAddress);
            var order = await _orderService.CreateOrderAsync(userEmail, orderDto.BasketId, orderDto.DeliveryMethodId,
                 address);
             
            return order is null ? BadRequest(new ApiResponse(400)) : Ok(_mapper.Map<Order, OrderToReturnDto>(order));

        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersForUSerAsync(email);
            var mappedOrder = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders);

            return Ok(mappedOrder);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrder(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdForUserAsync(email, id);
            var mappedOrder = _mapper.Map<Order, OrderToReturnDto>(order);

            return order is null ? BadRequest(new ApiResponse(400)) : Ok(mappedOrder);
        }


        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var methods = await _orderService.GetDeliveryMethodsAsync();

            return Ok(methods);
        }
    }
}
