using System.ComponentModel.DataAnnotations;

namespace Lista3_Crud.Domain.Entities
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; init; }
    }
}