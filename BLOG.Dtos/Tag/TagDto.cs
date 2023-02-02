namespace BLOG.Dtos.Tag
{
    using BLOG.Entities;

    public class TagDto : BaseDto<TagEntity>
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public override TagEntity CreateEntity()
        {
            return new TagEntity
            {
                Id = this.Id,
                Name = this.Name,
            };
        }
    }
}