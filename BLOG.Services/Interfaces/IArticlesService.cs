namespace BLOG.Services.Interfaces
{
    using BLOG.Dtos.Article;

    using CSharpFunctionalExtensions;

    using System.Threading.Tasks;

    public interface IArticlesService : IBaseService<ArticleDto, CreateOrUpdateArticleDto>
    {
        Task<Result<AdminPaginationArticlesDto>> ListWithPagination(int pageNumber);

        Task<Result<ArticleDto>> Get(string slug);
    }
}