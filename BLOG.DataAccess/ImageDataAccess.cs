namespace BLOG.DataAccess
{
    using BLOG.DataAccess.Interfaces;
    using BLOG.Entities;
    using Microsoft.EntityFrameworkCore;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ImageDataAccess : BaseDataAccess<ImageEntity>, IImageDataAccess
    {
        public ImageDataAccess(BlogContext context) : base(context)
        {
        }

        public async Task CreateRange(List<ImageEntity> entities)
        {
            await this.Context.Images.AddRangeAsync(entities);
            await this.Context.SaveChangesAsync();
        }

        public async Task DeleteFromArticleId(int articleId)
        {
            List<ImageEntity> imageEntities = await this.Context.Images.Where(x => x.ArticleEntityId == articleId).ToListAsync();
            if (imageEntities != null)
            {
                this.Context.Images.RemoveRange(imageEntities);
                await this.Context.SaveChangesAsync();
            }
        }

        public async Task<List<ImageEntity>> GetImagesFromArticleId(int articleId)
        {
            return await this.Context.Images.Where(x => x.ArticleEntityId == articleId).ToListAsync();
        }

        public async Task<List<ImageEntity>> ListThumbnailsFromArticleIds(List<int> articleIds)
        {
            return await this.Context.Images.Where(x => articleIds.Contains(x.ArticleEntityId) && x.IsThumbnail).ToListAsync();
        }
    }
}