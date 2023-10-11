using EntityLayer.Concrete;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MenuList.Authenticate
{
    public class CreateToken
    {
        private IConfiguration _configuration;

        public CreateToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Token(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
            };

            var bytes = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
            );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}
