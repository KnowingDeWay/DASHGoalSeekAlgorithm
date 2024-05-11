using GoalSeekAlgorithm.Server.RequestModels;
using Microsoft.AspNetCore.Mvc;
using MathNet.Numerics;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using GoalSeekAlgorithm.Server.ResponseModels;

namespace GoalSeekAlgorithm.Server.Controllers
{
    /// <summary>
    /// Controller containing endpoint(s) that are involved in providing a suite of operations related with the usuage of the
    /// Goal Seek Algorithm to seek solutions to various equations
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        /// <summary>
        /// An endpoint method which will attempt to find an approximate root/answer to a specified equation with parameters set
        /// in the <see cref="GoalSeekRequestModel"/> object.
        /// </summary>
        /// <param name="requestModel">A request model containing parameters for executing the Goal Seek algorithm</param>
        /// <returns>
        /// A HTTP Response containing a response object with the answer and number of iterations executed to
        /// reach the correct answer (if applicable)
        /// </returns>
        [HttpPost(Name = "goalseek")]
        public async Task<ActionResult> GoalSeek(GoalSeekRequestModel requestModel)
        {
            double answer = requestModel.Input;
            string formula = requestModel.Formula ?? string.Empty;
            formula += $" - {requestModel.TargetResult}";

            string funcString = "input => " + formula;
            Func<double, double> fx = await CSharpScript.EvaluateAsync<Func<double, double>>(funcString);

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
            return Ok(new GoalSeekResponseModel
            {
                TargetInput = answerFound ? answer : null,
                IterationsRequired = answerFound ? numberOfIterations : null
            });
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
            var eqVariables = new Dictionary<string, double>()
            {
                { "input", xn }
            };

            double firstDerivative = Differentiate.FirstDerivative(fx, xn);

            double functionValue = fx.Invoke(xn);
             
            return xn - (functionValue / firstDerivative);
        }
    }
}
