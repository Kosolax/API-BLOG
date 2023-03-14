namespace BLOG.DataAccess.Interfaces
{
    using BLOG.Entities;

    public interface IArticleTagDataAccess : IBaseDataAccess<ArticleTagEntity>
    {
        Task<List<TagEntity>> GetTagsFromArticleId(int articleId);

        Task<List<ArticleTagEntity>> GetArticleTagsFromArticleIds(List<int> articleIds);

        Task<List<ArticleTagEntity>> CreateRange(List<ArticleTagEntity> articleTags);

        Task DeleteFromArticleId(int articleId);

        Task<List<int>> ListArticleWithSearchAndTagsId(string search, List<int> ids);
    }
}