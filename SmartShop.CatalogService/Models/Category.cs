using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartShop.CatalogService.Models
{
    public class Category
    {
        public long Id { get; set; }

        public string CategoryName { get; set; }

        public List<Product> Products { get; set; }
    }
}
