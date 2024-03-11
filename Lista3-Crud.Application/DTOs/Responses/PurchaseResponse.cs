using Lista3_Crud.Domain.Entities;

namespace Lista3_Crud.Application.DTOs.Responses
{
    public record PurchaseResponse(int Id, int CustomerId, IEnumerable<PurchaseItemResponse> PurchaseItems)
    {
        public static PurchaseResponse MapToResponse(Purchase purchase)
        {
            return new PurchaseResponse(
                purchase.Id,
                purchase.CustomerId,
                PurchaseItemResponse.MapToResponseList(purchase.PurchaseItems));
        }

        public static IEnumerable<PurchaseResponse> MapToResponseList(IEnumerable<Purchase> purchases)
        {
            return purchases.Select(p => MapToResponse(p));
        }
    }
}