namespace BLOG.Dtos
{
    using System.Collections.Generic;

    public abstract class BaseAdminPaginationDto<T, T1> where T : class
    {
        public int CurrentPage { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public List<T1>? Items { get; set; }
    }
}