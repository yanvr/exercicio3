using Lista3_Crud.Domain.Entities;

namespace Lista3_Crud.Application.DTOs.Requests
{
    public record UpdateCustomerRequest(int Id, string Name, string Email, string Cpf, string Password)
    {
        public static Customer MapToEntity(UpdateCustomerRequest request)
        {
            return Customer.CreateInstance(request.Name, request.Email, request.Cpf, request.Password, request.Id);
        }
    }
}