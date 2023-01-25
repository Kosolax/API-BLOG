namespace BLOG.DataAccess
{
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

        public async Task EnsureSeedData(bool isProduction)
        {
            ContextInitializer initializer = new ContextInitializer();
            await initializer.Seed(this, isProduction);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Action> listConfiguration = new List<Action>
            {
            };

            foreach (Action action in listConfiguration)
            {
                action.Invoke();
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}