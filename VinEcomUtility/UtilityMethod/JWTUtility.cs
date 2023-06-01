using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;

namespace VinEcomUtility.UtilityMethod
{
    public static class JWTUtility
    {
        public static string GenerateToken(this User user, IConfiguration configuration, DateTime createdAt, int minuteValidFor, string role, int storeId = -1, string? secretKey = null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim("StoreId", storeId.ToString())
            };
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                notBefore: createdAt,
                expires: createdAt.AddMinutes(minuteValidFor),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #region IsRenewableToken
        public static bool IsRenewableToken(this string token, DateTime now, int minimumTimeSpan)
        {
            var handler = new JwtSecurityTokenHandler();
            var createdAt = handler.ReadJwtToken(token).ValidFrom;
            if ((now - createdAt).TotalMinutes < minimumTimeSpan) return false;
            return true;
        }
        #endregion

        #region IsExpired
        public static bool IsExpired(this string token, DateTime now)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.ValidTo < now;
        }
        #endregion
    }
}
