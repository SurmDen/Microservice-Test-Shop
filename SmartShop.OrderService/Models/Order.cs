using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartShop.OrderService.Models
{
    public class Order
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string Address { get; set; }

        public List<OrderLine> Lines { get; set; }

        public bool Shiped { get; set; }
    }
}
