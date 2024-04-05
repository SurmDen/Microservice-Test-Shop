using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartShop.CatalogService.Models;
using Microsoft.AspNetCore.Authorization;

namespace SmartShop.CatalogService.Controllers
{
    [ApiController]
    [Route("Products")]
    public class ProductController : ControllerBase
    {
        private IProductRepository repository;

        public ProductController(IProductRepository Repository)
        {
            repository = Repository;
        }

        [HttpGet("All")]
        public IActionResult GetAllProducts()
        {
            return Ok(repository.GetProducts());
        }

        [HttpGet("ForBusket/{id}")]
        public IActionResult GetProductForBusket(long id)
        {
            return Ok(repository.GetBusketProduct(id));
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("Create")]
        public IActionResult CreateProduct(Product product)
        {
            if (product != null)
            {
                if (ModelState.IsValid)
                {
                    repository.CreateProduct(product);
                    return Ok();
                }
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Update")]
        public IActionResult UpdateProduct(Product product)
        {
            if (product != null)
            {
                if (ModelState.IsValid)
                {
                    repository.UpdateProduct(product);
                    return Ok();
                }
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteProduct(long id)
        {
            if (id != 0)
            {
                repository.DeleteProduct(id);
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("GetProduct/{id}")]
        public IActionResult GetProduct(long id)
        {
            if (id != 0)
            {
                return Ok(repository.GetProduct(id));
            }
            return BadRequest();
        }

    }
}
