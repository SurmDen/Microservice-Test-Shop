using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartShop.OrderService.Models
{
    public interface IOrderRepository
    {
        public IEnumerable<Order> GetOrders();
        public void MakeOrder(string address);
        public void ShipOrder(long id);
        public void DeleteOrder(long id);
    }
}
