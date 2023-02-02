namespace BLOG.DataAccess.Seed
{
    using BLOG.Entities;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TagSeed : IContextSeed
    {
        public TagSeed(BlogContext context)
        {
            this.Context = context;
        }

        public BlogContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Tags.Any() && !isProduction)
            {
                List<TagEntity> tags = new List<TagEntity>
                {
                    new TagEntity()
                    {
                        Name = "tag1",
                        IsDeleted = false,
                    },
                    new TagEntity()
                    {
                        Name = "tag2",
                        IsDeleted = false,
                    },
                    new TagEntity()
                    {
                        Name = "tag3",
                        IsDeleted = false,
                    },
                    new TagEntity()
                    {
                        Name = "tag4",
                        IsDeleted = false,
                    },
                    new TagEntity()
                    {
                        Name = "tag5",
                        IsDeleted = false,
                    },
                };

                await this.Context.Tags.AddRangeAsync(tags);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}