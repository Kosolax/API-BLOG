namespace BLOG.DataAccess.Seed
{
    using BLOG.DataAccess.Enum;

    using Microsoft.AspNetCore.Identity;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RoleSeed : IContextSeed
    {
        public RoleSeed(BlogContext context)
        {
            this.Context = context;
        }

        public BlogContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Roles.Any() && !isProduction)
            {
                List<IdentityRole> roles = new List<IdentityRole>
                {
                    new IdentityRole()
                    {
                        Name = ERole.Admin.ToString(),
                        NormalizedName = ERole.Admin.ToString().ToUpper(),
                    },
                    new IdentityRole()
                    {
                        Name = ERole.Viewer.ToString(),
                        NormalizedName = ERole.Viewer.ToString().ToUpper(),
                    }
                };

                await this.Context.Roles.AddRangeAsync(roles);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}