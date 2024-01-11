namespace WikY.Repositories.Contracts
{
    public interface IRepository<T, ID>
    {
        public Task<T> Create(T entity);
        public Task<T?> GetById(ID id);
        public Task<IAsyncEnumerable<T>> GetAll();
        public Task Update(T entity);
        public Task Delete(T entity);
    }
}
