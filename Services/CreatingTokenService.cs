using Apps_Review_Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Apps_Review_Api.Services
{
    public class CreatingTokenService
    {
        private readonly IConfiguration _config;

       public CreatingTokenService(IConfiguration configuration) {
            this._config = configuration;
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: cred
                );

            var jwt  =  new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
