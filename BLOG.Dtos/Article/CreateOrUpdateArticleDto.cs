namespace BLOG.Dtos.Article
{
    using BLOG.Dtos.Image;
    using BLOG.Dtos.Tag;
    using BLOG.Entities;

    using System.Collections.Generic;

    public class CreateOrUpdateArticleDto : BaseDto<ArticleEntity>
    {
        public string? Content { get; set; }

        public string? Slug { get; set; }

        public List<TagDto>? Tags { get; set; }

        public List<CreateOrUpdateImageDto> Images { get; set; }

        public string? Title { get; set; }

        public override ArticleEntity CreateEntity()
        {
            return new ArticleEntity()
            {
                Content = Content,
                Slug = Slug,
                Title = Title,
            };
        }
    }
}