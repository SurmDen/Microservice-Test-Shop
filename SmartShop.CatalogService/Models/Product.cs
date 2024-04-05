using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartShop.CatalogService.Models
{
    public class Product
    {
        public long Id { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public long? CategoryId { get; set; }

        public Category Category{ get; set; }
    }
}
