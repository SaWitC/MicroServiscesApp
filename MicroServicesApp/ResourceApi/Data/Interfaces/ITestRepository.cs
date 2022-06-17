using ResourceApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceApi.Data.Interfaces
{
    public interface ITestRepository
    {
        public Task<IEnumerable<TestModel>> GetTestsAsync(int size, int page);
        public Task<bool> CreateTestAsync(TestModel model);
    }
}
