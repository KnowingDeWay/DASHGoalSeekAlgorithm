using GoalSeekAlgorithm.Server.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace GoalSeekAlgorithm.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
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

        private int ApplyNewtonsMethod(int x)
        {
            return 0;
        }
    }
}
