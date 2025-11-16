using ECommerce.Services_Abstractions;
using ECommerce.Shared;
using ECommerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetProducts([FromQuery]ProductQueryParams queryParams)
        {
            var Products = await _productService.getAllProductsAsync(queryParams);

            return Ok(Products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var Product = await _productService.getProductByIdAsync(id);
            return Ok(Product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
        {
            var Brands = await _productService.GetAllBrandsAsync();

            return Ok(Brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
        {
            var Types = await _productService.GetAllTypesAsync();
            return Ok(Types);
        }

    }
}
