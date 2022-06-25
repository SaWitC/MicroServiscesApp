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

namespace ResourceApi.Controllers
{
    //CRUD
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : BaseController
    {
       // private string UserName => User.Claims.Single(c => c.Type == ClaimTypes.).Value.ToString();
        private Guid Id => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        //private ITestRepository _testRepository;
        public TestsController(ITestRepository testRepository) : base(testRepository)
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
            if (Id != Guid.Empty)
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
            }
            return BadRequest();
        }
        [HttpPatch("{Id}")]

        public async Task<IActionResult> Update(int? Id,string Title,string Image)
        {
            try
            {
                if (Id== null)
                    return BadRequest();

                var test = await  _testRepository.GetTestByIdAsync((int) Id);
                if (test == null)
                    return BadRequest("not found");
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
                return BadRequest("Error "+e.Message);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                if (Id <= 0)
                    return BadRequest();
                var result =await _testRepository.RemoveAsync(Id);
                if(result)
                    return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("[action]/{page}")]
        [Authorize]
        public async Task<IActionResult> GetTestsByAvtor(int page)
        {
            try
            {
               // var tests = 
                return Ok(JsonSerializer.Serialize(await _testRepository.GetTestsByAvtorId(Id.ToString(), 6, page)));
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
