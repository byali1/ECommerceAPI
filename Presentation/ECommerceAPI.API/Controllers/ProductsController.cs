using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        //Test
        [HttpGet]
        public async Task Get()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new Product{Id = Guid.NewGuid(),Name = "Product 1",Price = 131,CreatedDate = DateTime.UtcNow,Stock = 10},
                new Product{Id = Guid.NewGuid(),Name = "Product 2",Price = 231,CreatedDate = DateTime.UtcNow,Stock = 20},
                new Product{Id = Guid.NewGuid(),Name = "Product 3",Price = 331,CreatedDate = DateTime.UtcNow,Stock = 130},
            });
            await _productWriteRepository.SaveAsync();
        }
    }
}
