using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Classes
{
    public class GetFilteredProductsDto
    {
        public int IdBrand { get; set; }
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public int PriceMin { get; set; }
        public int PriceMax { get; set;}
    }

    public class FilteredProductsDto
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public decimal Score { get; set; }

        public string Category { get; set; }

        public string Brand { get; set; }
    }
}
