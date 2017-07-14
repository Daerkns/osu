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
                    Source = @"",
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
                        Metadata = new BeatmapMetadata
                        {
                            Tags = @"mmbk.com yuzu__rinrin charlotte",
                        },
                    },
                    new BeatmapInfo
                    {
                        Version = @"Futsuu",
                        Ruleset = ruleset,
                        StarDifficulty = 2.23f,
                    },
                    new BeatmapInfo
                    {
                        Version = @"Muzukashii",
                        Ruleset = ruleset,
                        StarDifficulty = 3.19f,
                    },
                    new BeatmapInfo
                    {
                        Version = @"Charlotte's Oni",
                        Ruleset = ruleset,
                        StarDifficulty = 3.97f,
                    },
                    new BeatmapInfo
                    {
                        Version = @"Labyrinth Oni",
                        Ruleset = ruleset,
                        StarDifficulty = 5.08f,
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
