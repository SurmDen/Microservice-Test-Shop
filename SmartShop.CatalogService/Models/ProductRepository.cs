using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusketManager;

namespace SmartShop.CatalogService.Models
{
    public class ProductRepository : IProductRepository
    {
        public ProductDbContext Context;

        public ProductRepository(ProductDbContext productDbContext)
        {
            Context = productDbContext;
        }

        public IEnumerable<Product> GetProducts()
        {
            IEnumerable<Product> products =  Context.Products.Include(p => p.Category);
            foreach (Product p  in products)
            {
                p.Category.Products = null;
            }

            return products;
        }

        public BusketProduct GetBusketProduct(long id)
        {
            Product product = Context.Products.Include(p => p.Category).First(p=>p.Id == id);
            product.Category.Products = null;
            return new BusketProduct()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                CategoryName = product.Category.CategoryName
            };
        }



        public void CreateProduct(Product product)
        {
             Context.Products.Add(product);
             Context.SaveChanges();
        }

        public void DeleteProduct(long id)
        {
            Context.Products.Remove(Context.Products.Find(id));
            Context.SaveChanges();
        }

        public Product GetProduct(long id)
        {
            Product p = Context.Products.Include(p => p.Category).First(p=>p.Id == id);
            p.Category.Products = null;
            return p;
        }

        public void UpdateProduct(Product product)
        {
            Context.Products.Update(product);
            Context.SaveChanges();
        }
    }
}
