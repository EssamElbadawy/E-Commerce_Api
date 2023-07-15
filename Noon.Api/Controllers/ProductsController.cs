using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Noon.Api.Dtos;
using Noon.Api.Helper;
using Noon.Core;
using Noon.Core.Entities;
using Noon.Core.Specifications;

namespace Noon.Api.Controllers
{
   
    public class ProductsController : GenericController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(
             IMapper mapper,
             IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParam specParam)
        {

            var spec = new ProductWithBrandAndTypeSpecification(specParam);
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            var countSpec = new ProductCountSpecification(specParam);
            var response = new Pagination<ProductToReturnDto>()
            {
                PageSize = specParam.PageSize,
                PageIndex = specParam.PageIndex,
                Data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products),
                Count = await _unitOfWork.Repository<Product>().GetCountWithAsync(countSpec)
            };

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            //var product = await _productRepository.GetAsync(id);
            var spec = new ProductWithBrandAndTypeSpecification(id);

            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(spec);
            var mappedProduct = _mapper.Map<ProductToReturnDto>(product);
            return Ok(mappedProduct);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetTypes()
        {
            var types = await _unitOfWork.Repository<ProductType>().GetAllAsync();

            return Ok(types);
        }


        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);

        }
    }
}
