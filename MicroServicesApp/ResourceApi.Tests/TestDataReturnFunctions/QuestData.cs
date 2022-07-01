using ResourceApi.Models;
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
    }
}
