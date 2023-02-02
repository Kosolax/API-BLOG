namespace BLOG.Entities
{
    public class TagEntity : BaseEntity
    {
        public bool IsDeleted { get; set; }

        public string? Name { get; set; }
    }
}