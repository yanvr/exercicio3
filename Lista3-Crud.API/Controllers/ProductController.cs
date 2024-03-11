using Lista3_Crud.Application.DTOs.Requests;
using Lista3_Crud.Application.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lista3_Crud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet("{id}")]
        public async Task<IActionResult> Getproduct(int id)
        {
            var product = await _productService.GetById(id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> Getproducts()
        {
            var products = await _productService.GetAll();

            if (!products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {
            await _productService.Add(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequest request)
        {
            await _productService.Update(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.Delete(id);
            return Ok();
        }
    }
}