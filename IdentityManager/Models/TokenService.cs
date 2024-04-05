using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Models
{
    public class TokenService : ITokenService
    {
        public static readonly string key = "$$smaRt$$shoP$$is$$protected$$";
        public static readonly string issuer = "Surmanidze Denis";
        public static readonly string audience = "SmartShopUsers";

        public string  CreateToken(RequestUser user)
        {
            
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity
                (claims,JwtBearerDefaults.AuthenticationScheme,
                JwtRegisteredClaimNames.Name, 
                ClaimTypes.Role);

            var signingCredentials = new SigningCredentials
                (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),SecurityAlgorithms.HmacSha256);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.CreateJwtSecurityToken
                (
                    subject:identity,
                    issuer:issuer,
                    audience:audience,
                    expires:DateTime.Now.AddMinutes(200),
                    signingCredentials: signingCredentials
                );

            return handler.WriteToken(token);
          
        }

        public bool ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                handler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                }, out SecurityToken securityToken);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
