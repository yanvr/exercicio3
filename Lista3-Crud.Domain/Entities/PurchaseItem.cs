using Lista3_Crud.Domain.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lista3_Crud.Domain.Entities
{
    [Table("PurchaseItems")]
    public sealed class PurchaseItem : Entity
    {
        [Column("product_id")]
        public int ProductId { get; init; }

        [Column("purchase_id")]
        public int PurchaseId { get; set; }

        [Column("quantity")]
        public int Quantity { get; init; }

        [Column("price")]
        public decimal Price { get; init; }

        private Product? _product;

        public Product? Product => _product;

        private PurchaseItem()
        { }

        public static PurchaseItem CreateInstance(int productId, int purchaseId, int quantity, decimal price, int? id = null)
        {
            DomainException
                .ThrowWhen(id != null && id <= 0, "The id must greater than zero");

            DomainException
                .ThrowWhen(productId <= 0, "The product id must greater than zero");

            DomainException
                .ThrowWhen(purchaseId <= 0 && id != null, "The purchase id must greater than zero");

            DomainException
                .ThrowWhen(quantity <= 0, "The quantity must greater than zero");

            DomainException
                .ThrowWhen(price <= 0, "The price must greater than zero");

            return new PurchaseItem
            {
                ProductId = productId,
                PurchaseId = purchaseId,
                Quantity = quantity,
                Price = price,
            };
        }

        public void AddProduct(Product product)
        {
            if (product is not null)
                _product = product;
        }
    }
}