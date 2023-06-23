using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Classes
{
    public class SaleDto
    {
        public int IdCustomer { get; set; }
        public int IdStore { get; set; }
        public List<ItemSaleDto> Items { get; set; }
    }

    public class ItemSaleDto
    {
        public int IdSale { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { set; get; }
    }
}
