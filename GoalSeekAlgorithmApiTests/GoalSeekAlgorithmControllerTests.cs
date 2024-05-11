using GoalSeekAlgorithm.Server.Controllers;
using GoalSeekAlgorithm.Server.RequestModels;
using GoalSeekAlgorithm.Server.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Dynamic;
using static Microsoft.FSharp.Core.ByRefKinds;

namespace GoalSeekAlgorithmApiTests
{
    public class GoalSeekAlgorithmControllerTests
    {
        [Theory]
        [InlineData("12 * input * input - 6 * input + 55", 3, 317, 20, 4.929298384)]
        [InlineData("2.5 * input", 100, 2500, 10, 1000)]
        [InlineData("-7.2 * input / 3.9 - 9", 1.1, 15.7, 3, -13.379166666)]
        [InlineData("4", 2, 6, 2, 0)]
        public async void GSA_Valid_Cases_Return_Accrate_Root(string formula, double input, double targetResult, int maxIterations,
            decimal expectedAnswer)
        {
            GoalSeekRequestModel requestModel = new GoalSeekRequestModel
            {
                Formula = formula,
                Input = input,
                TargetResult = targetResult,
                MaximumIterations = maxIterations
            };

            ILogger logger = Mock.Of<ILogger>();

            ApiController apiController = new ApiController(logger);

            ActionResult returnedResult = await apiController.GoalSeek(requestModel);

            GoalSeekResponseModel? resModel = ((OkObjectResult)returnedResult).Value as GoalSeekResponseModel;

            double calculatedValue = resModel?.TargetInput ?? 0;

            decimal roundedValue = Math.Round((decimal)calculatedValue, 9);

            Assert.True(roundedValue == expectedAnswer);
        }

        [Theory]
        [InlineData("a * input - 3")]
        [InlineData("5 * input * input - 9 - x")]
        [InlineData("-3 * input + 4r")]
        [InlineData("definitely invalid")]
        public async void GSA_Invalid_Formula_Returns_400_Error(string formula)
        {
            GoalSeekRequestModel requestModel = new GoalSeekRequestModel
            {
                Formula = formula,
                Input = 10,
                TargetResult = 55,
                MaximumIterations = 2
            };

            ILogger logger = Mock.Of<ILogger>();

            ApiController apiController = new ApiController(logger);

            ActionResult returnedResult = await apiController.GoalSeek(requestModel);

            Assert.True(returnedResult is BadRequestObjectResult);
        }

        [Theory]
        [InlineData("2 * input", (dynamic)"", 3, 10)]
        [InlineData("3 * input * input - 9", 1, (dynamic)"20", 4)]
        [InlineData("-9 * input", 10, 457, (dynamic)"x")]
        [InlineData((dynamic)10, (dynamic)"x", 22, (dynamic)"1009")]
        public async void GSA_Invalid_Params_Returns_400(dynamic formula, dynamic input, dynamic targetResult, dynamic maxIterations)
        {
            dynamic requestModel = new ExpandoObject();
            requestModel.Formula = formula;
            requestModel.Input = input;
            requestModel.TargetResult = targetResult;
            requestModel.MaxIterations = maxIterations;

            ILogger logger = Mock.Of<ILogger>();

            ApiController apiController = new ApiController(logger);

            ActionResult returnedResult = await apiController.GoalSeek(requestModel);

            Assert.True(returnedResult is BadRequestObjectResult);
        }
    }
}