// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using osu.Game.Beatmaps;
using osu.Game.Rulesets;
using osu.Game.Users;

namespace osu.Game.Online.API.Requests
{
    public class GetBeatmapSetsResponse : BeatmapMetadata
    {
        [JsonProperty(@"id")]
        private int onlineId { get; set; }

        [JsonProperty(@"play_count")]
        private int playCount { get; set; }

        [JsonProperty(@"favourite_count")]
        private int favouriteCount { get; set; }

        [JsonProperty(@"has_favourited")]
        private bool favourited { get; set; }

        [JsonProperty(@"submitted_date")]
        private string submittedDate { get; set; }

        [JsonProperty(@"last_updated")]
        private string lastUpdated { get; set; }

        [JsonProperty(@"ranked_date")]
        private string rankedDate { get; set; }

        [JsonProperty(@"creator")]
        private string creatorUsername { get; set; }

        [JsonProperty(@"user_id")]
        private long creatorId = 1;

        [JsonProperty(@"bpm")]
        private double bpm { get; set; }

        [JsonProperty(@"covers")]
        private BeatmapSetOnlineCovers covers { get; set; }

        [JsonProperty(@"preview_url")]
        private string preview { get; set; }

        [JsonProperty(@"video")]
        private bool hasVideo { get; set; }

        [JsonProperty(@"status")]
        private string status { get; set; }

        [JsonProperty(@"beatmaps")]
        private IEnumerable<GetBeatmapSetsBeatmapResponse> beatmaps { get; set; }

        public BeatmapSetInfo ToBeatmapSet(RulesetStore rulesets)
        {
            return new BeatmapSetInfo
            {
                OnlineBeatmapSetID = onlineId,
                Metadata = this,
                OnlineInfo = new BeatmapSetOnlineInfo
                {
                    Author = new User
                    {
                        Id = creatorId,
                        Username = creatorUsername,
                    },
                    Preview = @"https:" + preview,
                    IsRanked = status == @"ranked",
                    IsFavourited = favourited,
                    PlayCount = playCount,
                    FavouriteCount = favouriteCount,
                    SubmittedDate = DateTimeOffset.Parse(submittedDate),
                    LastUpdated = DateTimeOffset.Parse(lastUpdated),
                    RankedDate = DateTimeOffset.Parse(rankedDate),
                    BPM = bpm,
                    Covers = covers,
                },
                Beatmaps = beatmaps.Select(b =>
                {
                    var beatmap = b.ToBeatmap(rulesets);
                    beatmap.OnlineInfo.HasVideo = hasVideo;
                    return beatmap;
                }).OrderBy(b => b.Ruleset.ID).ThenBy(b => b.StarDifficulty).ToList(),
            };
        }

        private class GetBeatmapSetsBeatmapResponse : BeatmapMetadata
        {
            [JsonProperty(@"id")]
            private int onlineId { get; set; }

            [JsonProperty(@"playcount")]
            private int playCount { get; set; }

            [JsonProperty(@"passcount")]
            private int passCount { get; set; }

            [JsonProperty(@"mode_int")]
            private int ruleset { get; set; }

            [JsonProperty(@"difficulty_rating")]
            private double starDifficulty { get; set; }

            [JsonProperty(@"version")]
            private string version { get; set; }

            [JsonProperty(@"total_length")]
            private double length { get; set; }

            [JsonProperty(@"cs")]
            private float circleSize { get; set; }

            [JsonProperty(@"drain")]
            private float drainRate { get; set; }

            [JsonProperty(@"accuracy")]
            private float overallDifficulty { get; set; }

            [JsonProperty(@"ar")]
            private float approachRate { get; set; }

            [JsonProperty(@"count_circles")]
            private int circleCount { get; set; }

            [JsonProperty(@"count_sliders")]
            private int sliderCount { get; set; }

            public BeatmapInfo ToBeatmap(RulesetStore rulesets)
            {
                return new BeatmapInfo
                {
                    OnlineBeatmapID = onlineId,
                    Metadata = this,
                    Version = version,
                    Ruleset = rulesets.GetRuleset(ruleset),
                    StarDifficulty = starDifficulty,
                    OnlineInfo = new BeatmapOnlineInfo
                    {
                        Length = length * 1000,
                        CircleCount = circleCount,
                        SliderCount = sliderCount,
                        PlayCount = playCount,
                        PassCount = passCount,
                    },
                    Difficulty = new BeatmapDifficulty
                    {
                        CircleSize = circleSize,
                        DrainRate = drainRate,
                        OverallDifficulty = overallDifficulty,
                        ApproachRate = approachRate,
                    },
                };
            }
        }
    }
}
