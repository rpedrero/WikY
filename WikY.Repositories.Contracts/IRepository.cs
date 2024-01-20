namespace WikY.Repositories.Contracts
{
    public interface IRepository<T, ID>
    {
        Task<T> CreateAsync(T entity);
        Task<T?> GetByIdAsync(ID id);
        IAsyncEnumerable<T> GetAll();
        Task<bool> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
