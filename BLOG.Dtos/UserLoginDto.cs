namespace BLOG.Dtos
{
    using BLOG.Entities;
    using System.ComponentModel.DataAnnotations;

    public class UserLoginDto : BaseDto<UserEntity>
    {
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }

        public override UserEntity CreateEntity()
        {
            return new UserEntity()
            {
                UserName = Email,
                PasswordHash = Password,
            };
        }
    }
}