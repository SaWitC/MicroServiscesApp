using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResourceApi.Data;
using ResourceApi.Data.Interfaces;
using ResourceApi.Models;
using ResourceApi.ViewModel;


namespace ResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestsController : BaseController<QuestsController>
    {
        private Guid Id => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public QuestsController(IQuestRepository questRepository,
            ITestRepository testRepository,
            ILeftAnswerRepository leftAnswerRepository,
            ILogger<QuestsController> logger):base(testRepository,questRepository,leftAnswerRepository,logger)
        {}

        // GET: api/Quests
        [HttpGet("{TestId}")]
        public async Task<ActionResult> GetQuests(int TestId)
        {
            _logger.LogInformation($"Function GetQuests {DateTime.Now} input data [testId:{TestId}]");
            return Ok(JsonSerializer.Serialize(await _questRepository.GetQuestsByTestId(TestId)));
        }

        [Authorize]
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
                        _logger.LogWarning($"Function Create {DateTime.Now} quest == null");

                    }
                    _logger.LogWarning($"Function Create {DateTime.Now} entity == null");
                }
                _logger.LogWarning($"Function Create {DateTime.Now} test == null");

            }
            return BadRequest();
        }

        // DELETE: api/Quests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuest(int id)
        {
            var quest = await _questRepository.GetQuestByid(id);
            if (quest == null)
            {
                _logger.LogWarning($"Function DeleteQuest {DateTime.Now} quest == null inputData [id:{id}]");

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
                _logger.LogWarning($"Function Update {DateTime.Now} entity == null");
            }
            _logger.LogWarning($"Function Update {DateTime.Now} model is invalid");

            return BadRequest();
        }

        [HttpGet("[action]/{TestId}")]
        public async Task<IActionResult> GetTestForPassing(int TestId)
        {
            return Ok(JsonSerializer.Serialize(await _questRepository.GetFullQuestsForPassingAsync(TestId)));
        }
    }
}
