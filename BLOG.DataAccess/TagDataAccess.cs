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

        public async Task<bool> DoListExist(List<int> list)
        {
            return await this.Context.Tags.CountAsync(x => list.Contains(x.Id)) == list.Count();
        }

        public override async Task<List<TagEntity>> List()
        {
            return await this.Context.Tags.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<List<TagEntity>> ListFromIds(List<int> ids)
        {
            return await this.Context.Tags.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public override async Task<List<TagEntity>> ListSkipTake(int skip, int take)
        {
            return await this.Context.Tags.Where(x => !x.IsDeleted).Skip(skip).Take(take).ToListAsync();
        }
    }
}