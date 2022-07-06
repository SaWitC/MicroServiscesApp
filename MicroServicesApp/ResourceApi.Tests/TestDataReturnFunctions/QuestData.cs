using Microsoft.EntityFrameworkCore.ChangeTracking;
using ResourceApi.Models;
using ResourceApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceApi.Tests.TestDataReturnFunctions
{
    class QuestData
    {
        public static async Task<IEnumerable<Quest>> ReturnQuests(Quest model)
        {
            return new List<Quest>() { model };
        }

        public static async Task<Quest> ReturnQuest(Quest model,int id)
        {
            return model;
        }
    }
}
