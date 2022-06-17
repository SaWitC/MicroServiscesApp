using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResourceApi.Data.Interfaces;
using ResourceApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceApi.Data.Repository
{
    public class TestsRepository:ITestRepository
    {
        private readonly Microsoft.Extensions.Logging.ILogger<TestsRepository> _logger;
        private readonly AppDbContext _context;

        public TestsRepository(ILogger<TestsRepository> logger,AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<System.Collections.Generic.IEnumerable<Models.TestModel>> GetTestsAsync(int size, int page = 1)
        {
           return await _context.testModels.Take(size).Skip(page * size).ToListAsync();
        }
        public async Task<bool> CreateTestAsync(TestModel model)
        {
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.AvtorId) && !string.IsNullOrEmpty(model.Title))
                {
                    await _context.testModels.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }

            return false;
            //if (model == null&&model.Quests.Count()>0)
            //{
            //    var Quests = new List<Models.Quest>();

            //    var entity =await _context.testModels.AddAsync(model);
            //    if (entity != null)
            //    {
            //        foreach (var item in quests)
            //        {
            //            item.TestId = entity.Entity.Id;
            //            Quests.Add(item);
            //        }
            //        await _context.Quests.AddRangeAsync(Quests);
            //    }
            //    return false;

            //}
            //return false;
        }

    }
}
