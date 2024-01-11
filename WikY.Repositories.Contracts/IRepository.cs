namespace WikY.Repositories.Contracts
{
    public interface IRepository<T, ID>
    {
        public T Create(T entity);
        public T GetById(ID id);
        public ICollection<T> GetAll();
        public void Update(T entity);
        public void Delete(T entity);
    }
}
