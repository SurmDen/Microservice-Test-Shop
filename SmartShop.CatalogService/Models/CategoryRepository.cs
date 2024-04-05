using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartShop.CatalogService.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private ProductDbContext context;

        public CategoryRepository(ProductDbContext productDbContext)
        {
            context = productDbContext;
        }

        public IEnumerable<Category> Categories => context.Categories;

        public void CreateCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void DeleteCategory(long id)
        {
            context.Categories.Remove(context.Categories.Find(id));
            context.SaveChanges();
        }
    }
}
