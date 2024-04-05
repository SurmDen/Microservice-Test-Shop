using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusketManager;

namespace SmartShop.CatalogService.Models
{
    public interface IProductRepository
    {
        public BusketProduct GetBusketProduct(long id);
        public IEnumerable<Product> GetProducts();
        public void CreateProduct(Product product);
        public void DeleteProduct(long id);
        public void UpdateProduct(Product product);
        public Product GetProduct(long id);
    }
}
