using System.Net;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using ECommerceAPI.Application.ViewModels.Products;

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
        public async Task<IActionResult> Get()
        {
            //CRUD yok, no-tracking
            return Ok(_productReadRepository.GetAll(false));
        }


        //Test
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok( await _productReadRepository.GetByIdAsync(id, false));
        }

        //Test
        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        //Test
        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
           var product = await _productReadRepository.GetByIdAsync(model.Id);
           product.Stock = model.Stock;
           product.Name = model.Name;
           product.Price = model.Price;
           await _productWriteRepository.SaveAsync();
           return Ok();
        }

        //Test
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.Remove(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}


/*

 await _productWriteRepository.AddRangeAsync(new()
   {
       new Product{Id = Guid.NewGuid(),Name = "Product 1",Price = 131,CreatedDate = DateTime.UtcNow,Stock = 10},
       new Product{Id = Guid.NewGuid(),Name = "Product 2",Price = 231,CreatedDate = DateTime.UtcNow,Stock = 20},
       new Product{Id = Guid.NewGuid(),Name = "Product 3",Price = 331,CreatedDate = DateTime.UtcNow,Stock = 130},
   });
   await _productWriteRepository.SaveAsync();

 */