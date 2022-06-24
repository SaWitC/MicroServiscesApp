using Microsoft.EntityFrameworkCore;
using ResourceApi.Data.Interfaces;
using ResourceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceApi.Data.Repository
{
    public class LeftAnswerRepository : ILeftAnswerRepository
    {
        private readonly AppDbContext _appDbContext;
        public LeftAnswerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<bool> CreateAsync(List<string> answers, int QuestId)
        {
            try
            {
                List<LeftAnswer> leftAnswers = new List<LeftAnswer>();
                foreach(var x in answers)
                {
                    if (!string.IsNullOrEmpty(x))
                    {
                        leftAnswers.Add(new LeftAnswer() { QuestId = QuestId, Title = x });
                    }
                }
                _appDbContext.LeftAnswers.AddRange(leftAnswers);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveAsync(int Id)
        {
            var answer = await _appDbContext.LeftAnswers.FirstOrDefaultAsync(o => o.Id == Id);
            if (answer != null)
            {
                _appDbContext.LeftAnswers.Remove(answer);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;    
        }

        public async Task<bool> UpdateAsync(int LeftAnswerId, string value)
        {
            var answer = await _appDbContext.LeftAnswers.FirstOrDefaultAsync(o => o.Id ==LeftAnswerId);
            if (answer != null)
            {
                answer.Title = value;
                _appDbContext.LeftAnswers.Update(answer);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<LeftAnswer>> GetLeftAnswersByQuestIdAsync(int QuestId)
        {
            return await _appDbContext.LeftAnswers.Where(o => o.QuestId == QuestId).ToListAsync();
        }

        public async Task<bool> UpdateRangeAsync(List<LeftAnswer> OldAnswers, List<string> NewAnswerTitles)
        {
            if(OldAnswers.Count>0 && NewAnswerTitles.Count > 0)
            {
                for (int i = 0; i < NewAnswerTitles.Count; i++)
                {
                    OldAnswers[i].Title = NewAnswerTitles[i];
                }
                _appDbContext.UpdateRange(OldAnswers);
                await _appDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }


    }
}
