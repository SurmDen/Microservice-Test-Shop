using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using SmartShop.CatalogService.Models;

namespace SmartShop.CatalogService.ProtoServices
{
    public class CatalogServiceProtoModel : CatalogServiceProto.CatalogServiceProtoBase
    {
        private ILogger<CatalogServiceProtoModel> logger;
        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;

        public CatalogServiceProtoModel
            (ILogger<CatalogServiceProtoModel> logger, 
             IProductRepository productRepository,
             ICategoryRepository categoryRepository)
        {
            this.logger = logger;
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public override Task<ProductModelCreated> CreateProduct(ProductModel request, ServerCallContext context)
        {
            Product product;

            if (request.CategoryId == 0)
            {
                product = new Product
                {
                    ProductName = request.ProductName,
                    Description = request.Description,
                    Price = request.Price,
                    Category = new Category { CategoryName = request.CategoryName }
                };
            }
            else
            {
                product = new Product
                {
                    ProductName = request.ProductName,
                    Description = request.Description,
                    Price = request.Price,
                    CategoryId = request.CategoryId
                };
            }

            productRepository.CreateProduct(product);
            return Task.FromResult(new ProductModelCreated());
        }

        public override Task<ProductModelUpdated> UpdateProduct(ProductModel request, ServerCallContext context)
        {
            Product product;
            if (request.CategoryId == 0)
            {
                product = new Product
                {
                    Id = request.Id,
                    ProductName = request.ProductName,
                    Description = request.Description,
                    Price = request.Price,
                    Category = new Category { CategoryName = request.CategoryName }
                };
            }
            else
            {
                product = new Product
                {
                    Id = request.Id,
                    ProductName = request.ProductName,
                    Description = request.Description,
                    Price = request.Price,
                    CategoryId = request.CategoryId
                };
            }

            productRepository.UpdateProduct(product);
            return Task.FromResult(new ProductModelUpdated());
        }

        public override Task<ProductModelDeleted> DeleteProduct(GetProductModel request, ServerCallContext context)
        {
            productRepository.DeleteProduct(request.Id);
            return Task.FromResult(new ProductModelDeleted());
        }

        public override Task<ProductModel> GetProduct(GetProductModel request, ServerCallContext context)
        {
            Product product = productRepository.GetProduct(request.Id);
            ProductModel productModel = null;
            if (product != null)
            {
                productModel = new ProductModel
                {
                    Id = (int)product.Id,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryId = (int)product.CategoryId,
                    CategoryName = product.Category.CategoryName
                };
            }

            return Task.FromResult(productModel);
        }

        public override async Task GetAllProducts(GetProductModels request, IServerStreamWriter<ProductModel> responseStream, ServerCallContext context)
        {
            List<Product> products = productRepository.GetProducts().ToList();
            foreach (Product product in products)
            {
                await responseStream.WriteAsync(new ProductModel 
                {
                    Id = (int)product.Id,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryId = (int)product.CategoryId,
                    CategoryName = product.Category.CategoryName
                });
            }
            
        }

        public override Task<CategoryModelCreated> CreateCategory(CategoryModel request, ServerCallContext context)
        {
            Category category = new Category
            {
                CategoryName = request.CategoryName
            };

            categoryRepository.CreateCategory(category);

            return Task.FromResult(new CategoryModelCreated());
        }

        public override Task<CategoryModelDeleted> DeleteCategory(CategoryModelId request, ServerCallContext context)
        {
            categoryRepository.DeleteCategory(request.Id);

            return Task.FromResult(new CategoryModelDeleted());
        }

        public override async Task GetAllCategories(GetCategoryModels request, IServerStreamWriter<CategoryModel> responseStream, ServerCallContext context)
        {
            List<Category> categories = categoryRepository.Categories.ToList();

            foreach (Category category in categories)
            {
                await responseStream.WriteAsync(new CategoryModel { Id = (int)category.Id, CategoryName = category.CategoryName });
            }
        }
    }
}
