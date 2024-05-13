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

        // The following inline data expected values come from an online newton's method calculator...
        // see: https://planetcalc.com/7748/
        [Theory]
        [InlineData("12 * input * input - 6 * input + 55", 3, 317, 20, 4.929298384)]
        [InlineData("2.5 * input", 100, 2500, 10, 1000)]
        [InlineData("-7.2 * input / 3.9 - 9", 1.1, 15.7, 3, -13.379166667)]
        [InlineData("4", 2, 6, 2, 0)]
        [InlineData("1/input - input * input + 5", 9, -15.75, 10, 4.579124627)]
        public async void GSA_Valid_Cases_Return_Accrate_Root(string formula, double input, double targetResult, int maxIterations,
            decimal expectedAnswer)
        {
            // Different implementations of Newton's method across the web deliver slightly different
            // results due to various differing implmentations of differentiation functionality among
            // other possible factors. To accommodate for this, a tolerance factor of ±0.001% has been
            // applied from the 'expected' answer
            decimal tolerance = 0.00001M;

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

            Assert.True(Math.Abs(expectedAnswer - roundedValue) <= Math.Abs(expectedAnswer * tolerance));
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
    }
}