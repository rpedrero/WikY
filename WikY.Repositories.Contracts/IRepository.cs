namespace WikY.Repositories.Contracts
{
    public interface IRepository<T, ID>
    {
        Task<T> Create(T entity);
        Task<T?> GetById(ID id);
        IAsyncEnumerable<T> GetAll();
        Task Update(T entity);
        Task Update(T oldEntity, T newEntity);
        Task Delete(T entity);
    }
}
