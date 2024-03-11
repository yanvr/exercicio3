using Lista3_Crud.Application.DTOs.Requests;
using Lista3_Crud.Application.DTOs.Responses;
using Lista3_Crud.Application.interfaces;
using Lista3_Crud.Domain.Interfaces;

namespace Lista3_Crud.Application.Services
{
    public class PurchaseService(IUnitOfWork uow)
        : IPurchaseService
    {
        private readonly IUnitOfWork _uow = uow;

        public async Task Add(CreatePurchaseRequest request)
        {
            try
            {
                using var transaction = _uow.BeginTransaction();

                var purchase = CreatePurchaseRequest.MapToEntity(request);

                var insertedPurchaseId = await _uow.PurchaseRepository.Add(purchase);

                foreach (var item in purchase.PurchaseItems)
                {
                    item.PurchaseId = insertedPurchaseId;
                    await _uow.PurchaseItemRepository.Add(item);
                }

                _uow.Commit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _uow.Dispose();
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var transaction = _uow.BeginTransaction();

                var purchase = await _uow.PurchaseRepository.GetPurchaseByIdWithItems(id);

                if (purchase == null)
                {
                }

                foreach (var item in purchase!.PurchaseItems)
                {
                    await _uow.PurchaseItemRepository.Delete(item);
                }

                await _uow.PurchaseRepository.Delete(purchase);

                _uow.Commit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _uow.Dispose();
            }
        }

        public async Task<IEnumerable<PurchaseResponse>> GetAll()
        {
            var purchases = await _uow.PurchaseRepository.GetAllPurchasesWithItems();

            if (!purchases.Any()) return [];

            return PurchaseResponse.MapToResponseList(purchases);
        }

        public async Task<PurchaseResponse?> GetById(int id)
        {
            var purchase = await _uow.PurchaseRepository.GetPurchaseByIdWithItems(id);

            if (purchase is null) return null;

            return PurchaseResponse.MapToResponse(purchase);
        }

        public async Task Update(UpdatePurchaseRequest request)
        {
            try
            {
                var transaction = _uow.BeginTransaction();

                var purchase = await _uow.PurchaseRepository.GetPurchaseByIdWithItems(request.Id);

                if (purchase == null)
                {
                }

                foreach (var item in purchase!.PurchaseItems)
                {
                    await _uow.PurchaseItemRepository.Update(item);
                }

                await _uow.PurchaseRepository.Update(purchase);

                _uow.Commit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _uow.Dispose();
            }
        }
    }
}