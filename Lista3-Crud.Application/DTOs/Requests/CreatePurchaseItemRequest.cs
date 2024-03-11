using Lista3_Crud.Domain.Entities;

namespace Lista3_Crud.Application.DTOs.Requests
{
    public record CreatePurchaseItemRequest(int PurchaseId, int ProductId, int Quantity, decimal Price)
    {
        public static PurchaseItem MapToEntity(CreatePurchaseItemRequest request)
        {
            return PurchaseItem.CreateInstance(request.ProductId, request.PurchaseId, request.Quantity, request.Price);
        }

        public static IEnumerable<PurchaseItem> MapToEntityList(IEnumerable<CreatePurchaseItemRequest> requestList)
        {
            return requestList.Select(r => MapToEntity(r));
        }
    }
}