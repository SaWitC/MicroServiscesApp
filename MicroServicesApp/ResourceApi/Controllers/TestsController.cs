using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceApi.Data.Interfaces;
using ResourceApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : BaseController
    {
       // private string UserName => User.Claims.Single(c => c.Type == ClaimTypes.).Value.ToString();
        private Guid Id => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        private ITestRepository _testRepository;
        public TestsController(ITestRepository testRepository) : base(testRepository)
        {

        }
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetTests(string category="",int page = 0)
        {
            //return Ok(_testRepository.GetTestsAsync(page,5)); 
            if (!string.IsNullOrEmpty(Id.ToString())) 
                return Ok(Id);
            return Ok("no name");

        }
        [HttpGet]
        [Route("api/[controller]/[action]")]
        [Authorize]
        public async Task<IActionResult> CreateTest(byte Quests)
        {
            if (Quests > 0 && Quests < 100)
            {
                return Ok(Quests+Id.ToString());
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("api/[controller]/[action]")]

        public async Task<IActionResult> CreateTest(TestModel model, byte Quests)
        {
            try
            {
                var quests =new List<Quest>();
                var result =await _testRepository.CreateTestAsync(model,quests);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            return Ok();
        }
    }
}
