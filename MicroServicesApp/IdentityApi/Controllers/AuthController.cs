using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityApi.Data.optionsModels;
using IdentityApi.Models;
using IdentityApi.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using IdentityApi.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly Logger<AuthController> _logger;
        public AuthController(IAccountRepository accountRepository,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            Logger<AuthController> logger)
        {
            _logger = logger;
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
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var res =await _userManager.AddToRoleAsync(user,"user");
                    if (res.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);

                        return Ok();
                    }
                    
                    //var createduser = _userManager.FindByNameAsync(user.UserName);
                    
                }
                _logger.LogWarning("incorrect values ");
                return BadRequest("Имя пользователя занято");
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

                        var token = await _accountRepository.GenerateJWTToken(user);
                        _logger.LogInformation($"Loged {JsonSerializer.Serialize(user)}");

                        return Ok(new {Token=token});
                        //return Ok(res);
                    }
                }

                return Unauthorized();
            }
            return Unauthorized();
        }
    }
}
