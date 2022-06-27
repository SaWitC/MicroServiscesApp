using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResourceApi.Data.Interfaces;

namespace ResourceApi.Controllers
{
    public class BaseController<T>:ControllerBase
    {
        //public ITestRepository _testRepository;

        protected readonly IQuestRepository _questRepository;
        protected readonly ITestRepository _testRepository;
        protected readonly ILogger<T> _logger;
        protected readonly ILeftAnswerRepository _leftAnswerRepository;
        public BaseController(ITestRepository testRepository,
            IQuestRepository questRepository,
            ILeftAnswerRepository leftAnswerRepository,
            ILogger<T> logger)
        {
            this._testRepository = testRepository;
            this._questRepository = questRepository;
            this._logger = logger;
            this._leftAnswerRepository = leftAnswerRepository;
        }
    }
}
