using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GoalSeekAlgorithm.Server.RequestModels
{
    public class GoalSeekRequestModel
    {
        [JsonPropertyName("formula")]
        public string? Formula { get; set; }

        [JsonPropertyName("input")]
        public int Input { get; set; }

        [JsonPropertyName("targetResult")]
        public int TargetResult { get; set; }

        [JsonPropertyName("maximumIterations")]
        public int MaximumIterations { get; set; }
    }
}
