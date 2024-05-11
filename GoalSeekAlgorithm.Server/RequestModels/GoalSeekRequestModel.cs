﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GoalSeekAlgorithm.Server.RequestModels
{
    public class GoalSeekRequestModel
    {
        [JsonProperty("formula")]
        public string? Formula { get; set; }

        [JsonProperty("input")]
        public double Input { get; set; }

        [JsonProperty("targetResult")]
        public double TargetResult { get; set; }

        [JsonProperty("maximumIterations")]
        public int MaximumIterations { get; set; }
    }
}
