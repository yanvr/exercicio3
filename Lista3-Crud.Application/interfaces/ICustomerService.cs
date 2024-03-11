using Lista3_Crud.Application.DTOs.Requests;
using Lista3_Crud.Application.DTOs.Responses;

namespace Lista3_Crud.Application.interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponse?> GetById(int id);

        Task<IEnumerable<CustomerResponse>> GetAll();

        Task Add(CreateCustomerRequest request);

        Task Update(UpdateCustomerRequest entity);

        Task Delete(int id);
    }
}