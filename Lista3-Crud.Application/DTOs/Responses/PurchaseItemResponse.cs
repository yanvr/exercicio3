using Lista3_Crud.Domain.Entities;

namespace Lista3_Crud.Application.DTOs.Responses
{
    public record PurchaseItemResponse(int Id, int PurchaseId, int ProductId)
    {
        public static PurchaseItemResponse MapToResponse(PurchaseItem purchaseItem)
        {
            return new PurchaseItemResponse(purchaseItem.Id, purchaseItem.PurchaseId, purchaseItem.ProductId);
        }

        public static IEnumerable<PurchaseItemResponse> MapToResponseList(IEnumerable<PurchaseItem> purchaseItems)
        {
            return purchaseItems.Select(p => MapToResponse(p));
        }
    }
}