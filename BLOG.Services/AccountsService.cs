namespace BLOG.Services
{
    using BLOG.DataAccess.Enum;
    using BLOG.Dtos.Authentication;
    using BLOG.Entities;
    using BLOG.Services.Handlers;
    using BLOG.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;

    using System.IdentityModel.Tokens.Jwt;

    public class AccountsService : IAccountsService
    {
        private readonly JwtHandler _jwtHandler;

        private readonly UserManager<UserEntity> _userManager;

        public AccountsService(UserManager<UserEntity> userManager, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        public async Task<KeyValuePair<bool, string>> Login(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
            {
                return new KeyValuePair<bool, string>(false, string.Empty);
            }

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = await _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new KeyValuePair<bool, string>(true, token);
        }

        public async Task<KeyValuePair<bool, List<string>>> RegisterUser(UserRegistrationDto userRegistration)
        {
            List<string> errors = new List<string>();
            var user = userRegistration.CreateEntity();
            if (user != null)
            {
                var result = await _userManager.CreateAsync(user, userRegistration.Password);
                if (!result.Succeeded)
                {
                    errors = result.Errors.Select(e => e.Description).ToList();

                    return new KeyValuePair<bool, List<string>>(false, errors);
                }

                // We only register viewer as we don't people to create admin so easily
                await _userManager.AddToRoleAsync(user, ERole.Viewer.ToString());
                return new KeyValuePair<bool, List<string>>(true, errors);
            }
            else
            {
                errors.Add("We couldn't map the user.");
            }

            return new KeyValuePair<bool, List<string>>(false, errors);
        }
    }
}