using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceApi.Data;
using ResourceApi.Data.Interfaces;
using ResourceApi.Models;

namespace ResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestsController : ControllerBase
    {
        private readonly IQuestRepository _questRepository;
        private readonly ITestRepository _testRepository;


        public QuestsController(IQuestRepository questRepository,
            ITestRepository testRepository)
        {
            _questRepository = questRepository;
            _testRepository = testRepository;
        }

        // GET: api/Quests
        [HttpGet("{Id}")]
        public async Task<ActionResult> GetQuests(int Id)
        {
            return Ok(JsonSerializer.Serialize(await _questRepository.GetQuestsByTestId(Id)));
        }

        // PUT: api/Quests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutQuest(int id, Quest quest)
        //{
        //    if (id != quest.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(quest).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!QuestExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Quests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quest>> Create(Quest quest,int? id)
        {
            if (id != null)
            {
                var test =await _testRepository.GetTestByIdAsync((int)id);
                if (test != null)
                {
                    quest.TestId = test.Id;
                    await _questRepository.CreateQuestAsync(quest);
                    //await _context.SaveChangesAsync();
                    return Ok(quest);
                }
            }
            //return CreatedAtAction("GetQuest", new { id = quest.Id }, quest);
            return BadRequest();
        }

        // DELETE: api/Quests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuest(int id)
        {
            var quest = await _questRepository.GetQuestByid(id);
            if (quest == null)
            {
                return NotFound();
            }

            await _questRepository.RemoveQuestAsync(quest);

            return NoContent();
        }

        //private bool QuestExists(int id)
        //{
        //    return _context.Quests.Any(e => e.Id == id);
        //}
    }
}
