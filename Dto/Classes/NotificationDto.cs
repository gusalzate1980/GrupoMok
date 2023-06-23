using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Classes
{
    public class PurchaseNotificationDto
    {
        public string Customer { get; set; }
        public string Email { get;set;}
        public string Store { get; set; }
        public string Subject { get; set; }
        public List<PurchasedItem> PurchasedItems { get;set; }
    }

    public class PurchasedItem
    {
        public string Name { set;get;}
        public int Price { get; set; }
        public int Quantity { get;set;}
    }
}
