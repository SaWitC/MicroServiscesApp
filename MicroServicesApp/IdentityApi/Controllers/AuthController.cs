using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityApi.Data.optionsModels;
using IdentityApi.Models;
using IdentityApi.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using IdentityApi.Data.Interfaces;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthController(IAccountRepository accountRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User() { UserName = model.UserName, Email = model.Email };
                var resoult = await _userManager.CreateAsync(user, model.Password);
                if (resoult.Succeeded)
                {
                    //var createduser = _userManager.FindByNameAsync(user.UserName);
                    await _signInManager.SignInAsync(user, false);
                    return Ok();
                }

                return BadRequest("Can not register");
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("api/[controller]/[action]")]

        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.Password))
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Ok(_accountRepository.GenerateJWTToken(user));
                    }
                }

                return Unauthorized();
            }
            return Unauthorized();
        }
    }
}
