using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResourceApi.Models;
using ResourceApi.ViewModel;

namespace ResourceApi.Data.Interfaces
{
    public interface IQuestRepository
    {
        Task<bool> CreateQuestAsync(int TestId, Quest quest);
        Task<bool> CreateQuestAsync(Quest quest);
        Task<bool> EditQuestAsync(UpdateViewModel quest);
        Task<bool> RemoveQuestAsync(int? QuestId);
        Task<bool> RemoveQuestAsync(Quest model);
        Task<Quest> GetQuestByid(int id);

        Task<IEnumerable<Quest>> GetQuestsByTestId(int Id);



    }
}
