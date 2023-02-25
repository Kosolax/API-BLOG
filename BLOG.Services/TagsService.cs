namespace BLOG.Services
{
    using BLOG.DataAccess.Interfaces;
    using BLOG.Dtos.Tag;
    using BLOG.Entities;
    using BLOG.Services.Handlers;
    using BLOG.Services.Interfaces;

    using CSharpFunctionalExtensions;

    using Microsoft.Extensions.Configuration;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TagsService : IBaseService<TagDto, CreateOrUpdateTagDto>, ITagsService
    {
        private readonly IConfiguration _configuration;

        private readonly IConfigurationSection _paginationConfiguration;

        private readonly ITagDataAccess _dataAccess;

        private readonly PaginationHandler _paginationHandler;

        private int _tagPerPage;

        public TagsService(PaginationHandler paginationHandler, IConfiguration configuration, ITagDataAccess dataAccess)
        {
            this._configuration = configuration;
            this._paginationConfiguration = this._configuration.GetSection("PaginationSettings");
            this._tagPerPage = Convert.ToInt32(this._paginationConfiguration["tagPerPage"]);
            this._dataAccess = dataAccess;
            this._paginationHandler = paginationHandler;
        }

        public async Task<Result<TagDto>> Create(CreateOrUpdateTagDto itemToCreate)
        {
            if (itemToCreate == null)
            {
                return Result.Failure<TagDto>("Could not map dto into entity");
            }

            TagEntity entityToCreate = itemToCreate.CreateEntity();

            TagEntity createdEntity = await this._dataAccess.Create(entityToCreate);
            if (createdEntity == null)
            {
                return Result.Failure<TagDto>("Could not create the entity");
            }

            TagDto dto = this.CreateDto(createdEntity);
            if (dto == null)
            {
                return Result.Failure<TagDto>("Could not map the new dto from entity");
            }

            return Result.Success(dto);
        }

        public async Task Delete(int id)
        {
            TagEntity oldEntity = await this._dataAccess.Find(id);
            if (oldEntity == null)
            {
                Result.Failure("Could not find the entity you want to delete");
                return;
            }

            TagEntity newEntity = oldEntity;
            newEntity.IsDeleted = true;

            await this._dataAccess.Update(newEntity, oldEntity);
            Result.Success();
        }

        public async Task<bool> DoListExist(List<int> list)
        {
            return await this._dataAccess.DoListExist(list);
        }

        public async Task<Result<TagDto>> Get(int id)
        {
            TagEntity entity = await this._dataAccess.Find(id);
            if (entity == null)
            {
                return Result.Failure<TagDto>("Could not fetch entity");
            }

            TagDto dto = this.CreateDto(entity);
            if (dto == null)
            {
                return Result.Failure<TagDto>("Could not map entity");
            }

            return Result.Success(dto);
        }

        public async Task<Result<List<TagDto>>> List()
        {
            List<TagEntity> entities = await this._dataAccess.List();
            if (entities == null)
            {
                return Result.Failure<List<TagDto>>("Could not fetch entitites");
            }

            List<TagDto> dtos = this.CreateDtos(entities);
            if (dtos == null)
            {
                return Result.Failure<List<TagDto>>("Could not map entitites into dtos");
            }

            return Result.Success(dtos);
        }

        public async Task<Result<AdminPaginationTagsDto>> ListWithPagination(int pageNumber)
        {
            int countTags = await this._dataAccess.Count();
            int totalPages = this._paginationHandler.GetTotalPages(countTags, this._tagPerPage);
            pageNumber = this._paginationHandler.GetPageNumber(pageNumber, totalPages);

            List<TagEntity> entities = await this._dataAccess.ListSkipTake((pageNumber - 1) * this._tagPerPage, this._tagPerPage);
            if (entities == null)
            {
                return Result.Failure<AdminPaginationTagsDto>("Could not fetch entitites");
            }

            List<TagDto> dtos = this.CreateDtos(entities);
            if (dtos == null)
            {
                return Result.Failure<AdminPaginationTagsDto>("Could not map entitites into dtos");
            }

            AdminPaginationTagsDto paginatedTagsDto = new AdminPaginationTagsDto()
            {
                CurrentPage = pageNumber,
                Items = dtos,
                TotalItems = countTags,
                TotalPages = totalPages,
            };

            return Result.Success(paginatedTagsDto);
        }

        public async Task<Result<TagDto>> Update(CreateOrUpdateTagDto itemToUpdate, int id)
        {
            if (itemToUpdate == null)
            {
                return Result.Failure<TagDto>("Could not map dto into entity");
            }

            TagEntity entityToUpdate = itemToUpdate.CreateEntity();

            TagEntity oldEntity = await this._dataAccess.Find(id);
            if (oldEntity == null)
            {
                return Result.Failure<TagDto>("Could not find the entity you want to update");
            }

            entityToUpdate.Id = id;

            TagEntity updatedEntity = await this._dataAccess.Update(entityToUpdate, oldEntity);
            if (updatedEntity == null)
            {
                return Result.Failure<TagDto>("Could not update the entity");
            }

            TagDto dto = this.CreateDto(updatedEntity);
            if (dto == null)
            {
                return Result.Failure<TagDto>("Could not map the new dto from entity");
            }

            return Result.Success(dto);
        }

        public List<TagDto> CreateDtos(List<TagEntity?> tagEntities)
        {
            List<TagDto> tags = new List<TagDto>();

            for (int i = 0; i < tagEntities.Count; i++)
            {
                TagEntity tagEntity = tagEntities[i];
                tags.Add(new TagDto()
                {
                    Id = tagEntity.Id,
                    Name = tagEntity.Name,
                });
            }

            return tags;
        }

        private TagDto CreateDto(TagEntity tagEntity)
        {
            return new TagDto()
            {
                Id = tagEntity.Id,
                Name = tagEntity.Name,
            };
        }

        public async Task<Dictionary<int, TagDto>> ListFromIds(List<int> ids)
        {
            return this.CreateDtos(await this._dataAccess.ListFromIds(ids)).ToDictionary(x => x.Id);
        }
    }
}