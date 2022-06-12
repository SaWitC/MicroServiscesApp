using Microsoft.AspNetCore.Mvc;
using ResourceApi.Data.Interfaces;

namespace ResourceApi.Controllers
{
    public class BaseController:ControllerBase
    {
        public ITestRepository _testRepository;

        public BaseController(ITestRepository testRepository)
        {
            this._testRepository = testRepository;
        }
    }
}
