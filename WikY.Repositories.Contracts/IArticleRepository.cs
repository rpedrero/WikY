using WikY.Entities;

namespace WikY.Repositories.Contracts
{
    public interface IArticleRepository : IRepository<Article, int>
    {
        Task<Article?> GetByTopicAsync(string topic);
        Task<Article?> GetLastAsync();
        IAsyncEnumerable<Article> Find(string? topic, string? content, string? author);
        Task<bool> UpdateAsync(int id, string? topic = null, string? content = null, string? author = null, DateTime? dateCreated = null, DateTime? dateModified = null);
    }
}
