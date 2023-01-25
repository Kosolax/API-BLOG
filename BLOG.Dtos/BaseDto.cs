namespace BLOG.Dtos
{
    public abstract class BaseDto<T>
        where T : class
    {
        public abstract T CreateEntity();
    }
}