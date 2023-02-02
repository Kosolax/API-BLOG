namespace BLOG.Services.Handlers
{
    public class PaginationHandler
    {
        public int GetPageNumber(int pageNumber, int totalPages)
        {
            if (pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }

            return pageNumber;
        }

        public int GetTotalPages(int countArticles, int articlePerPage)
        {
            int totalPages;
            if ((countArticles / (float)articlePerPage) != (countArticles / articlePerPage))
            {
                totalPages = (countArticles / articlePerPage) + 1;
            }
            else
            {
                totalPages = (countArticles / articlePerPage);
            }

            return totalPages;
        }
    }
}