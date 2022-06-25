using ResourceApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceApi.Data.Interfaces
{
    public interface ITestRepository
    { 
        Task<IEnumerable<TestModel>> GetTestsAsync(int size, int page);
        Task<bool> CreateTestAsync(TestModel model);
        Task<IEnumerable<TestModel>> GetAll();
        Task<TestModel> GetTestByIdAsync(int Id);
        Task<bool> UpdateAsync(TestModel model);
        Task<bool> RemoveAsync(int Id);
        Task<IEnumerable<TestModel>> GetTestsByAvtorId(string Id,int size, int page = 0);

        public Task<bool> TestCountAdd(TestModel model);
        public Task<bool> TestCountTake_Away(TestModel model);

        public Task<TestModel> GetFullTestForPassingAsync(int TestId);



    }
}
