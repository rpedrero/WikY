namespace WikY.Models.Article
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Topic { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
