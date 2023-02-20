namespace BLOG.Entities
{
    using System;

    public class ArticleEntity : BaseEntity
    {
        public bool IsDeleted { get; set; }

        public string? Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? Description { get; set; }

        public string? Slug { get; set; }

        public string? Thumbnail { get; set; }

        public string? Title { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int Views { get; set; }
    }
}