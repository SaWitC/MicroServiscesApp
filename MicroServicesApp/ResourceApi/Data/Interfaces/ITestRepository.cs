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
        Task<TestModel> GetTestNyIdAsync(int Id);

        Task<bool> UpdateAsync(TestModel model);
        Task<bool> RemoveAsync(int Id);


    }
}
