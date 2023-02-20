namespace BLOG.Services
{
    using BLOG.DataAccess.Interfaces;
    using BLOG.Dtos.Article;
    using BLOG.Dtos.Tag;
    using BLOG.Entities;
    using BLOG.Services.Handlers;
    using BLOG.Services.Interfaces;

    using CSharpFunctionalExtensions;
    using Microsoft.Extensions.Configuration;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ArticlesService : IArticlesService
    {
        private readonly IArticleDataAccess _dataAccess;

        private readonly IArticlesTagsService _articlesTagsService;

        private readonly ITagsService _tagsService;

        private readonly PaginationHandler _paginationHandler;

        private readonly IConfigurationSection _paginationConfiguration;

        private readonly IConfiguration _configuration;

        private int _articlePerPage;

        private int _descriptionSize;

        public ArticlesService(ITagsService tagsService, IArticleDataAccess dataAccess, PaginationHandler paginationHandler, IConfiguration configuration, IArticlesTagsService articlesTagsService)
        {
            this._dataAccess = dataAccess;
            this._tagsService = tagsService;
            this._paginationHandler = paginationHandler;
            this._configuration = configuration;
            this._articlesTagsService = articlesTagsService;
            this._paginationConfiguration = this._configuration.GetSection("PaginationSettings");
            this._articlePerPage = Convert.ToInt32(this._paginationConfiguration["articlePerPage"]);
            this._descriptionSize = Convert.ToInt32(this._paginationConfiguration["descriptionSize"]);
        }

        public async Task<Result<ArticleDto>> Create(CreateOrUpdateArticleDto itemToCreate)
        {
            if (itemToCreate == null)
            {
                return Result.Failure<ArticleDto>("Could not map dto into entity");
            }

            if (itemToCreate.Tags == null || itemToCreate.Tags.Count == 0)
            {
                return Result.Failure<ArticleDto>("Article must have tags.");
            }

            bool areTagsLegit = await this._tagsService.DoListExist(itemToCreate.Tags.Select(x => x.Id).ToList());
            if (!areTagsLegit)
            {
                return Result.Failure<ArticleDto>("You nasty hacker.");
            }

            Result<ArticleDto> resultGetExistingArticle = await this.Get(itemToCreate.Slug);

            if (resultGetExistingArticle.IsSuccess)
            {
                return Result.Failure<ArticleDto>("Impossible de possèder un slug dupliqué.");
            }

            ArticleEntity entityToCreate = itemToCreate.CreateEntity();

            entityToCreate.CreatedDate = DateTime.Now;

            this.SetDescription(entityToCreate);

            ArticleEntity createdEntity = await this._dataAccess.Create(entityToCreate);
            if (createdEntity == null)
            {
                return Result.Failure<ArticleDto>("Could not create the entity");
            }

            Result articlesTagsResult = await this._articlesTagsService.CreateOrUpdateArticlesTags(itemToCreate.Tags, createdEntity.Id);
            if (articlesTagsResult.IsFailure)
            {
                Result deleteArticlesTagsResult = await this._articlesTagsService.DeleteArticlesTags(createdEntity.Id);
                if (deleteArticlesTagsResult.IsFailure)
                {
                    return Result.Failure<ArticleDto>("Error while creating");
                }

                await this._dataAccess.Delete(createdEntity.Id);
            }

            Result<ArticleDto> result = await this.Get(createdEntity.Slug);
            if (result.IsFailure)
            {
                return Result.Failure<ArticleDto>("Error while fetching new item");
            }

            ArticleDto dto = result.Value;

            if (dto == null)
            {
                return Result.Failure<ArticleDto>("Could not map the new dto from entity");
            }

            return Result.Success(dto);
        }

        public async Task Delete(int id)
        {
            ArticleEntity oldEntity = await this._dataAccess.Find(id);
            if (oldEntity == null)
            {
                Result.Failure("Could not find the entity you want to delete");
                return;
            }

            ArticleEntity newEntity = oldEntity;
            newEntity.IsDeleted = true;

            await this._dataAccess.Update(newEntity, oldEntity);
            Result.Success();
        }

        public async Task<Result<ArticleDto>> Get(string slug)
        {
            ArticleEntity? entity = await this._dataAccess.Get(slug);
            if (entity == null)
            {
                return Result.Failure<ArticleDto>("Could not fetch entity");
            }

            ArticleDto dto = await this.CreateDto(entity);
            if (dto == null)
            {
                return Result.Failure<ArticleDto>("Could not map entity");
            }

            return Result.Success(dto);
        }

        public Task<Result<ArticleDto>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<ArticleDto>>> List()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<AdminPaginationArticlesDto>> ListWithPagination(int pageNumber)
        {
            int countTags = await this._dataAccess.Count();
            int totalPages = this._paginationHandler.GetTotalPages(countTags, this._articlePerPage);
            pageNumber = this._paginationHandler.GetPageNumber(pageNumber, totalPages);

            List<ArticleEntity> entities = await this._dataAccess.ListSkipTake((pageNumber - 1) * this._articlePerPage, this._articlePerPage);
            if (entities == null)
            {
                return Result.Failure<AdminPaginationArticlesDto>("Could not fetch entitites");
            }

            List<LightAdminArticleDto> dtos = await this.CreateLightAdminDtos(entities);
            if (dtos == null)
            {
                return Result.Failure<AdminPaginationArticlesDto>("Could not map entitites into dtos");
            }

            AdminPaginationArticlesDto paginatedTagsDto = new AdminPaginationArticlesDto()
            {
                CurrentPage = pageNumber,
                Items = dtos,
                TotalItems = countTags,
                TotalPages = totalPages,
            };

            return Result.Success(paginatedTagsDto);
        }

        public async Task<Result<ArticleDto>> Update(CreateOrUpdateArticleDto itemToUpdate, int id)
        {
            if (itemToUpdate == null)
            {
                return Result.Failure<ArticleDto>("Could not map dto into entity");
            }

            if (itemToUpdate.Tags == null || itemToUpdate.Tags.Count == 0)
            {
                return Result.Failure<ArticleDto>("Article must have tags.");
            }

            bool areTagsLegit = await this._tagsService.DoListExist(itemToUpdate.Tags.Select(x => x.Id).ToList());
            if (!areTagsLegit)
            {
                return Result.Failure<ArticleDto>("You nasty hacker.");
            }

            Result<ArticleDto> resultGetExistingArticle = await this.Get(itemToUpdate.Slug);

            if (resultGetExistingArticle.IsSuccess && resultGetExistingArticle.Value.Id != id)
            {
                return Result.Failure<ArticleDto>("Impossible de possèder un slug dupliqué.");
            }

            ArticleEntity entityToUpdate = itemToUpdate.CreateEntity();

            entityToUpdate.UpdatedDate = DateTime.Now;

            this.SetDescription(entityToUpdate);

            ArticleEntity oldEntity = await this._dataAccess.Find(id);
            if (oldEntity == null)
            {
                return Result.Failure<ArticleDto>("Could not find the entity you want to update");
            }

            entityToUpdate.Id = id;
            entityToUpdate.CreatedDate = oldEntity.CreatedDate;
            entityToUpdate.Views = oldEntity.Views;
            ArticleEntity updatedEntity = await this._dataAccess.Update(entityToUpdate, oldEntity);
            if (updatedEntity == null)
            {
                return Result.Failure<ArticleDto>("Could not update the entity");
            }

            Result articlesTagsResult = await this._articlesTagsService.CreateOrUpdateArticlesTags(itemToUpdate.Tags, updatedEntity.Id);
            if (articlesTagsResult.IsFailure)
            {
                Result deleteArticlesTagsResult = await this._articlesTagsService.DeleteArticlesTags(updatedEntity.Id);
                if (deleteArticlesTagsResult.IsFailure)
                {
                    return Result.Failure<ArticleDto>("Error while creating");
                }

                await this._dataAccess.Delete(updatedEntity.Id);
            }

            Result<ArticleDto> result = await this.Get(updatedEntity.Slug);
            if (result.IsFailure)
            {
                return Result.Failure<ArticleDto>("Error while fetching new item");
            }

            ArticleDto dto = result.Value;

            if (dto == null)
            {
                return Result.Failure<ArticleDto>("Could not map the new dto from entity");
            }

            return Result.Success(dto);
        }

        private async Task<ArticleDto> CreateDto(ArticleEntity entity)
        {
            ArticleDto articleDto = new ArticleDto()
            {
                Id = entity.Id,
                Content = entity.Content,
                CreatedDate = entity.CreatedDate,
                Description = entity.Description,
                Slug = entity.Slug,
                Thumbnail = entity.Thumbnail,
                Title = entity.Title,
                UpdatedDate = entity.UpdatedDate,
                Views = entity.Views,
            };

            articleDto.Tags = await this._articlesTagsService.GetTagsFromArticleId(articleDto.Id);
            return articleDto;
        }

        private async Task<List<LightAdminArticleDto>> CreateLightAdminDtos(List<ArticleEntity> entities)
        {
            List<LightAdminArticleDto> lightAdminArticleDtos = new List<LightAdminArticleDto>();

            List<int> articleIds = entities.Select(x => x.Id).ToList();
            Dictionary<int, List<TagDto>> dictionary = await this._articlesTagsService.GetDictionaryArticleIdTagsFromArticleIds(articleIds);

            for (int i = 0; i < entities.Count; i++)
            {
                ArticleEntity articleEntity = entities[i];
                lightAdminArticleDtos.Add(new LightAdminArticleDto()
                {
                    Tags = dictionary[articleEntity.Id],
                    Title = articleEntity.Title,
                    Views = articleEntity.Views,
                });
            }

            return lightAdminArticleDtos;
        }

        private void SetDescription(ArticleEntity entity)
        {
            if (!string.IsNullOrEmpty(entity.Content))
            {
                int descriptionSize = this._descriptionSize;

                if (entity.Content.Length < this._descriptionSize)
                {
                    descriptionSize = entity.Content.Length;
                }

                entity.Description = entity.Content[..descriptionSize];
                entity.Description += " ...";
            }
        }
    }
}