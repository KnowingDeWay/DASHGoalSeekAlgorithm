using GoalSeekAlgorithm.Server.Controllers;
using GoalSeekAlgorithm.Server.RequestModels;
using GoalSeekAlgorithm.Server.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

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
    }
}