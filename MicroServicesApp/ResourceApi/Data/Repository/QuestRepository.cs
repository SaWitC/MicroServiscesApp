using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        public async Task<EntityEntry<Quest>> CreateQuestAsync(int TestId, Quest quest)
        {
            if (TestId > 0)
            {
                var test = await _context.testModels.FirstOrDefaultAsync(o => o.Id == TestId);
                if (test != null)
                {
                    quest.test = test;
                    quest.TestId = test.Id;
                    var entityEntry =await _context.Quests.AddAsync(quest);
                    await _context.SaveChangesAsync();
                    return entityEntry;
                }
            }
            return null;
        }
        public async Task<EntityEntry<Quest>> CreateQuestAsync(Quest quest)
        {
            try
            {
                var entity =await _context.Quests.AddAsync(quest);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

                _context.Quests.Update(Oldquest);
                await _context.SaveChangesAsync();
                return true;
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
            return await _context.Quests.Where(o => o.TestId == Id).Include(o=>o.LeftAnswers).ToListAsync();
        }
        public async Task<Quest> GetQuestByid(int id)
        {
            return await _context.Quests.FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<IEnumerable<Quest>> GetFullQuestsForPassingAsync(int TestId)
        {
            var result= await _context.Quests.Where(o => o.TestId == TestId).Include(o => o.LeftAnswers).ToListAsync();
            return result;
        }


    }
}
