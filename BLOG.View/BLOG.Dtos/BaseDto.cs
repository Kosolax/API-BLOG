namespace BLOG.Dtos
{
    using BLOG.Dtos.Validation.Service;
    using BLOG.Entities;

    public abstract class BaseDto<T>
        where T : BaseEntity
    {
        public ValidationService<T>? ValidationService { get; set; }

        public abstract T CreateEntity();
    }
}