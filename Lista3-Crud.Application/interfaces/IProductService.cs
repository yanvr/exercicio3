using Lista3_Crud.Application.DTOs.Requests;
using Lista3_Crud.Application.DTOs.Responses;

namespace Lista3_Crud.Application.interfaces
{
    public interface IProductService
    {
        Task<ProductResponse?> GetById(int id);

        Task<IEnumerable<ProductResponse>> GetAll();

        Task Add(CreateProductRequest request);

        Task Update(UpdateProductRequest entity);

        Task Delete(int id);
    }
}