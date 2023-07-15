using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noon.Api.Errors;
using Noon.Core.Entities;
using Noon.Core.Services;

namespace Noon.Api.Controllers
{
    [Authorize]
    public class PaymentsController : GenericController
    {
        private readonly IPaymentService _paymentService;


        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            return basket is null ? NotFound(new ApiResponse(404, "Basket Not Found")) : Ok(basket);
        }
    }
}
