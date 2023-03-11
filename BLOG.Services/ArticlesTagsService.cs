﻿namespace BLOG.Services
{
    using BLOG.DataAccess.Interfaces;
    using BLOG.Dtos.Tag;
    using BLOG.Entities;
    using BLOG.Services.Interfaces;

    using CSharpFunctionalExtensions;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ArticlesTagsService : IArticlesTagsService
    {
        private readonly IArticleTagDataAccess _articlesTagsDataAccess;

        private readonly ITagsService _tagsService;

        public ArticlesTagsService(IArticleTagDataAccess articlesTagsDataAccess, ITagsService tagsService)
        {
            this._articlesTagsDataAccess = articlesTagsDataAccess;
            this._tagsService = tagsService;
        }

        public async Task<Result> CreateOrUpdateArticlesTags(List<TagDto> tags, int articleId)
        {
            List<ArticleTagEntity> entities = new List<ArticleTagEntity>();
            Result deleteResult = await this.DeleteArticlesTags(articleId);
            if (deleteResult.IsFailure)
            {
                return Result.Failure("Error while deleting old tags");
            }

            foreach (var tag in tags)
            {
                entities.Add(new ArticleTagEntity
                {
                    ArticleEntityId = articleId,
                    TagEntityId = tag.Id,
                });
            }

            await this._articlesTagsDataAccess.CreateRange(entities);
            return Result.Success();
        }

        public async Task<Result> DeleteArticlesTags(int articleId)
        {
            await this._articlesTagsDataAccess.DeleteFromArticleId(articleId);
            return Result.Success();
        }

        public async Task<Dictionary<int, List<TagDto>>> GetDictionaryArticleIdTagsFromArticleIds(List<int> articleIds)
        {
            Dictionary<int, List<TagDto>> dictionary = new Dictionary<int, List<TagDto>>();

            List<ArticleTagEntity> articleTagEntities = await this._articlesTagsDataAccess.GetArticleTagsFromArticleIds(articleIds);
            List<int> tagIds = articleTagEntities.Select(x => x.TagEntityId).ToList();
            Dictionary<int, TagDto> tagDtos = await this._tagsService.ListFromIds(tagIds);

            foreach (var articleTagEntity in articleTagEntities)
            {
                if (dictionary.ContainsKey(articleTagEntity.ArticleEntityId))
                {
                    dictionary[articleTagEntity.ArticleEntityId].Add(tagDtos[articleTagEntity.TagEntityId]);
                }
                else
                {
                    dictionary.Add(articleTagEntity.ArticleEntityId, new List<TagDto>() { tagDtos[articleTagEntity.TagEntityId] });
                }
            }

            return dictionary;
        }

        public async Task<List<int>> ListArticleWithSearchAndTagsId(string search, List<int> ids)
        {
            return await this._articlesTagsDataAccess.ListArticleWithSearchAndTagsId(search, ids);
        }

        public async Task<List<TagDto>> GetTagsFromArticleId(int articleId)
        {
            List<TagEntity> tagEntities = await this._articlesTagsDataAccess.GetTagsFromArticleId(articleId);
            return this._tagsService.CreateDtos(tagEntities);
        }
    }
}