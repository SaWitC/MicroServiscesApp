using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ResourceApi.Models;
using ResourceApi.ViewModel;

namespace ResourceApi.Data.Interfaces
{
    public interface IQuestRepository
    {
        Task<EntityEntry<Quest>> CreateQuestAsync(int TestId, Quest quest);
        Task<EntityEntry<Quest>> CreateQuestAsync(Quest quest);
        Task<bool> EditQuestAsync(CreateQuestVM quest,Quest Oldquest);
        Task<bool> RemoveQuestAsync(int? QuestId);
        Task<bool> RemoveQuestAsync(Quest model);
        Task<Quest> GetQuestByid(int id);

        Task<IEnumerable<Quest>> GetQuestsByTestId(int Id);



    }
}
