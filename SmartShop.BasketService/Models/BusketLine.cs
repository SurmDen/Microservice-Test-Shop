using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusketManager;

namespace SmartShop.BasketService.Models
{
    public class BusketLine
    {
        
        public int Quantity { get; set; }

        public BusketProduct BusketProduct { get; set; }

       
    }
}
