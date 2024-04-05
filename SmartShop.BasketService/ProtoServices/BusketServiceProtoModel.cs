using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using SmartShop.BasketService.Models;

namespace SmartShop.BasketService.ProtoServices
{
    public class BusketServiceProtoModel:BusketServiceProto.BusketServiceProtoBase
    {
        private ILogger<BusketServiceProtoModel> logger;
        private IBusketSaver busket;

        public BusketServiceProtoModel(ILogger<BusketServiceProtoModel> logger, IBusketSaver busket )
        {
            this.busket = busket;
            this.logger = logger;
        }

        public override async Task GetBusketLines(GetBusketLineModels request, IServerStreamWriter<BusketLineModel> responseStream, ServerCallContext context)
        {
            List<BusketLine> lines = busket.SetBusket();

            foreach (BusketLine line in lines)
            {
                await responseStream.WriteAsync(new BusketLineModel 
                {
                    Id = (int)line.BusketProduct.Id,
                    ProductName = line.BusketProduct.ProductName,
                    Price = line.BusketProduct.Price,
                    Description = line.BusketProduct.Description,
                    CategoryName = line.BusketProduct.CategoryName,
                    Quantity = line.Quantity
                });
            }
        }
    }
}
