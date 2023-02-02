namespace BLOG.Dtos.Authentication
{
    public class AuthResponseDto
    {
        public string? ErrorMessage { get; set; }

        public bool IsAuthSuccessful { get; set; }

        public string? Token { get; set; }
    }
}