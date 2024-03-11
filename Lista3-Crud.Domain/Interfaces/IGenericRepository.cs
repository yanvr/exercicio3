using Lista3_Crud.Domain.Entities;

namespace Lista3_Crud.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<T?> GetById(int id);

        Task<IEnumerable<T>> GetAll();

        Task<int> Add(T entity);

        Task Update(T entity);

        Task Delete(T entity);
    }
}