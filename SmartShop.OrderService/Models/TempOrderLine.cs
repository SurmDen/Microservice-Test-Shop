using BusketManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartShop.OrderService.Models
{
    public class TempOrderLine
    {
        public int Quantity { get; set; }

        public BusketProduct BusketProduct { get; set; }
    }
}
