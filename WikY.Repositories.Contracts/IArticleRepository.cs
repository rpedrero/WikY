﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikY.Entities;

namespace WikY.Repositories.Contracts
{
    public interface IArticleRepository : IRepository<Article, int>
    {
        Task<Article?> GetByTopic(string topic);
        Task<Article?> GetLast();
    }
}
