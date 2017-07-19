// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using Newtonsoft.Json;
using osu.Game.Database;

namespace osu.Game.Online.API.Requests
{
    public class GetBeatmapDetailsRequest : APIRequest<GetBeatmapDetailsResponse>
    {
        private readonly BeatmapInfo beatmap;

        private string lookupString => beatmap.OnlineBeatmapID > 0 ? beatmap.OnlineBeatmapID.ToString() : $@"lookup?checksum={beatmap.Hash}&filename={beatmap.Path}";

        public GetBeatmapDetailsRequest(BeatmapInfo beatmap)
        {
            this.beatmap = beatmap;
        }

        protected override string Target => $@"beatmaps/{lookupString}";
    }

    public class GetBeatmapDetailsResponse : BeatmapOnlineInfo
    {
        [JsonProperty(@"mode_int")]
        private int mode { get; set; }

        [JsonProperty(@"difficulty_size")]
        private float difficultySize { get; set; }

        [JsonProperty(@"difficulty_rating")]
        private float starRating { get; set; }

        [JsonProperty(@"version")]
        private string version { get; set; }

        [JsonProperty(@"drain")]
        private float drainRate { get; set; }

        [JsonProperty(@"accuracy")]
        private float overallDifficulty { get; set; }

        [JsonProperty(@"ar")]
        private float approachRate { get; set; }

        [JsonProperty(@"failtimes")]
        private BeatmapMetrics failTimes { get; set; }

        [JsonProperty(@"beatmapset")]
        private GetBeatmapSetsResponse set { get; set; }

        public BeatmapMetrics ToMetrics()
        {
            return new BeatmapMetrics
            {
                Ratings = set.Ratings,
                Fails = failTimes.Fails,
                Retries = failTimes.Retries,
            };
        }

        public BeatmapInfo ToBeatmap(RulesetDatabase rulesets)
        {
            return new BeatmapInfo
            {
                Ruleset = rulesets.GetRuleset(mode),
                Version = version,
                StarDifficulty = starRating,
                BeatmapSet = set.ToBeatmapSet(rulesets, false),
                Metrics = failTimes,
                Difficulty = new BeatmapDifficulty
                {
                    DrainRate = drainRate,
                    CircleSize = difficultySize,
                    OverallDifficulty = overallDifficulty,
                    ApproachRate = approachRate,
                },
            };
        }
    }
}
