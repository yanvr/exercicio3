using Lista3_Crud.Domain.Entities;
using Lista3_Crud.Domain.interfaces;
using Lista3_Crud.Infra.Data.Connection;
using System.Data;

namespace Lista3_Crud.Infra.Data.Repositories
{
    public class PurchaseItemRepository(ApplicationDbContext context)
        : GenericRepository<PurchaseItem>(context), IPurchaseItemRepository
    {
    }
}