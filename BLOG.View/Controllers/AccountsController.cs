namespace Blog.Web.Controllers
{
    using BLOG.Dtos;
    using BLOG.Dtos.Response;
    using BLOG.Services.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountService;

        public AccountsController(IAccountsService accountService)
        {
            this._accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            var result = await this._accountService.Login(userLogin);
            if (result.Key)
            {
                return this.Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = result.Value });
            }

            return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var result = await this._accountService.RegisterUser(userForRegistration);
            if (result.Key)
            {
                return StatusCode(201);
            }

            return BadRequest(new RegistrationResponseDto { Errors = result.Value });
        }
    }
}