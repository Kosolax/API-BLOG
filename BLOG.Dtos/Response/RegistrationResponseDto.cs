namespace BLOG.Dtos.Response
{
    using System.Collections.Generic;

    public class RegistrationResponseDto
    {
        public IEnumerable<string>? Errors { get; set; }

        public bool IsSuccessfulRegistration { get; set; }
    }
}