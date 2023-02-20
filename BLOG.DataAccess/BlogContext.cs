namespace BLOG.DataAccess
{
    using BLOG.DataAccess.Configuration;
    using BLOG.DataAccess.Seed;
    using BLOG.Entities;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class BlogContext : IdentityDbContext<UserEntity>
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public DbSet<TagEntity> Tags { get; set; }

        public DbSet<ArticleEntity> Articles { get; set; }

        public DbSet<ArticleTagEntity> ArticlesTags { get; set; }

        public async Task EnsureSeedData(bool isProduction)
        {
            ContextInitializer initializer = new ContextInitializer();
            await initializer.Seed(this, isProduction);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Action> listConfiguration = new List<Action>
            {
                new TagConfiguration(modelBuilder).Execute,
                new ArticleConfiguration(modelBuilder).Execute,
                new ArticleTagConfiguration(modelBuilder).Execute,
            };

            foreach (Action action in listConfiguration)
            {
                action.Invoke();
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}