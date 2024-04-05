using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartShop.CatalogService.Models;

namespace SmartShop.CatalogService.Controllers
{
    [ApiController]
    [Route("Categories")]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet("All")]
        public IActionResult GetAllCategories()
        {
            if (categoryRepository.Categories.Count() != 0)
            {
                return Ok(categoryRepository.Categories);
            }

            return BadRequest();
        }

        [HttpPost("Create")]
        public IActionResult CreateCategory(Category category)
        {
            if (category != null)
            {
                categoryRepository.CreateCategory(category);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult DeleteCategory(long id)
        {
            if (id != 0)
            {
                categoryRepository.DeleteCategory(id);
                return Ok();
            }

            return BadRequest();
        }
    }
}
