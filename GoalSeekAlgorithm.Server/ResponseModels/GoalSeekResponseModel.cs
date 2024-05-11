using Newtonsoft.Json;

namespace GoalSeekAlgorithm.Server.ResponseModels
{
    public class GoalSeekResponseModel
    {
        [JsonProperty("targetInput")]
        public double? TargetInput { get; set; }

        [JsonProperty("iterationsRequired")]
        public int? IterationsRequired { get; set; }
    }
}
