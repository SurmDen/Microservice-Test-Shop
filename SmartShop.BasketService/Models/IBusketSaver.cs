using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartShop.BasketService.Models
{
    public interface IBusketSaver
    {
        public void GetBusket(Busket busket);
        public List<BusketLine> SetBusket();
    }

    public class BusketSaver : IBusketSaver
    {
        private Busket Busket;

        public void GetBusket(Busket busket)
        {
            Busket = busket;
        }

        public List<BusketLine> SetBusket()
        {
            return Busket.BusketLines;
        }
    }
}
