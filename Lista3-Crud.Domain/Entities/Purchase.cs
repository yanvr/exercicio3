using Lista3_Crud.Domain.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Lista3_Crud.Domain.Entities
{
    [Table("Purchases")]
    public sealed class Purchase : Entity
    {
        [Column("customer_id")]
        public int CustomerId { get; init; }

        [Column("date")]
        public DateTime Date { get; init; }

        [JsonIgnore]
        private IEnumerable<PurchaseItem> _purchasesItems = [];

        [JsonIgnore]
        public List<PurchaseItem> PurchaseItems => _purchasesItems.ToList();

        private Purchase()
        { }

        public static Purchase CreateInstance(int customerId)
        {
            DomainException
                .ThrowWhen(customerId <= 0, "The customer id must greater than zero");

            return new Purchase
            {
                CustomerId = customerId,
                Date = DateTime.UtcNow,
            };
        }

        public void AddItems(IEnumerable<PurchaseItem> purchaseItems)
        {
            _purchasesItems = purchaseItems;
        }
    }
}