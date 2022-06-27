using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using ResourceApi.Data.Interfaces;
using ResourceApi.Models;
using ResourceApi.ViewModel;

namespace ResourceApi.Data.Repository
{
    public class QuestRepository:BaseRepository<QuestRepository>,IQuestRepository
    {
        public QuestRepository(AppDbContext appDbContext, ILogger<QuestRepository> logger) : base(appDbContext, logger) { }
        public async Task<EntityEntry<Quest>> CreateQuestAsync(int TestId, Quest quest)
        {
            
            if (TestId > 0)
            {
                var test = await _appDbContext.testModels.FirstOrDefaultAsync(o => o.Id == TestId);
                if (test != null)
                {
                    quest.test = test;
                    quest.TestId = test.Id;
                    var entityEntry =await _appDbContext.Quests.AddAsync(quest);
                    await _appDbContext.SaveChangesAsync();
                    return entityEntry;
                }
                _logger.LogWarning($"{DateTime.Now} test ==null");
            }
            _logger.LogWarning($"{DateTime.Now} TestId is incorect {TestId}");
            return null;
        }
        public async Task<EntityEntry<Quest>> CreateQuestAsync(Quest quest)
        {
            try
            {
                var entity =await _appDbContext.Quests.AddAsync(quest);
                await _appDbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError($"{DateTime.Now} CreateQuestError {e.Message}");
                return null;
            }
        }
        public async Task<bool> EditQuestAsync(CreateQuestVM model, Quest Oldquest)
        {

            if (Oldquest.TestId > 0 &&Oldquest.Id>0)
            {
                Oldquest.ImgPath = model.ImgPath;
                Oldquest.QuestText = model.QuestText;
                Oldquest.Right_answer = model.Right_answer;
                Oldquest.HelpText = model.HelpText;

                _appDbContext.Quests.Update(Oldquest);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            _logger.LogWarning($"{DateTime.Now} Edit error model ={JsonSerializer.Serialize(model)}\n oldQuest ={JsonSerializer.Serialize(Oldquest)}");

            return false;
        }

        public async Task<bool> RemoveQuestAsync(Quest model)
        {
            try
            {
                _appDbContext.Quests.Remove(model);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError($"{DateTime.Now} Remove Error {e.Message}");

                return false;
            }
        }
        public async Task<bool> RemoveQuestAsync(int? QuestId)
        {
            try
            {
                if (QuestId != null)
                {
                    var quest = await _appDbContext.Quests.FirstOrDefaultAsync(o => o.Id == QuestId);
                    if (quest != null)
                    {
                        _appDbContext.Quests.Remove(quest);
                        await _appDbContext.SaveChangesAsync();

                        return true;
                    }
                }
                _logger.LogWarning($"{DateTime.Now}  QuestId ==null");

                return false;
            }
            catch(Exception e)
            {
                _logger.LogError($"{DateTime.Now} Remove quest error {e.Message}");

                return false;
            }
        }

        public async Task<IEnumerable<Quest>> GetQuestsByTestId(int Id)
        {
            return await _appDbContext.Quests.Where(o => o.TestId == Id).Include(o=>o.LeftAnswers).ToListAsync();
        }
        public async Task<Quest> GetQuestByid(int id)
        {
            return await _appDbContext.Quests.FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<IEnumerable<Quest>> GetFullQuestsForPassingAsync(int TestId)
        {
            var result= await _appDbContext.Quests.Where(o => o.TestId == TestId).Include(o => o.LeftAnswers).ToListAsync();
            return result;
        }


    }
}
