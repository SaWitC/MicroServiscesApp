using ResourceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceApi.Data.Interfaces
{
    public interface ILeftAnswerRepository
    {
        public Task<bool> CreateAsync(List<string> answers,int QuestId);
        public Task<bool> UpdateAsync(int LeftAnswerId, string value);
        public Task<bool> RemoveAsync(int Id);

        public Task<List<LeftAnswer>> GetLeftAnswersByQuestIdAsync(int QuestId);

        public Task<bool> UpdateRangeAsync(List<LeftAnswer> OldAnswers, List<string> NewAnswerTitles);


    }
}
