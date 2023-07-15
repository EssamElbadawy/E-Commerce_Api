using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Noon.Api.Dtos;
using Noon.Api.Errors;
using Noon.Core.Entities;
using Noon.Core.Repositories;

namespace Noon.Api.Controllers
{
    public class BasketController : GenericController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;


        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }


        [HttpGet]

        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket);
        }


        [HttpPost]

        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
            var createdOrUpdated = await _basketRepository.UpdateBasketAsync(mappedBasket);

           return createdOrUpdated is not null ? Ok(createdOrUpdated) : BadRequest(new ApiResponse(400));
        }

        [HttpDelete]

        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
