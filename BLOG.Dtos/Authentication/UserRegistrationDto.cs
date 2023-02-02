namespace BLOG.Dtos.Authentication
{
    using BLOG.Entities;
    using System.ComponentModel.DataAnnotations;

    public class UserRegistrationDto : BaseDto<UserEntity>
    {
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        public override UserEntity CreateEntity()
        {
            return new UserEntity
            {
                Email = Email,
                NormalizedUserName = Email.ToUpper(),
                NormalizedEmail = Email.ToUpper(),
                UserName = Email,
                FirstName = FirstName,
                LastName = LastName,
                PasswordHash = Password,
            };
        }
    }
}