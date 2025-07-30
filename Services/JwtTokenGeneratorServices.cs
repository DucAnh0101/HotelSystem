using DataAccessLayer.RequestDTO;
using DataAccessLayer.ResponseDTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class JwtTokenGeneratorServices
    {
        private readonly JWTSettings _settings;

        public JwtTokenGeneratorServices(IOptions<JWTSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateToken(UserRes res)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, res.ID.ToString()),
            new Claim(ClaimTypes.Role, res.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
