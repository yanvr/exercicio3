using Lista3_Crud.Domain.Entities;

namespace Lista3_Crud.Application.DTOs.Responses
{
    public record ProductResponse(int Id, string Name, decimal Price, int Quantity)
    {
        public static ProductResponse MapToResponse(Product product)
        {
            return new ProductResponse
            (
                product.Id,
                product.Name,
                product.Price,
                product.Quantity
            );
        }

        public static IEnumerable<ProductResponse> MapToResponseList(IEnumerable<Product> customers)
        {
            return customers.Select(c => MapToResponse(c));
        }
    }
}