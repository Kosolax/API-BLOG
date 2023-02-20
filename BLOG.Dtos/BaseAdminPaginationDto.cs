namespace BLOG.Dtos
{
    using System.Collections.Generic;

    public abstract class BaseAdminPaginationDto<T>
    {
        public int CurrentPage { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public List<T>? Items { get; set; }
    }
}