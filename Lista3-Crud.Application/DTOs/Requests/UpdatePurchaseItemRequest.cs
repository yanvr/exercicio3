namespace Lista3_Crud.Application.DTOs.Requests
{
    public record UpdatePurchaseItemRequest(int Id, int ProductId, int Quantity, decimal Price)
    {
    }
}