using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace IdentityAPI.Helpers
{
    public static class JwtTokenManager
    {
        private static string GenerateJwtToken(IConfiguration configuration, List<Claim> claims, TokenType tokenType)
        {
            var key = Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]);

            DateTime expirationTime;

            if (tokenType == TokenType.Refresh)
            {
                expirationTime = DateTime.UtcNow.AddMonths(3);
            }

            else
            {
                expirationTime = DateTime.UtcNow.AddMinutes(15);
            }
            var token = new JwtSecurityToken(
                            issuer: configuration["JwtSettings:Issuer"],
                            audience: configuration["JwtSettings:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(15),
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string ValidateToken(IConfiguration configuration, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["JwtSettings:SecretKey"]);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);

            return tokenHandler.WriteToken((JwtSecurityToken)validatedToken);
        }

        public static string GenerateJwtAccessToken(IConfiguration configuration, List<Claim> claims)
        {
            return GenerateJwtToken(configuration, claims, TokenType.Access);
        }

        public static string GenerateJwtRefreshToken(IConfiguration configuration, List<Claim> claims)
        {
            return GenerateJwtToken(configuration, claims, TokenType.Refresh);
        }
    }
}
