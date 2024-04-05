using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IdentityManager.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace SmartShop.OrderService.Models
{
    public class OrderRepository : IOrderRepository
    {
        private OrderDbContext context;

        public OrderRepository(OrderDbContext context)
        {
            this.context = context;
        }

        public void DeleteOrder(long id)
        {
            Order order = context.Orders.Include(o => o.Lines).ThenInclude(l=>l.Product).First(o => o.Id == id);
            context.Orders.Remove(order);
            context.SaveChanges();
        }

        public IEnumerable<Order> GetOrders()
        {
            IEnumerable<Order> orders = context.Orders.Include(o => o.Lines).ThenInclude(l=>l.Product);
            foreach (Order order in orders)
            {
                foreach (OrderLine product in order.Lines)
                {
                    product.Order = null;
                }
            }

            return orders;
        }

        public void MakeOrder(string address)
        {

            string answer = string.Empty;
            string answer2 = string.Empty;
            CurrentUser user = new CurrentUser();
            List<TempOrderLine> tempLines = new List<TempOrderLine>();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:5100/Account/CurrentUser");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        answer = reader.ReadToEnd();
                    }
                }
            }

            user = JsonConvert.DeserializeObject<CurrentUser>(answer) ?? null;



            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create("http://localhost:5400/Busket/BusketProducts");
            HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();

            if (response2.StatusCode == HttpStatusCode.OK)
            {
                using (Stream stream2 = response2.GetResponseStream())
                {
                    using (StreamReader reader2 = new StreamReader(stream2))
                    {
                        answer2 = reader2.ReadToEnd();
                    }
                }
            }

            tempLines = JsonConvert.DeserializeObject<List<TempOrderLine>>(answer2);
            List<OrderLine> lines = new List<OrderLine>();
            foreach (TempOrderLine t in tempLines)
            {
                lines.Add(new OrderLine 
                {
                    Quantity = t.Quantity,
                    Product = t.BusketProduct
                });
            }


            if (user != null && lines!= null)
            {
                foreach (OrderLine line in lines)
                {
                    line.Product.Id = default(long);
                }

                Order order = new Order()
                {
                    Address = address,
                    UserName = user.Name ?? "unnamed",
                    UserEmail = user.Email ?? "none",
                    Lines = lines
                };

                

                context.Orders.Add(order);
                context.SaveChanges();
            }
        }

        public void ShipOrder(long id)
        {
            Order order = context.Orders.Include(o => o.Lines).First(o => o.Id == id);
            order.Shiped = !order.Shiped;
            context.SaveChanges();
        }
    }
}
