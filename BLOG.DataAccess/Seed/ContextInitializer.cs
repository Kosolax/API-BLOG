namespace BLOG.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ContextInitializer
    {
        public async Task Seed(BlogContext context, bool isProduction)
        {
            List<IContextSeed> listSeed = new List<IContextSeed>
            {
                new RoleSeed(context),
                new UserSeed(context),
                new TagSeed(context),
                new ArticleSeed(context),
            };

            foreach (IContextSeed contextSeed in listSeed)
            {
                await contextSeed.Execute(isProduction);
            }
        }
    }
}