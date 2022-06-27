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
    public class TestsRepository:BaseRepository<TestsRepository>,ITestRepository
    {
        public TestsRepository(AppDbContext appDbContext, ILogger<TestsRepository> logger) : base(appDbContext, logger) { }

        public async Task<System.Collections.Generic.IEnumerable<Models.TestModel>> GetTestsAsync(int size, int page=0)
        {
            return await _appDbContext.testModels.Skip(page * size).Take(size).ToListAsync();
        }
        public async Task<bool> CreateTestAsync(TestModel model)
        {
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.AvtorId) && !string.IsNullOrEmpty(model.Title))
                {
                    await _appDbContext.testModels.AddAsync(model);
                    await _appDbContext.SaveChangesAsync();
                    return true;
                }
            }
            _logger.LogWarning($"{DateTime.Now} model ==null");
            return false;
        }

        public async Task<IEnumerable<TestModel>> GetAll()
        {
            return await _appDbContext.testModels.ToListAsync();
        }

        public async Task<TestModel> GetTestByIdAsync(int Id)
        {
            return await _appDbContext.testModels.FirstOrDefaultAsync(o => o.Id == Id);
        }

        public async Task<bool> UpdateAsync(TestModel model)
        {
            try
            {
                _appDbContext.testModels.Update(model);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"{DateTime.Now} updating error {e.Message}");
                return false;
            }
        }

        public async Task<bool> RemoveAsync(int Id)
        {
            try
            {
                var test =await _appDbContext.testModels.FirstOrDefaultAsync(o => o.Id == Id);
                if (test != null)
                {
                    _appDbContext.testModels.Remove(test);
                    await _appDbContext.SaveChangesAsync();
                    return true;
                }

                return false;

            }
            catch (Exception e)
            {
                _logger.LogError($"{DateTime.Now} Remove Error {e.Message}");

                return false;
            }
        }

        public async Task<IEnumerable<TestModel>> GetTestsByAvtorId(string Id,int size,int page=0)
        { 
            return await _appDbContext.testModels.Where(o => o.AvtorId == Id).Skip(page*size).Take(size).ToListAsync();
        }

        public async Task<bool> TestCountAdd(TestModel model)
        {
            try
            {
                model.QuestsCount++;
                _appDbContext.testModels.Update(model);
                await _appDbContext.SaveChangesAsync();
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
                _appDbContext.testModels.Update(model);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError($"{DateTime.Now} Test count take away {e.Message}");
                return false;
            }
        }
        public async Task<TestModel> GetFullTestForPassingAsync(int TestId)
        {
            var result = await _appDbContext.testModels.FirstOrDefaultAsync(o => o.Id == TestId);
            if(result!=null)
                result.Quests = await _appDbContext.Quests.Where(o => o.TestId == TestId).Include(o => o.LeftAnswers).ToListAsync();
            return result;
        }
    }
}
