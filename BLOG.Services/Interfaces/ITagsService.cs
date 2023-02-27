namespace BLOG.Services.Interfaces
{
    using BLOG.Dtos.Tag;
    using BLOG.Entities;

    using CSharpFunctionalExtensions;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITagsService : IBaseService<TagDto, CreateOrUpdateTagDto>
    {
        Task<bool> DoListExist(List<int> list);

        Task<Result<AdminPaginationTagsDto>> ListWithPagination(int pageNumber);

        List<TagDto> CreateDtos(List<TagEntity> tagEntities);

        Task<Dictionary<int, TagDto>> ListFromIds(List<int> ids);
    }
}