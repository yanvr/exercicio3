using Lista3_Crud.Domain.Entities;
using Lista3_Crud.Domain.interfaces;
using Lista3_Crud.Domain.Interfaces;
using Lista3_Crud.Infra.Data.Connection;
using System.Data;

namespace Lista3_Crud.Infra.Data.Repositories
{
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ICustomerRepository? _customerRepository;
        private readonly IProductRepository? _productRepository;
        private readonly IPurchaseRepository? _purchaseRepository;
        private readonly IPurchaseItemRepository? _purchaseItemRepository;

        public ICustomerRepository CustomerRepository
            => _customerRepository ?? new CustomerRepository(_context);

        public IProductRepository ProductRepository
            => _productRepository ?? new ProductRepository(_context);

        public IPurchaseRepository PurchaseRepository
            => _purchaseRepository ?? new PurchaseRepository(_context);

        public IPurchaseItemRepository PurchaseItemRepository
            => _purchaseItemRepository ?? new PurchaseItemRepository(_context);

        public void Commit()
        {
            _context.SaveChanges();
        }

        #region IDisposable Support

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose() => Dispose(true);

        #endregion IDisposable Support

        public IDbTransaction BeginTransaction()
        {
            return _context.BeginTransaction();
        }
    }
}