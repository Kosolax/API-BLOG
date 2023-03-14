namespace BLOG.Dtos.Article
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PostViewerPaginationRequest
    {
        public List<int>? TagsId { get; set; }

        public string? Search { get; set; }

        [Required]
        public int PageNumber { get; set; }
    }
}
