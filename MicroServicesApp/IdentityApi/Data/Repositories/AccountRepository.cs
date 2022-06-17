using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityApi.Data.Interfaces;
using IdentityApi.Data.optionsModels;
using IdentityApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityApi.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IOptions<AuthOptions> _options;
        private readonly UserManager<User> _userManager;

        public AccountRepository(IOptions<AuthOptions> options, UserManager<User> userManager)
        {
            _options = options;
            _userManager = userManager;
        }
        public async Task<string> GenerateJWTToken(User user)
        {
            var authParams = _options.Value;
            var securityKey = authParams.GetSymetricSecurityKey();
            var creditails = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name,user.UserName),
                
                
                //new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };
            foreach (var VARIABLE in await _userManager.GetRolesAsync(user) )
            {
               claims.Add(new Claim("role", VARIABLE));
            }

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
