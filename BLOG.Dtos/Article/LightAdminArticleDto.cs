namespace BLOG.Dtos.Article
{
    using BLOG.Dtos.Tag;

    public class LightAdminArticleDto
    {
        public int Id { get; set; }

        public string? Slug { get; set; }

        public List<TagDto>? Tags { get; set; }

        public string? Title { get; set; }

        public int Views { get; set; }
    }
}