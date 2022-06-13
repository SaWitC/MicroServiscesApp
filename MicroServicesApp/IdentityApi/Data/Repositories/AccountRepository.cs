using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityApi.Data.Interfaces;
using IdentityApi.Data.optionsModels;
using IdentityApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityApi.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IOptions<AuthOptions> _options;

        public AccountRepository(IOptions<AuthOptions> options)
        {
            _options = options;
        }
        public string GenerateJWTToken(User user)
        {
            var authParams = _options.Value;
            var securityKey = authParams.GetSymetricSecurityKey();
            var creditails = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name,user.UserName),
                new Claim("role","user")
                
                //new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: creditails
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
