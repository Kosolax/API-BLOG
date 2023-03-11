namespace BLOG.DataAccess.Interfaces
{
    using BLOG.Entities;

    using System.Collections.Generic;

    public interface IArticleDataAccess : IBaseDataAccess<ArticleEntity>
    {
        Task<ArticleEntity?> Get(string slug);

        Task<List<ArticleEntity>> ListFromIdsSkipTake(List<int> ids, int skip, int take);
    }
}