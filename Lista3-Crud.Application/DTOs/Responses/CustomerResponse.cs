using Lista3_Crud.Domain.Entities;

namespace Lista3_Crud.Application.DTOs.Responses
{
    public record CustomerResponse(int Id, string Name, string Email, string Cpf)
    {
        public static CustomerResponse MapToResponse(Customer response)
        {
            return new CustomerResponse(
                response.Id,
                response.Name,
                response.Email,
                response.Cpf);
        }

        public static IEnumerable<CustomerResponse> MapToResponseList(IEnumerable<Customer> customers)
        {
            return customers.Select(c => MapToResponse(c));
        }
    }
}