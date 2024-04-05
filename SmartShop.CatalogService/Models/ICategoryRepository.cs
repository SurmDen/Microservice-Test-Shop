using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartShop.CatalogService.Models
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> Categories { get; }

        public void CreateCategory(Category category);

        public void DeleteCategory(long id);
    }
}
