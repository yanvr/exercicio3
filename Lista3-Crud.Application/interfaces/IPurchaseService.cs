using Lista3_Crud.Application.DTOs.Requests;
using Lista3_Crud.Application.DTOs.Responses;

namespace Lista3_Crud.Application.interfaces
{
    public interface IPurchaseService
    {
        Task<PurchaseResponse?> GetById(int id);

        Task<IEnumerable<PurchaseResponse>> GetAll();

        Task Add(CreatePurchaseRequest request);

        Task Update(UpdatePurchaseRequest request);

        Task Delete(int id);
    }
}