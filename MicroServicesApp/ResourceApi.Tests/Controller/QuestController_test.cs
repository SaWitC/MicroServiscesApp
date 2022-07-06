using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ResourceApi.Data.Interfaces;
using ResourceApi.Models;
using ResourceApi.Tests.TestDataReturnFunctions;
using ResourceApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ResourceApi.Tests.Controller
{
    public class QuestController_test
    {

        [Fact]
        public async Task ShouldReturnArrayOfQuests()
        {
            //arrange
            var fixture = new Fixture();
            var LeftAnswer = fixture.Build<LeftAnswer>().With(o => o.QuestId,1).Create();
            var Test = fixture.Build<TestModel>().Without(o => o.Quests).With(o => o.Id, 1).Create();

            List<LeftAnswer> leftAnswers = new List<LeftAnswer>() { LeftAnswer };
            var Quest = fixture.Build<Quest>().With(o=>o.LeftAnswers, leftAnswers).With(o=>o.Id,1).With(o=>o.TestId,1).With(o=>o.test,Test).Create();
            var IQuestMoq = new Mock<IQuestRepository>();
            var ITestMoq = new Mock<ITestRepository>();
            var ILefAnswersMoq = new Mock<ILeftAnswerRepository>();

            var QuestEnumerable = new List<Quest>();
            QuestEnumerable.Add(Quest);

            var jsonResoult = JsonSerializer.Serialize(QuestEnumerable);

            IQuestMoq.Setup(repo => repo.GetQuestsByTestId(1)).Returns(() => { return QuestData.ReturnQuests(Quest); });
            var Loger = new Mock<ILogger<ResourceApi.Controllers.QuestsController>>();
            //act
            var controller = new Controllers.QuestsController(IQuestMoq.Object,ITestMoq.Object,ILefAnswersMoq.Object, Loger.Object);
            //assert
            var result = await controller.GetQuests(1);
            Assert.NotNull(result);
            var model = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(jsonResoult, model.Value);
        }

        //[Fact]
        //public async Task ShouldReturnBadRequest()
        //{
        //    //arrange
        //    var fixture = new Fixture();
        //    var LeftAnswer = fixture.Build<LeftAnswer>().With(o => o.QuestId, 1).Create();
        //    //var Quest = fixture.Build<Quest>().Without(o => o.LeftAnswers).With(o => o.Id, 1).Create();

        //    List<LeftAnswer> leftAnswers = new List<LeftAnswer>() { LeftAnswer };
        //    var CreateQuestVm = fixture.Build<CreateQuestVM>().Create();
        //    var Quest = fixture.Build<Quest>().Without(o=>o.LeftAnswers).With(o=>o.Id,1).With(o=>o.TestId,1).Create();


        //    var IQuestMoq = new Mock<IQuestRepository>();
        //    var ITestMoq = new Mock<ITestRepository>();
        //    var ILefAnswersMoq = new Mock<ILeftAnswerRepository>();

        //    var QuestEnumerable = new List<CreateQuestVM>();
        //    QuestEnumerable.Add(CreateQuestVm);

        //    var jsonResoult = JsonSerializer.Serialize(QuestEnumerable);

        //    IQuestMoq.Setup(repo => repo.GetQuestByid(1)).Returns(() => { return QuestData.ReturnQuest(Quest, 1); });

        //    var Loger = new Mock<ILogger<ResourceApi.Controllers.QuestsController>>();
        //    //act
        //    var controller = new Controllers.QuestsController(IQuestMoq.Object, ITestMoq.Object, ILefAnswersMoq.Object, Loger.Object);
        //    //assert
        //    var result = await controller.Create(CreateQuestVm, 1);
        //    Assert.NotNull(result);
        //    var model = Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal(jsonResoult, model.Value);
        //}
    }
}
