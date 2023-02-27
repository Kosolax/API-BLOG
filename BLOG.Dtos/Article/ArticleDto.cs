namespace BLOG.Dtos.Article
{
    using BLOG.Dtos.Image;
    using BLOG.Dtos.Tag;

    public class ArticleDto
    {
        public int Id { get; set; }

        public string? Content { get; set; }

        public string? Description { get; set; }

        public string? Slug { get; set; }

        public List<TagDto>? Tags { get; set; }

        public string? Title { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int Views { get; set; }

        public List<ImageDto>? Images { get; set; }
    }
}