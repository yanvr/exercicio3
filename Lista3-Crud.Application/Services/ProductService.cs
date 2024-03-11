using Lista3_Crud.Application.DTOs.Requests;
using Lista3_Crud.Application.DTOs.Responses;
using Lista3_Crud.Application.interfaces;
using Lista3_Crud.Domain.Interfaces;

namespace Lista3_Crud.Application.Services
{
    public class ProductService(IUnitOfWork uow) : IProductService
    {
        private readonly IUnitOfWork _uow = uow;

        public async Task Add(CreateProductRequest request)
        {
            var product = CreateProductRequest.MapToEntity(request);
            await _uow.ProductRepository.Add(product);
        }

        public async Task Delete(int id)
        {
            var product = await _uow.ProductRepository.GetById(id);
            await _uow.ProductRepository.Delete(product!);
        }

        public async Task<IEnumerable<ProductResponse>> GetAll()
        {
            var products = await _uow.ProductRepository.GetAll();

            if (products == null) return [];

            return ProductResponse.MapToResponseList(products);
        }

        public async Task<ProductResponse?> GetById(int id)
        {
            var product = await _uow.ProductRepository.GetById(id);

            if (product == null) return null;

            return ProductResponse.MapToResponse(product!);
        }

        public async Task Update(UpdateProductRequest request)
        {
            var product = UpdateProductRequest.MapToEntity(request);
            await _uow.ProductRepository.Update(product);
        }
    }
}