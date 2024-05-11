using GoalSeekAlgorithm.Server.RequestModels;
using Microsoft.AspNetCore.Mvc;
using MathNet.Numerics;

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
        public ActionResult GoalSeek(GoalSeekRequestModel requestModel)
        {
            int answer = requestModel.Input;
            for (int i = 0; i < requestModel.MaximumIterations; i++)
            {
                answer = ApplyNewtonsMethod(answer);
            }
            return Ok(answer);
        }

        /// <summary>
        /// Calculates an approximate root using Newtown's Method
        /// ( i.e. xn+1 = xn - f(xn)/f'(xn) )
        /// </summary>
        /// <param name="xn">Initial Guess</param>
        /// <returns>A new approximation for the root of an equation (xn+1)</returns>
        private int ApplyNewtonsMethod(int xn)
        {
            return 0;
        }
    }
}
