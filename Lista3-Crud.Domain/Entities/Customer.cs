using Lista3_Crud.Domain.Exceptions;
using Lista3_Crud.Domain.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Lista3_Crud.Domain.Entities
{
    [Table("Customers")]
    public sealed class Customer : Entity
    {
        [Column("name")]
        public string Name { get; init; } = string.Empty;

        [Column("email")]
        public string Email { get; init; } = string.Empty;

        [Column("cpf")]
        public string Cpf { get; init; } = string.Empty;

        [Column("password")]
        public string Password { get; init; } = string.Empty;

        [JsonIgnore]
        public IEnumerable<Purchase> Purchases { get; init; } = [];

        private Customer()
        { }

        public static Customer CreateInstance(string name, string email, string cpf, string password, int? id = null)
        {
            DomainException
                .ThrowWhen(id != null && id <= 0, "The id must be greater than zero");

            DomainException
                .ThrowWhen(string.IsNullOrEmpty(name), "The name must be valid");

            DomainException
                .ThrowWhen(!Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"), "The email must be valid");

            DomainException
                .ThrowWhen(!CpfValidator.IsValid(cpf), "The cpf must be valid");

            return new Customer
            {
                Id = id.GetValueOrDefault(),
                Name = name,
                Cpf = cpf.Replace(".", "").Replace("-", ""),
                Email = email,
                Password = CriptHandler.Encrypt(password),
            };
        }
    }
}