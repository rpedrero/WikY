namespace WikY.Repositories.Contracts
{
    public interface IRepository<T, ID>
    {
        Task<T> CreateAsync(T entity);
        Task<T?> GetByIdAsync(ID id);
        IAsyncEnumerable<T> GetAll();
        Task UpdateAsync(T entity);
        Task UpdateAsync(T oldEntity, T newEntity);
        Task DeleteAsync(T entity);
    }
}
