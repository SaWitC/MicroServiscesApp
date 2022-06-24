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
using ResourceApi.ViewModel;


namespace ResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestsController : ControllerBase
    {
        private readonly IQuestRepository _questRepository;
        private readonly ITestRepository _testRepository;
        private readonly ILeftAnswerRepository _leftAnswerRepository;


        public QuestsController(IQuestRepository questRepository,
            ITestRepository testRepository,
            ILeftAnswerRepository leftAnswerRepository)
        {
            _leftAnswerRepository = leftAnswerRepository;
            _questRepository = questRepository;
            _testRepository = testRepository;
        }

        // GET: api/Quests
        [HttpGet("{TestId}")]
        public async Task<ActionResult> GetQuests(int TestId)
        {
            return Ok(JsonSerializer.Serialize(await _questRepository.GetQuestsByTestId(TestId)));
        }

        [HttpPost("{TestId}")]
        public async Task<ActionResult<Quest>> Create(CreateQuestVM Quest,int? TestId)
        {
            if (TestId != null)
            {
                var test =await _testRepository.GetTestByIdAsync((int)TestId);
                if (test != null)
                {
                    var questModels = new Quest();
                    questModels.ImgPath = Quest.ImgPath;
                    questModels.QuestText = Quest.QuestText;
                    questModels.Right_answer = Quest.Right_answer;
                    questModels.HelpText = Quest.HelpText;


                    questModels.TestId = test.Id;
                    var entity=await _questRepository.CreateQuestAsync(questModels);
                    if (entity != null)
                    {
                        var quest = await _questRepository.GetQuestByid(entity.Entity.Id);
                        if (quest != null)
                        {
                            await _testRepository.TestCountAdd(test);
                            var IsCreatedAnswer = await _leftAnswerRepository.CreateAsync(Quest.LeftAnswers,quest.Id);
                            
                            if (IsCreatedAnswer)
                                return Ok();
                        }
                    }
                    //await _context.SaveChangesAsync();
                    //return ba();
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(CreateQuestVM Quest,int id)
        {
            if (ModelState.IsValid)
            {
                var quest = await _questRepository.GetQuestByid(id);
                if (quest != null)
                {
                   var result= await _questRepository.EditQuestAsync(Quest,quest);
                    if (result) {
                        var Answers= await _leftAnswerRepository.GetLeftAnswersByQuestIdAsync(quest.Id);

                        bool operateResoult;
                        if (Answers.Count > 0)
                            operateResoult = await _leftAnswerRepository.UpdateRangeAsync(Answers, Quest.LeftAnswers);
                        else
                            operateResoult = await _leftAnswerRepository.CreateAsync(Quest.LeftAnswers, quest.Id);
                        if (operateResoult)
                            return Ok();
                    }
                }
               
            }
            return BadRequest();
        }
    }
}
