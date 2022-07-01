using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceApi.Data.Interfaces;
using ResourceApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ResourceApi.Controllers
{
    //CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : BaseController<TestsController>
    {
       // private string UserName => User.Claims.Single(c => c.Type == ClaimTypes.).Value.ToString();
        public Guid Id => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public Guid id { get; set; } = Guid.Empty;

        //private ITestRepository _testRepository;
        public TestsController(ITestRepository testRepository, ILogger<TestsController> logger) : base(testRepository,null,null,logger)
        {

        }

        ///R
        [HttpGet("[action]/{page}")]
        //[Authorize(Roles = "user")]
        
        public async Task<IActionResult> GetTests(string category="",int page = 0)
        {
            var result = await _testRepository.GetTestsAsync(6, page);
            return Ok(JsonSerializer.Serialize(result));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTest(int id)
        {
            var test = await _testRepository.GetTestByIdAsync(id);
            if (test != null)
                return Ok(test);
            else
                return Ok(null);
        }

        ///C
        [HttpPost]
        [Route("api/[controller]/[action]")]
        //[Authorize]
        public async Task<IActionResult> CreateTest(TestModel test)
        {
            
            if (Id != Guid.Empty||id!=Guid.Empty)
            {
                
                test.CreatedDate = DateTime.Now;
                test.QuestsCount = 0;
                test.AvtorId = Id.ToString();
                if (ModelState.IsValid)
                {
                    var resoult = await _testRepository.CreateTestAsync(test);
                    if (resoult)
                    {
                        return Ok();
                    }
                }
                _logger.LogWarning($"Function CreateTest {DateTime.Now} model is invalid ");
            }
            _logger.LogWarning($"Function CreateTest {DateTime.Now} User Id == null");

            return BadRequest();
        }
        [HttpPatch("{Id}")]

        public async Task<IActionResult> Update(int? Id,string Title,string Image)
        {
            try
            {
                if (Id == null || id != Guid.Empty)
                {
                    _logger.LogWarning($"Function Update {DateTime.Now} Id == null");
                    return BadRequest();
                }

                var test = await  _testRepository.GetTestByIdAsync((int) Id);
                if (test == null) {
                    _logger.LogWarning($"Function Update {DateTime.Now} test == null");
                    return BadRequest("not found");
                }
                else
                {
                    if (!string.IsNullOrEmpty(Title))
                    {
                        test.Title = Title;
                        var result= await  _testRepository.UpdateAsync(test);
                        if (!result) return BadRequest();
                    }
                }
                return Ok("Updated");
            }
            catch(Exception e)
            {
                _logger.LogError($"Function Update {DateTime.Now} {e.Message}");

                return BadRequest("Error "+e.Message);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {

            if (Id <= 0)
                return BadRequest();
            var result =await _testRepository.RemoveAsync(Id);
            if(result)
                return Ok();
            return BadRequest();
        }

        [HttpGet("[action]/{page}")]
        [Authorize]
        public async Task<IActionResult> GetTestsByAvtor(int page)
        {
            try
            {
                return Ok(JsonSerializer.Serialize(await _testRepository.GetTestsByAvtorId(Id.ToString(), 6, page)));
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
