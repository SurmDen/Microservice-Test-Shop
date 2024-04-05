using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusketManager;

namespace SmartShop.OrderService.Models
{
    public class OrderLine
    {
        public long Id { get; set; }

        public int Quantity { get; set; }

        public BusketProduct Product { get; set; }

        public Order Order { get; set; }

        public long OrderId { get; set; }
    }
}
