namespace BLOG.DataAccess
{
    using BLOG.DataAccess.Interfaces;
    using BLOG.Entities;

    using Microsoft.EntityFrameworkCore;

    public class ArticleTagDataAccess : BaseDataAccess<ArticleTagEntity>, IArticleTagDataAccess
    {
        public ArticleTagDataAccess(BlogContext context) : base(context)
        {
        }

        public async Task<List<TagEntity>> GetTagsFromArticleId(int articleId)
        {
            return await this.Context.ArticlesTags.Include(x => x.TagEntity).Where(x => x.ArticleEntityId == articleId).Select(x => x.TagEntity).ToListAsync();
        }

        public async Task<List<ArticleTagEntity>> GetArticleTagsFromArticleIds(List<int> articleIds)
        {
            return await this.Context.ArticlesTags.Where(x => articleIds.Contains(x.ArticleEntityId)).ToListAsync();
        }

        public async Task<List<ArticleTagEntity>> CreateRange(List<ArticleTagEntity> articleTags)
        {
            await this.Context.ArticlesTags.AddRangeAsync(articleTags);
            await this.Context.SaveChangesAsync();

            return articleTags;
        }

        public async Task DeleteFromArticleId(int articleId)
        {
            List<ArticleTagEntity> articleTagEntities = await this.Context.ArticlesTags.Where(x => x.ArticleEntityId == articleId).ToListAsync();
            if (articleTagEntities != null)
            {
                this.Context.ArticlesTags.RemoveRange(articleTagEntities);
                await this.Context.SaveChangesAsync();
            }
        }

        public async Task<List<int>> ListArticleWithSearchAndTagsId(string search, List<int> ids)
        {
            List<int> articleKept = new List<int>();
            Dictionary<int, List<int>> dictionaryArticleIdTagIds = await this.Context.ArticlesTags
                .Include(x => x.ArticleEntity)
                .Where(x => x.ArticleEntity != null &&
                            !x.ArticleEntity.IsDeleted &&
                            x.ArticleEntity.Title != null &&
                            x.ArticleEntity.Description != null &&
                            (search == null || (search != null && (x.ArticleEntity.Title.Contains(search) || x.ArticleEntity.Description.Contains(search))))
                )
                .GroupBy(x => x.ArticleEntityId)
                .ToDictionaryAsync(x => x.Key, x => x.Select(x => x.TagEntityId).ToList());

            foreach (var keypair in dictionaryArticleIdTagIds)
            {
                bool containsAllTags = true;
                for (int j = 0; j < ids.Count; j++)
                {
                    if (!dictionaryArticleIdTagIds[keypair.Key].Contains(ids[j]))
                    {
                        containsAllTags = false;
                    }
                }

                if (containsAllTags)
                {
                    articleKept.Add(keypair.Key);

                }
            }

            return articleKept;
        }
    }
}