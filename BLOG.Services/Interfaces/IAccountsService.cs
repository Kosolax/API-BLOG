namespace BLOG.Services.Interfaces
{
    using BLOG.Dtos.Authentication;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAccountsService
    {
        Task<KeyValuePair<bool, string>> Login(UserLoginDto userForAuthenticationDto);

        Task<KeyValuePair<bool, List<string>>> RegisterUser(UserRegistrationDto userForRegistration);
    }
}