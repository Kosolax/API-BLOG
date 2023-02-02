namespace BLOG.Dtos.Tag
{
    using BLOG.Entities;

    public class CreateOrUpdateTagDto : BaseDto<TagEntity>
    {
        public string? Name { get; set; }

        public override TagEntity CreateEntity()
        {
            return new TagEntity
            {
                Name = this.Name,
            };
        }
    }
}