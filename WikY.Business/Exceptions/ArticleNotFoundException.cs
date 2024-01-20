namespace WikY.Business.Exceptions
{
    public class ArticleNotFoundException : Exception
    {
        public ArticleNotFoundException(int id) : base($"No article with ID {id} has been found.")
        {
        }
    }
}
