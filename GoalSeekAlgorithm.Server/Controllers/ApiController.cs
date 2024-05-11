using GoalSeekAlgorithm.Server.RequestModels;
using Microsoft.AspNetCore.Mvc;
using MathNet.Numerics;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using GoalSeekAlgorithm.Server.ResponseModels;
using Microsoft.CodeAnalysis.Scripting;

namespace GoalSeekAlgorithm.Server.Controllers
{
    /// <summary>
    /// Controller containing endpoint(s) that are involved in providing a suite of operations related with the usuage of the
    /// Goal Seek Algorithm to seek solutions to various equations
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger _logger;
        public ApiController(ILogger logger) 
        {
            _logger = logger;
        }

        /// <summary>
        /// An endpoint method which will attempt to find an approximate root/answer to a specified equation with parameters set
        /// in the <see cref="GoalSeekRequestModel"/> object.
        /// </summary>
        /// <param name="requestModel">A request model containing parameters for executing the Goal Seek algorithm</param>
        /// <returns>
        /// A HTTP Response containing a response object with the answer and number of iterations executed to
        /// reach the correct answer (if applicable)
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> GoalSeek(GoalSeekRequestModel requestModel)
        {
            Func<double, double> fx;

            GoalSeekResponseModel? model = null;

            ActionResult result = Ok();

            try
            {
                double answer = requestModel.Input;
                string formula = requestModel.Formula ?? string.Empty;
                formula += $" - {requestModel.TargetResult}";

                string funcString = "input => " + formula;

                fx = await CSharpScript.EvaluateAsync<Func<double, double>>(funcString);
                int numberOfIterations = 0;
                bool answerFound = false;

                for (int i = 0; i < requestModel.MaximumIterations; i++)
                {
                    // Goal seek algorithm stops when answer is within 0.001 difference from the
                    // desired result
                    if (Math.Abs(fx.Invoke(answer)) > 0.001)
                    {
                        answer = ApplyNewtonsMethod(answer, fx);
                        numberOfIterations++;
                    }
                    else
                    {
                        answerFound = true;
                        break;
                    }
                }

                model = new GoalSeekResponseModel
                {
                    TargetInput = answerFound ? answer : null,
                    IterationsRequired = answerFound ? numberOfIterations : null
                };

                result = Ok(model);
            }
            catch (CompilationErrorException ex)
            {
                _logger.LogError(new EventId(), ex, "Formula is in invalid format. It must only contain one variable that must "
                    + "be called 'input' and it must be a valid mathematical function written using syntax understandable"
                    + " by a C# program");
                result = BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(), ex, ex.Message);
                result = StatusCode(500, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Calculates an approximate root using Newtown's Method
        /// ( i.e. xn+1 = xn - f(xn)/f'(xn) )
        /// </summary>
        /// <param name="xn">The initial guess (input) from where to start looking for a solution</param>
        /// <param name="fx">
        ///     A <see cref="Func&lt;double, double&gt;"/> structure that represents a single variable 
        ///     mathematical function that takes in one numerical value and returns another numerical
        ///     value based on a predefined expression
        /// </param>
        /// <returns>A new approximation for the root of an equation (xn+1)</returns>
        private static double ApplyNewtonsMethod(double xn, Func<double, double> fx)
        {
            try
            {
                var eqVariables = new Dictionary<string, double>()
                {
                    { "input", xn }
                };

                double firstDerivative = Differentiate.FirstDerivative(fx, xn);

                double functionValue = fx.Invoke(xn);

                return xn - (functionValue / firstDerivative);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
