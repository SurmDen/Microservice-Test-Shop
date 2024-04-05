using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartShop.OrderService.Models;
using Microsoft.Extensions.Logging;
using Grpc.Core;

namespace SmartShop.OrderService.ProtoServices
{
    public class OrderServiceProtoModel : OrderServiceProto.OrderServiceProtoBase
    {
        private ILogger<OrderServiceProtoModel> logger;
        private IOrderRepository repository;

        public OrderServiceProtoModel(ILogger<OrderServiceProtoModel> logger, IOrderRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public override Task<OrderModelMade> MakeOrder(MakeOrderModel request, ServerCallContext context)
        {
            repository.MakeOrder(request.Address);
            return Task.FromResult(new OrderModelMade());
        }

        public override Task<OrderModelDeleted> DeleteOrder(OrderModelsId request, ServerCallContext context)
        {
            repository.DeleteOrder(request.Id);
            return Task.FromResult(new OrderModelDeleted());
        }

        public override Task<OrderModelShipped> ShipOrder(OrderModelsId request, ServerCallContext context)
        {
            repository.ShipOrder(request.Id);
            return Task.FromResult(new OrderModelShipped());
        }

        public override async Task GetOrders(GetOrderModels request, IServerStreamWriter<OrderModel> responseStream, ServerCallContext context)
        {
            List <Order> orders = repository.GetOrders().ToList();

            foreach (Order order in orders)
            {
                await responseStream.WriteAsync(new OrderModel 
                {
                    Id = (int)order.Id,
                    UserName = order.UserName,
                    UserEmail = order.UserEmail,
                    Address = order.Address,
                    ProductName = string.Join(", ", order.Lines.Select(l => l.Product.ProductName)),
                    Price = order.Lines.Sum(l=>l.Product.Price * l.Quantity),
                    Shipped = order.Shiped
                });
            }
        }
    }
}
