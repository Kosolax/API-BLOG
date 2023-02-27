namespace BLOG.DataAccess.Interfaces
{
    using BLOG.Entities;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IImageDataAccess : IBaseDataAccess<ImageEntity>
    {
        Task CreateRange(List<ImageEntity> entities);

        Task DeleteFromArticleId(int articleId);

        Task<List<ImageEntity>> GetImagesFromArticleId(int articleId);
    }
}