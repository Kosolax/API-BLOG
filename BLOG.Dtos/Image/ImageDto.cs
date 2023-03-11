namespace BLOG.Dtos.Image
{
    public class ImageDto
    {
        public int Id { get; set; }

        public string? Placeholder { get; set; }

        public string? Base64Image { get; set; }

        public bool IsThumbnail { get; set; }

        public int ArticleId { get; set; }
    }
}