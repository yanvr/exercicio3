using Lista3_Crud.Domain.Entities;

namespace Lista3_Crud.Application.DTOs.Requests
{
    public record CreateCustomerRequest(string Name, string Email, string Cpf, string Password)
    {
        public static Customer MapToEntity(CreateCustomerRequest request)
        {
            return Customer.CreateInstance(request.Name, request.Email, request.Cpf, request.Password);
        }
    }
}