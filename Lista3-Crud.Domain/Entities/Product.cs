using Lista3_Crud.Domain.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lista3_Crud.Domain.Entities
{
    [Table("Products")]
    public sealed class Product : Entity
    {
        [Column("name")]
        public string Name { get; init; } = string.Empty;

        [Column("price")]
        public decimal Price { get; init; }

        [Column("quantity")]
        public int Quantity { get; init; }

        private Product()
        { }

        public static Product CreateInstance(string name, decimal price, int quantity, int? id = null)
        {
            DomainException
                .ThrowWhen(id != null && id <= 0, "The id must be greater than zero");

            DomainException
                .ThrowWhen(string.IsNullOrEmpty(name), "The product name must be valid");

            DomainException
               .ThrowWhen(price <= 0, "The price must be greater than zero");

            DomainException
              .ThrowWhen(quantity <= 0, "The quantity must be greater than zero");

            return new Product
            {
                Id = id.GetValueOrDefault(),
                Name = name,
                Price = price,
                Quantity = quantity
            };
        }
    }
}