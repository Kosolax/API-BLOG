namespace BLOG.Services
{
    using BLOG.DataAccess.Interfaces;
    using BLOG.Dtos.Image;
    using BLOG.Entities;
    using BLOG.Services.Interfaces;

    using CSharpFunctionalExtensions;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ImageService : IImageService
    {
        private readonly IImageDataAccess _imageDataAccess;

        public ImageService(IImageDataAccess imageDataAccess)
        {
            this._imageDataAccess = imageDataAccess;
        }

        public async Task<Result> CreateOrUpdateImages(List<CreateOrUpdateImageDto> images, int articleId)
        {
            List<ImageEntity> entities = new List<ImageEntity>();
            Result deleteResult = await this.DeleteImages(articleId);
            if (deleteResult.IsFailure)
            {
                return Result.Failure("Error while deleting old images");
            }

            foreach (var imageDto in images)
            {
                ImageEntity imageEntity = imageDto.CreateEntity();
                imageEntity.Id = default;
                imageEntity.ArticleEntityId = articleId;
                entities.Add(imageEntity);
            }

            await this._imageDataAccess.CreateRange(entities);
            return Result.Success();
        }

        public async Task<Result> DeleteImages(int articleId)
        {
            await this._imageDataAccess.DeleteFromArticleId(articleId);
            return Result.Success();
        }

        public async Task<List<ImageDto>> GetImagesFromArticleId(int articleId)
        {
            List<ImageEntity> imageEntities = await this._imageDataAccess.GetImagesFromArticleId(articleId);
            return this.CreateDtos(imageEntities);
        }

        private List<ImageDto> CreateDtos(List<ImageEntity> images)
        {
            List<ImageDto> imageDtos = new List<ImageDto>();

            foreach (var imageEntity in images)
            {
                ImageDto imageDto = this.CreateDto(imageEntity);
                imageDtos.Add(imageDto);
            }

            return imageDtos;
        }

        private ImageDto CreateDto(ImageEntity imageEntity)
        {
            return new ImageDto()
            {
                Id = imageEntity.Id,
                Base64Image = imageEntity.Base64Image,
                IsThumbnail = imageEntity.IsThumbnail,
                Placeholder = imageEntity.Placeholder,
                ArticleId = imageEntity.ArticleEntityId,
            };
        }

        public async Task<Dictionary<int, ImageDto>> GetDictionaryArticleIdThumbnailFromArticleIds(List<int> articleIds)
        {
            List<ImageEntity> thumbnailEntities = await this._imageDataAccess.ListThumbnailsFromArticleIds(articleIds);
            List<ImageDto> thumbnails = this.CreateDtos(thumbnailEntities);

            return thumbnails.ToDictionary(x => x.ArticleId);
        }
    }
}