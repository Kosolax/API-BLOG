namespace BLOG.Services.Interfaces
{
    using BLOG.Dtos.Tag;

    using CSharpFunctionalExtensions;

    public interface IArticlesTagsService
    {
        Task<List<TagDto>> GetTagsFromArticleId(int articleId);

        Task<Dictionary<int, List<TagDto>>> GetDictionaryArticleIdTagsFromArticleIds(List<int> articleIds);

        Task<Result> CreateOrUpdateArticlesTags(List<TagDto> tags, int articleId);

        Task<Result> DeleteArticlesTags(int articleId);
    }
}