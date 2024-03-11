using Lista3_Crud.Domain.Entities;
using Lista3_Crud.Domain.interfaces;
using System.Data;

namespace Lista3_Crud.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        IProductRepository ProductRepository { get; }
        IPurchaseRepository PurchaseRepository { get; }
        IPurchaseItemRepository PurchaseItemRepository { get; }

        IDbTransaction BeginTransaction();

        void Commit();
    }
}