using Lista3_Crud.Domain.Entities;
using Lista3_Crud.Domain.Interfaces;
using Lista3_Crud.Infra.Data.Connection;
using System.Data;

namespace Lista3_Crud.Infra.Data.Repositories
{
    public class ProductRepository(ApplicationDbContext context)
        : GenericRepository<Product>(context), IProductRepository
    {
    }
}