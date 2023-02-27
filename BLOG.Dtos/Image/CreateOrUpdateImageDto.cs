namespace BLOG.Dtos.Image
{
    using BLOG.Entities;

    public class CreateOrUpdateImageDto : BaseDto<ImageEntity>
    {
        public int Id { get; set; }

        public string? Placeholder { get; set; }

        public string? Base64Image { get; set; }

        public bool IsThumbnail { get; set; }

        public override ImageEntity CreateEntity()
        {
            return new ImageEntity
            {
                Id = Id,
                Placeholder = Placeholder,
                Base64Image = Base64Image,
                IsThumbnail = IsThumbnail,
            };
        }
    }
}