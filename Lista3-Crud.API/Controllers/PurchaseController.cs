using Lista3_Crud.Application.DTOs.Requests;
using Lista3_Crud.Application.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lista3_Crud.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController(IPurchaseService purchaseService) : ControllerBase
    {
        private readonly IPurchaseService _purchaseService = purchaseService;

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPurchase(int id)
        {
            var purchase = await _purchaseService.GetById(id);

            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }

        [HttpGet]
        public async Task<ActionResult> GetPurchases()
        {
            var purchases = await _purchaseService.GetAll();

            if (!purchases.Any())
            {
                return NotFound();
            }

            return Ok(purchases);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePurchase(CreatePurchaseRequest request)
        {
            await _purchaseService.Add(request);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePurchase(UpdatePurchaseRequest request)
        {
            await _purchaseService.Update(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePurchase(int id)
        {
            await _purchaseService.Delete(id);
            return Ok();
        }
    }
}