// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using Newtonsoft.Json;

namespace osu.Game.Database
{
    /// <summary>
    /// Beatmap info retrieved for previewing locally without having the beatmap downloaded.
    /// </summary>
    public class BeatmapOnlineInfo
    {
        /// <summary>
        /// The length in milliseconds of this beatmap's song.
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// The beats per minute of this beatmap's song.
        /// </summary>
        public double BPM { get; set; }

        /// <summary>
        /// The amount of circles in this beatmap.
        /// </summary>
        public int CircleCount { get; set; }

        /// <summary>
        /// The amount of sliders in this beatmap.
        /// </summary>
        public int SliderCount { get; set; }

        /// <summary>
        /// The percentage of people that played this map that cleared it.
        /// </summary>
        public int SuccessRate { get; set; }

        /// <summary>
        /// The amount of plays this beatmap has.
        /// </summary>
        [JsonProperty(@"playcount")]
        public int PlayCount { get; set; }

        /// <summary>
        /// The amount of passes this beatmap has.
        /// </summary>
        [JsonProperty(@"passcount")]
        public int PassCount { get; set; }
    }
}
