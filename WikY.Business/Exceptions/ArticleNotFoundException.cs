using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikY.Business.Exceptions
{
    public class ArticleNotFoundException : Exception
    {
        public ArticleNotFoundException(int id) : base($"No article with ID {id} has been found.")
        {
        }
    }
}
