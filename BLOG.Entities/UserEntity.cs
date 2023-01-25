﻿namespace BLOG.Entities
{
    using Microsoft.AspNetCore.Identity;

    public class UserEntity : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}