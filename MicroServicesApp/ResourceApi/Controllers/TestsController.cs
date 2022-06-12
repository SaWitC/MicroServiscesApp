using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceApi.Data.Interfaces;
using ResourceApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : BaseController
    {
       // private ITestRepository _testRepository;
        public TestsController(ITestRepository testRepository) : base(testRepository)
        {

        }
        [HttpGet]
        public async Task<IActionResult> GetTests(string category="",int page = 0)
        {
            return Ok(_testRepository.GetTestsAsync(page,5)); 
        }
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> CreateTest(byte Quests)
        {
            if (Quests > 0 && Quests < 100)
            {
                return Ok(Quests);
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
