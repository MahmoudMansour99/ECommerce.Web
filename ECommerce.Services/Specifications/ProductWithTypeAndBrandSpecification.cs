using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product, int>
    {

        public ProductWithTypeAndBrandSpecification(int id):base(P => P.Id == id)
        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);
        }
        public ProductWithTypeAndBrandSpecification(ProductQueryParams queryParams)
            : base(ProductSpecificationHelper.GetProductCriteria(queryParams))
        {
            AddInclude(P => P.ProductType);
            AddInclude(P => P.ProductBrand);

            switch(queryParams.sort)
            {
                case ProductSortingOptions.NameAsc: 
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                default:
                    AddOrderBy(P => P.Id);
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }
    }
}
