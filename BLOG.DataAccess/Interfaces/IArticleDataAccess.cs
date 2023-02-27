namespace BLOG.DataAccess.Interfaces
{
    using BLOG.Entities;

    public interface IArticleDataAccess : IBaseDataAccess<ArticleEntity>
    {
        Task<ArticleEntity?> Get(string slug);
    }
}