using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusketManager;

namespace SmartShop.BasketService.Models
{
    public class Busket
    {
        public List<BusketLine> BusketLines { get; set; } = new List<BusketLine>();

        public void AddBusketLine(BusketProduct p, int quantity)
        {
            BusketLine line = BusketLines?.Where(bl => bl.BusketProduct.Id == p.Id).FirstOrDefault();
            if (line == null)
            {
                BusketLines.Add(new BusketLine() { BusketProduct = p, Quantity = quantity });  
            }
            else
            {
                line.Quantity += quantity;
            }
        }
    }
}
