using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared
{
    public class ProductQueryParams
    {
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public string? search { get; set; }
        public ProductSortingOptions sort { get; set; }

        private int _PageIndex = 1;
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
            set
            {
                _PageIndex = (value <= 1) ? 1 : value;
            }
        }

        private const int DefaultPageIndex = 5;
        private const int MaxPageSize = 10;
        private int _PageSize = DefaultPageIndex;
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                if (value <= 0)
                    _PageSize = DefaultPageIndex;
                else if (value > MaxPageSize)
                    _PageSize = MaxPageSize;
                else
                    _PageSize = value;
            }
        }
    }
}
