using Lista3_Crud.Domain.Entities;

namespace Lista3_Crud.Application.DTOs.Requests
{
    public record CreatePurchaseRequest(int CustomerId, IEnumerable<CreatePurchaseItemRequest> PurchaseItems)
    {
        public static Purchase MapToEntity(CreatePurchaseRequest request)
        {
            var purchase = Purchase.CreateInstance(request.CustomerId);
            var purchaseItems = CreatePurchaseItemRequest.MapToEntityList(request.PurchaseItems);
            purchase.AddItems(purchaseItems);
            return purchase;
        }
    }
}