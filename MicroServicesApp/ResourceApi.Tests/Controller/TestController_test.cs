using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ResourceApi.Data.Interfaces;
using ResourceApi.Models;
using ResourceApi.Tests.TestDataReturnFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ResourceApi.Tests.Controller
{
    public class TestController_test
    {
        [Fact]
        public async Task ShouldReturnArrayOfTests()
        {
            //arrange
            var fixture = new Fixture();
            var Tests = fixture.Build<TestModel>().Without(o => o.Quests).Create();
            var ItestMoq = new Mock<ITestRepository>();
            
            var TestEnumerable = new List<TestModel>();
            TestEnumerable.Add(Tests);

            var jsonResoult = JsonSerializer.Serialize(TestEnumerable);

            ItestMoq.Setup(repo => repo.GetTestsAsync(6, 0)).Returns(() => { return TestData.ReturnTests(Tests); });
            var Loger = new Mock<ILogger<ResourceApi.Controllers.TestsController>>();
            //act
            var controller = new ResourceApi.Controllers.TestsController(ItestMoq.Object, Loger.Object);
            //assert
            var result = await controller.GetTests();
            Assert.NotNull(result);
            var model =Assert.IsType<OkObjectResult>(result);
            Assert.Equal(jsonResoult, model.Value);
        }

        [Fact]
        public async Task ShouldReturnTestById()
        {
            //arrange
            var fixture = new Fixture();
            var Test = fixture.Build<TestModel>().Without(o => o.Quests).With(o=>o.Id,1).Create();
            var ItestMoq = new Mock<ITestRepository>();

            ItestMoq.Setup(repo => repo.GetTestByIdAsync(1)).Returns(() => { return TestData.ReturnTest(Test,1); });
            var Loger = new Mock<ILogger<ResourceApi.Controllers.TestsController>>();
            //act
            var controller = new ResourceApi.Controllers.TestsController(ItestMoq.Object, Loger.Object);
            //assert
            var result = await controller.GetTest(1);
            Assert.NotNull(result);
            var model = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(Test, model.Value);
        }

        [Fact]
        public async Task ShouldReturnZeroOfTests()
        {
            //arrange
            var fixture = new Fixture();
            var Test = fixture.Build<TestModel>().Without(o => o.Quests).With(o => o.Id, 1).Create();
            var ItestMoq = new Mock<ITestRepository>();

            ItestMoq.Setup(repo => repo.GetTestByIdAsync(1)).Returns(() => { return TestData.ReturnTest(Test, 2); });
            var Loger = new Mock<ILogger<ResourceApi.Controllers.TestsController>>();
            //act
            var controller = new ResourceApi.Controllers.TestsController(ItestMoq.Object, Loger.Object);
            //assert
            var result = await controller.GetTest(1);
            Assert.NotNull(result);
            var model = Assert.IsType<OkObjectResult>(result);
            Assert.NotEqual(Test, model.Value);
            Assert.Null(model.Value);
        }
        [Fact]
        public async Task ShouldReturnStatusBadRequest()
        {
            //arrange 
            var fixture = new Fixture();
            var Test = fixture.Build<TestModel>().Without(o => o.Quests).With(o => o.Id, 1).With(o => o.Title, "").Create();
            var ItestMoq = new Mock<ITestRepository>();

            ItestMoq.Setup(repo => repo.CreateTestAsync(Test)).Returns(() => { return TestData.CheckModelValidation(Test); });
            var Loger = new Mock<ILogger<ResourceApi.Controllers.TestsController>>();
            //act
            var controller = new ResourceApi.Controllers.TestsController(ItestMoq.Object, Loger.Object);
            controller.id = Guid.NewGuid();
            //assert
            var result = await controller.CreateTest(Test);
            //Assert.Equal(await TestData.CheckModelValidation(Test),false);
            Assert.IsType<BadRequestObjectResult>(result);
            //Assert.NotEqual(Test, model.Value);
            //Assert.Null(model.Value);
        }
    }
}
