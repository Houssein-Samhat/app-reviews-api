using Apps_Review_Api.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace Apps_Review_Api.Services
{
    public class DecodingUserPassService
    {
        public bool VerifyPasswordHash(string password, User user)
        {
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(user.PasswordHash);
            }
        }
    }
}
