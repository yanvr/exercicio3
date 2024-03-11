using Lista3_Crud.Domain.Entities;

namespace Lista3_Crud.Application.DTOs.Requests
{
    public record CreateProductRequest(string Name, decimal Price, int Quantity)
    {
        public static Product MapToEntity(CreateProductRequest request)
        {
            return Product.CreateInstance(request.Name, request.Price, request.Quantity);
        }
    }
}