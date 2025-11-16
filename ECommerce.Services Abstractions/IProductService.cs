using ECommerce.Shared;
using ECommerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services_Abstractions
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDTO>> getAllProductsAsync(ProductQueryParams queryParams);

        Task<ProductDTO> getProductByIdAsync(int productId);
        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
        Task<IEnumerable<BrandDTO>> GetAllTypesAsync();
    }
}
