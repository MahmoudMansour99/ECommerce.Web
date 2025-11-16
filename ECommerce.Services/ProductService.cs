using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Services.Specifications;
using ECommerce.Services_Abstractions;
using ECommerce.Shared;
using ECommerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDTO>>(Brands);
        }

        public async Task<PaginatedResult<ProductDTO>> getAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Spec = new ProductWithTypeAndBrandSpecification(queryParams);
            var Products = await Repo.GetAllAsync(Spec);
            var DateToReturn = _mapper.Map<IEnumerable<ProductDTO>>(Products);
            var CountOfDataToReturn = DateToReturn.Count();
            var CountSpec = new ProductCountSpecification(queryParams);
            var CountOfAllProducts = await Repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDTO>(queryParams.PageIndex, CountOfDataToReturn, CountOfAllProducts, DateToReturn);
        }

        public async Task<IEnumerable<BrandDTO>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<BrandDTO>>(Types);
        }

        public async Task<ProductDTO> getProductByIdAsync(int id)
        {
            var Spec = new ProductWithTypeAndBrandSpecification(id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Spec);

            return _mapper.Map<ProductDTO>(Product);
        }
    }
}
