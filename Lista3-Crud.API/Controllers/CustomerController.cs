using Lista3_Crud.Application.DTOs.Requests;
using Lista3_Crud.Application.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lista3_Crud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetById(id);

            if (customer is null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetAll();

            if (!customers.Any())
            {
                return NotFound();
            }

            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request)
        {
            await _customerService.Add(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerRequest request)
        {
            await _customerService.Update(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.Delete(id);
            return Ok();
        }
    }
}