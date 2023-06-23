using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Classes
{
    public class RateProductDto
    {
        public int IdProduct { set;get; }
        public int IdCustomer { set; get; }
        public int Score { set; get; }
    }
}