using System;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;

namespace Users.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public string HashPassword(string Password)
        {
            return BCrypt.Net.BCrypt.HashPassword(Password);
        }
        public bool ValidatePassword(string Password)
        {
            return BCrypt.Net.BCrypt.Verify(Password, HashPassword(Password));
        }
    }
}
