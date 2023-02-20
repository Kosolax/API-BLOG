namespace BLOG.Entities
{
    public class ArticleTagEntity : BaseEntity
    {
        public ArticleEntity? ArticleEntity { get; set; }
        
        public int ArticleEntityId { get; set; }

        public TagEntity? TagEntity { get; set; }

        public int TagEntityId { get; set; }
    }
}