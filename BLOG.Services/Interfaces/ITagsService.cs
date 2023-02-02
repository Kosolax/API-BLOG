namespace BLOG.Services.Interfaces
{
    using BLOG.Dtos.Tag;
    using CSharpFunctionalExtensions;

    using System.Threading.Tasks;

    public interface ITagsService : IBaseService<TagDto, CreateOrUpdateTagDto>
    {
        Task<Result<AdminPaginationTagsDto>> ListWithPagination(int pageNumber);
    }
}