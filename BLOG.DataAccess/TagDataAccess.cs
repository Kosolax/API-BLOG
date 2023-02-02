namespace BLOG.DataAccess
{
    using BLOG.DataAccess.Interfaces;
    using BLOG.Entities;
    using Microsoft.EntityFrameworkCore;

    public class TagDataAccess : BaseDataAccess<TagEntity>, ITagDataAccess
    {
        public TagDataAccess(BlogContext blogContext) : base(blogContext)
        {
        }

        public override async Task<int> Count()
        {
            return await this.Context.Tags.CountAsync(x => !x.IsDeleted);
        }

        public override async Task<List<TagEntity>> List()
        {
            return await this.Context.Tags.Where(x => !x.IsDeleted).ToListAsync();
        }

        public override async Task<List<TagEntity>> ListSkipTake(int skip, int take)
        {
            return await this.Context.Tags.Where(x => !x.IsDeleted).Skip(skip).Take(take).ToListAsync();
        }
    }
}