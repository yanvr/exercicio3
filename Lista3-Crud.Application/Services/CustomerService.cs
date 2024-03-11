using Lista3_Crud.Application.DTOs.Requests;
using Lista3_Crud.Application.DTOs.Responses;
using Lista3_Crud.Application.interfaces;
using Lista3_Crud.Domain.Interfaces;

namespace Lista3_Crud.Application.Services
{
    public class CustomerService(IUnitOfWork uow) : ICustomerService
    {
        private readonly IUnitOfWork _uow = uow;

        public async Task Add(CreateCustomerRequest request)
        {
            var customer = CreateCustomerRequest.MapToEntity(request);
            await _uow.CustomerRepository.Add(customer);
        }

        public async Task Delete(int id)
        {
            var customer = await _uow.CustomerRepository.GetById(id);
            await _uow.CustomerRepository.Delete(customer!);
        }

        public async Task<IEnumerable<CustomerResponse>> GetAll()
        {
            var customers = await _uow.CustomerRepository.GetAll();

            if (customers is null) return [];

            return CustomerResponse.MapToResponseList(customers);
        }

        public async Task<CustomerResponse?> GetById(int id)
        {
            var customer = await _uow.CustomerRepository.GetById(id);

            if (customer is null) return null;

            return CustomerResponse.MapToResponse(customer!);
        }

        public async Task Update(UpdateCustomerRequest request)
        {
            var customer = UpdateCustomerRequest.MapToEntity(request);
            await _uow.CustomerRepository.Update(customer);
        }
    }
}