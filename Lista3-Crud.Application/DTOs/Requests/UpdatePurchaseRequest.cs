namespace Lista3_Crud.Application.DTOs.Requests
{
    public record UpdatePurchaseRequest(int Id, int CustomerId, DateTime Date)
    {
    }
}