namespace BLOG.DataAccess.Seed
{
    using BLOG.DataAccess.Enum;
    using BLOG.Entities;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using System.Linq;
    using System.Threading.Tasks;

    public class UserSeed : IContextSeed
    {
        public UserSeed(BlogContext context)
        {
            this.Context = context;
        }

        public BlogContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Users.Any() && !isProduction)
            {
                IdentityRole? adminRole = await this.Context.Roles.Where(x => x.Name == ERole.Admin.ToString()).FirstOrDefaultAsync();

                if (adminRole != null)
                {

                    UserEntity user = new UserEntity()
                    {
                        FirstName = "Maël",
                        LastName = "MININGER",
                        UserName = "ma.mininger@gmail.com",
                        NormalizedEmail = "MA.MININGER@GMAIL.COM",
                        NormalizedUserName = "MA.MININGER@GMAIL.COM",
                        Email = "ma.mininger@gmail.com",
                    };

                    var passwordHasher = new PasswordHasher<UserEntity>();
                    var hashedPassword = passwordHasher.HashPassword(user, "Azerty35!");

                    user.PasswordHash = hashedPassword;

                    await this.Context.Users.AddAsync(user);
                    await this.Context.SaveChangesAsync();


                    await this.Context.UserRoles.AddAsync(new IdentityUserRole<string>()
                    {
                        RoleId = adminRole.Id,
                        UserId = user.Id,
                    });

                    await this.Context.SaveChangesAsync();
                }
            }
        }
    }
}