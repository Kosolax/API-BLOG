namespace BLOG.Services.Interfaces
{
    using BLOG.Dtos.Image;
    using CSharpFunctionalExtensions;

    public interface IImageService
    {
        Task<List<ImageDto>> GetImagesFromArticleId(int articleId);

        Task<Dictionary<int, ImageDto>> GetDictionaryArticleIdThumbnailFromArticleIds(List<int> articleIds);

        Task<Result> CreateOrUpdateImages(List<CreateOrUpdateImageDto> images, int articleId);

        Task<Result> DeleteImages(int articleId);
    }
}