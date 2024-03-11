using Dapper;
using Lista3_Crud.Domain.Entities;
using Lista3_Crud.Domain.Interfaces;
using Lista3_Crud.Infra.Data.Connection;

namespace Lista3_Crud.Infra.Data.Repositories
{
    public class PurchaseRepository(ApplicationDbContext context)
        : GenericRepository<Purchase>(context), IPurchaseRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Purchase>> GetAllPurchasesWithItems()
        {
            var query = @"SELECT FROM Purchase p
                        INNER JOIN PurchaseItem pi ON p.id = pi.purchase_id";

            return await _context.Connection.QueryAsync<Purchase>(query);
        }

        public async Task<Purchase?> GetPurchaseByIdWithItems(int id)
        {
            try
            {
                var query = @"SELECT p.id, p.date, p.customer_id AS CustomerId,
                            pi.id, pi.quantity, pi.price, pi.purchase_id AS PurchaseId, pi.product_id AS ProductId,
                            pr.id, pr.name, pr.price, pr.quantity
                            FROM Purchases p
                            INNER JOIN PurchaseItems pi ON p.id = pi.purchase_id
                            INNER JOIN Products pr ON pi.product_id = pr.id
                            WHERE p.id = @Id";

                var purchase = await _context.Connection.QueryAsync<Purchase, PurchaseItem, Product, Purchase>(
                    query,
                    (purchase, purchaseItem, product) =>
                    {
                        Console.WriteLine(purchaseItem);
                        purchaseItem.AddProduct(product);
                        purchase.PurchaseItems.Add(purchaseItem);
                        return purchase;
                    },
                    new { id },
                    splitOn: "PurchaseId, ProductId"
                   );

                return purchase.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}