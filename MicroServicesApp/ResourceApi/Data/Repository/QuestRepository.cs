using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResourceApi.Data.Interfaces;
using ResourceApi.Models;
using ResourceApi.ViewModel;

namespace ResourceApi.Data.Repository
{
    public class QuestRepository:IQuestRepository
    {
        private readonly AppDbContext _context;

        public QuestRepository(AppDbContext context)
        {
            _context = context;

        }
        public async Task<bool> CreateQuestAsync(int TestId, Quest quest)
        {
            if (TestId > 0)
            {
                var test = await _context.testModels.FirstOrDefaultAsync(o => o.Id == TestId);
                if (test != null)
                {
                    quest.test = test;
                    quest.TestId = test.Id;
                    await _context.Quests.AddAsync(quest);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> CreateQuestAsync(Quest quest)
        {
            try
            {
                await _context.Quests.AddAsync(quest);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public async Task<bool> EditQuestAsync(UpdateViewModel model)
        {
            if (model.TestId > 0)
            {
                var test = _context.testModels.FirstOrDefaultAsync(o => o.Id == model.TestId);
                if (test != null)
                {
                    var quest = await _context.Quests.FirstOrDefaultAsync(o => o.Id == model.Id);
                    if (quest != null)
                    {
                        quest.QuestText = model.QuestText;
                        quest.HelpText = model.HelpText;
                        quest.ImgPath = model.ImgPath;
                        _context.Quests.Update(quest);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<bool> RemoveQuestAsync(Quest model)
        {
            try
            {
                _context.Quests.Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> RemoveQuestAsync(int? QuestId)
        {
            try
            {
                if (QuestId != null)
                {
                    var quest = await _context.Quests.FirstOrDefaultAsync(o => o.Id == QuestId);
                    if (quest != null)
                    {
                        _context.Quests.Remove(quest);
                        await _context.SaveChangesAsync();

                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Quest>> GetQuestsByTestId(int Id)
        {
            return await _context.Quests.Where(o => o.TestId == Id).ToListAsync();
        }
        public async Task<Quest> GetQuestByid(int id)
        {
            return await _context.Quests.FirstOrDefaultAsync(o => o.Id == id);
        }

    }
}
