namespace BLOG.Entities
{
    public class ImageEntity : BaseEntity
    {
        public string? Placeholder { get; set; }

        public string? Base64Image { get; set; }

        public ArticleEntity? ArticleEntity { get; set; }

        public int ArticleEntityId { get; set; }

        public bool IsThumbnail { get; set; }
    }
}