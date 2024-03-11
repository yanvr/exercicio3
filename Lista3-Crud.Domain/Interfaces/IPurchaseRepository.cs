using Lista3_Crud.Domain.Entities;

namespace Lista3_Crud.Domain.Interfaces
{
    public interface IPurchaseRepository : IGenericRepository<Purchase>
    {
        Task<Purchase?> GetPurchaseByIdWithItems(int id);

        Task<IEnumerable<Purchase>> GetAllPurchasesWithItems();
    }
}