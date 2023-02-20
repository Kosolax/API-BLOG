namespace BLOG.Dtos.Article
{
    using BLOG.Dtos.Tag;

    public class LightAdminArticleDto
    {
        public List<TagDto>? Tags { get; set; }

        public string? Title { get; set; }

        public int Views { get; set; }
    }
}