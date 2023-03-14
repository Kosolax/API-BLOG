namespace BLOG.Dtos.Article
{
    using BLOG.Dtos.Image;
    using BLOG.Dtos.Tag;

    using System;
    using System.Collections.Generic;

    public class LightViewerArticleDto
    {
        public string? Description { get; set; }

        public string? Slug { get; set; }

        public List<TagDto>? Tags { get; set; }

        public string? Title { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public List<ImageDto>? Images { get; set; }
    }
}