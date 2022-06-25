using System;
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
        //R
        public async Task<System.Collections.Generic.IEnumerable<Models.TestModel>> GetTestsAsync(int size, int page=0)
        {
            return await _context.testModels.Skip(page * size).Take(size).ToListAsync();
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

        public async Task<IEnumerable<TestModel>> GetAll()
        {
            return await _context.testModels.ToListAsync();
        }

        public async Task<TestModel> GetTestByIdAsync(int Id)
        {
            return await _context.testModels.FirstOrDefaultAsync(o => o.Id == Id);
        }

        public async Task<bool> UpdateAsync(TestModel model)
        {
            try
            {
                _context.testModels.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> RemoveAsync(int Id)
        {
            try
            {
                var test =await _context.testModels.FirstOrDefaultAsync(o => o.Id == Id);
                if (test != null)
                {
                    _context.testModels.Remove(test);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<IEnumerable<TestModel>> GetTestsByAvtorId(string Id,int size,int page=0)
        { 
            return await _context.testModels.Where(o => o.AvtorId == Id).Skip(page*size).Take(size).ToListAsync();
        }

        public async Task<bool> TestCountAdd(TestModel model)
        {
            try
            {
                model.QuestsCount++;
                _context.testModels.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> TestCountTake_Away(TestModel model)
        {
            try
            {
                model.QuestsCount--;
                _context.testModels.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<TestModel> GetFullTestForPassingAsync(int TestId)
        {
            var result = await _context.testModels.FirstOrDefaultAsync(o => o.Id == TestId);
            if(result!=null)
                result.Quests = await _context.Quests.Where(o => o.TestId == TestId).Include(o => o.LeftAnswers).ToListAsync();
            return result;
        }




    }
}
