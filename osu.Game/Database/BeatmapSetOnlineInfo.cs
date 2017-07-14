﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using Newtonsoft.Json;
using osu.Game.Users;

namespace osu.Game.Database
{
    /// <summary>
    /// Beatmap set info retrieved for previewing locally without having the set downloaded.
    /// </summary>
    public class BeatmapSetOnlineInfo
    {
        /// <summary>
        /// The author of this beatmap set.
        /// </summary>
        public User Author { get; set; }

        /// <summary>
        /// The date this beatmap set was submitted.
        /// </summary>
        public DateTimeOffset Submitted { get; set; }

        /// <summary>
        /// The date this beatmap set was ranked.
        /// </summary>
        public DateTimeOffset Ranked { get; set; }

        /// <summary>
        /// Whether or not this beatmap set has a background video.
        /// </summary>
        public bool HasVideo { get; set; }

        /// <summary>
        /// The different sizes of cover art for this beatmap set.
        /// </summary>
        [JsonProperty(@"covers")]
        public BeatmapSetOnlineCovers Covers { get; set; }

        /// <summary>
        /// A small sample clip of this beatmap set's song.
        /// </summary>
        [JsonProperty(@"previewUrl")]
        public string Preview { get; set; }

        /// <summary>
        /// The amount of plays this beatmap set has.
        /// </summary>
        [JsonProperty(@"play_count")]
        public int PlayCount { get; set; }

        /// <summary>
        /// The amount of people who have favourited this beatmap set.
        /// </summary>
        [JsonProperty(@"favourite_count")]
        public int FavouriteCount { get; set; }
    }

    public class BeatmapSetOnlineCovers
    {
        public string CoverLowRes { get; set; }

        [JsonProperty(@"cover@2x")]
        public string Cover { get; set; }

        public string CardLowRes { get; set; }

        [JsonProperty(@"card@2x")]
        public string Card { get; set; }

        public string ListLowRes { get; set; }

        [JsonProperty(@"list@2x")]
        public string List { get; set; }
    }
}
