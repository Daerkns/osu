// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Testing;
using osu.Game.Database;
using osu.Game.Overlays;
using osu.Game.Users;

namespace osu.Desktop.VisualTests.Tests
{
    internal class TestCaseBeatmapSetInspector : TestCase
    {
        public override string Description => @"inspect online beatmaps";

        private BeatmapSetInspectorOverlay inspector;

        [BackgroundDependencyLoader]
        private void load(RulesetDatabase rulesets)
        {
            var ruleset = rulesets.GetRuleset(1);
            var set = new BeatmapSetInfo
            {
                Metadata = new BeatmapMetadata
                {
                    Title = @"Soumatou Labyrinth",
                    Artist = @"Yunomi with Momobako&miko",
                    Author = @"komasy",
                    Source = @"This doesn't actually have a source, but hey, I'm not judging",
                    Tags = @"mmbk.com yuzu__rinrin charlotte",
                },
                OnlineInfo = new BeatmapSetOnlineInfo
                {
                    Author = new User
                    {
                        Username = @"komasy",
                        Id = 1980256,
                    },
                    Submitted = new DateTime(2017, 6, 11),
                    Ranked = new DateTime(2017, 7, 12),
                    HasVideo = false,
                    Ratings = new[] { 0, 0, 0, 0, 0, 0, 2, 2, 6, 25 },
                    BPM = 160,
                    Covers = new BeatmapSetOnlineCovers
                    {
                        Cover = @"https://assets.ppy.sh/beatmaps/625493/covers/cover.jpg?1499167472",
                    },
                    Preview = @"https://b.ppy.sh/preview/625493.mp3",
                    PlayCount = 1917,
                    FavouriteCount = 18,
                },
                Beatmaps = new List<BeatmapInfo>
                {
                    new BeatmapInfo
                    {
                        Version = @"yzrin's Kantan",
                        Ruleset = ruleset,
                        StarDifficulty = 1.40f,
                        Difficulty = new BeatmapDifficulty
                        {
                            CircleSize = 2,
                            DrainRate = 7,
                            OverallDifficulty = 3,
                            ApproachRate = 10,
                        },
                        Metrics = new BeatmapMetrics
                        {
                            Fails = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                            Retries = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 },
                        },
                        OnlineInfo = new BeatmapOnlineInfo
                        {
                            Length = 193000,
                            CircleCount = 262,
                            SliderCount = 0,
                            PlayCount = 100,
                            PassCount = 24,
                        },
                    },
                    new BeatmapInfo
                    {
                        Version = @"Futsuu",
                        Ruleset = ruleset,
                        StarDifficulty = 2.23f,
                        Difficulty = new BeatmapDifficulty
                        {
                            CircleSize = 2,
                            DrainRate = 6,
                            OverallDifficulty = 4,
                            ApproachRate = 10,
                        },
                        Metrics = new BeatmapMetrics
                        {
                            Fails = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                            Retries = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 },
                        },
                        OnlineInfo = new BeatmapOnlineInfo
                        {
                            Length = 193000,
                            CircleCount = 464,
                            SliderCount = 0,
                            PlayCount = 100,
                            PassCount = 16,
                        },
                    },
                    new BeatmapInfo
                    {
                        Version = @"Muzukashii",
                        Ruleset = ruleset,
                        StarDifficulty = 3.19f,
                        Difficulty = new BeatmapDifficulty
                        {
                            CircleSize = 2,
                            DrainRate = 6,
                            OverallDifficulty = 5,
                            ApproachRate = 10,
                        },
                        Metrics = new BeatmapMetrics
                        {
                            Fails = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                            Retries = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 },
                        },
                        OnlineInfo = new BeatmapOnlineInfo
                        {
                            Length = 193000,
                            CircleCount = 712,
                            SliderCount = 0,
                            PlayCount = 100,
                            PassCount = 16,
                        },
                    },
                    new BeatmapInfo
                    {
                        Version = @"Charlotte's Oni",
                        Ruleset = ruleset,
                        StarDifficulty = 3.97f,
                        Difficulty = new BeatmapDifficulty
                        {
                            CircleSize = 5,
                            DrainRate = 6,
                            OverallDifficulty = 5.5f,
                            ApproachRate = 10,
                        },
                        Metrics = new BeatmapMetrics
                        {
                            Fails = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                            Retries = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 },
                        },
                        OnlineInfo = new BeatmapOnlineInfo
                        {
                            Length = 193000,
                            CircleCount = 943,
                            SliderCount = 0,
                            PlayCount = 100,
                            PassCount = 15,
                        },
                    },
                    new BeatmapInfo
                    {
                        Version = @"Labyrinth Oni",
                        Ruleset = ruleset,
                        StarDifficulty = 5.08f,
                        Difficulty = new BeatmapDifficulty
                        {
                            CircleSize = 5,
                            DrainRate = 5,
                            OverallDifficulty = 6,
                            ApproachRate = 10,
                        },
                        Metrics = new BeatmapMetrics
                        {
                            Fails = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                            Retries = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 },
                        },
                        OnlineInfo = new BeatmapOnlineInfo
                        {
                            Length = 193000,
                            CircleCount = 1068,
                            SliderCount = 0,
                            PlayCount = 100,
                            PassCount = 17,
                        },
                    },
                },
            };

            Add(inspector = new BeatmapSetInspectorOverlay(set));
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            AddStep(@"toggle", inspector.ToggleVisibility);
        }
    }
}
