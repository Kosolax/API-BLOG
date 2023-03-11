namespace BLOG.DataAccess
{
    using BLOG.DataAccess.Interfaces;
    using BLOG.Entities;

    using Microsoft.EntityFrameworkCore;

    using System.Threading.Tasks;

    public class ArticleDataAccess : BaseDataAccess<ArticleEntity>, IArticleDataAccess
    {
        public ArticleDataAccess(BlogContext context) : base(context)
        {
        }

        public async Task<ArticleEntity?> Get(string slug)
        {
            return await this.Context.Articles.Where(x => x.Slug == slug).FirstOrDefaultAsync();
        }

        public override async Task<List<ArticleEntity>> ListSkipTake(int skip, int take)
        {
            return await this.Context.Articles.Where(x => !x.IsDeleted).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<List<ArticleEntity>> ListFromIdsSkipTake(List<int> ids, int skip, int take)
        {
            return await this.Context.Articles.Where(x => ids.Contains(x.Id)).Skip(skip).Take(take).ToListAsync();
        }
    }
}