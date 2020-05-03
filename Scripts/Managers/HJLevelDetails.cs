using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace HelixJump
{
    public class HJLevelDetails
    {
        [JsonProperty(HJGameConstants.kLevelKey)]
        public int LevelId { get; set; }

        [JsonProperty(HJGameConstants.kDistanceKey)]
        public float DistanceBetPlatform { get; set; }

        [JsonProperty(HJGameConstants.kBackgroundColorKey)]
        public string BackGroundColor { get; set; }

        [JsonProperty(HJGameConstants.kPoleColorKey)]
        public string PoleColor { get; set; }

        [JsonProperty(HJGameConstants.kBasePlatformColorKey)]
        public string BasePlatformSliceColor { get; set; }

        [JsonProperty(HJGameConstants.kBallColorKey)]
        public string BallColor { get; set; }

        [JsonProperty(HJGameConstants.kDeathBaseColor)]
        public string DeathBaseColor { get; set; }


        [JsonProperty(HJGameConstants.kScoreUnitKey)]
        public int ScoreUnit { get; set; }

        [JsonProperty(HJGameConstants.kPlatformDetailKey)]
        public List<HJPlatformDetails> PlatformDetails { get; set; }
    }

    public class HJPlatformDetails
    {
        [JsonProperty(HJGameConstants.kBaseSliceCountKey)]
        public int BaseSliceCount { get; set; }

        [JsonProperty(HJGameConstants.kDeathSliceCountKey)]
        public int DeathSliceCount { get; set; }
    }
}
