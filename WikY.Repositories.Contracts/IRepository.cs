namespace WikY.Repositories.Contracts
{
    public interface IRepository<T, ID>
    {
        public Task<T> Create(T entity);
        public Task<T?> GetById(ID id);
        public IAsyncEnumerable<T> GetAll();
        public Task Update(T entity);
        public Task Update(T oldEntity, T newEntity);
        public Task Delete(T entity);
    }
}
