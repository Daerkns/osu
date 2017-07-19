// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Game.Database;
using osu.Game.Screens.Select;
using System.Linq;
using OpenTK;

namespace osu.Desktop.VisualTests.Tests
{
    internal class TestCaseBeatmapDetails : TestCase
    {
        public override string Description => "BeatmapDetails tab of BeatmapDetailArea";

        private readonly BeatmapDetails details;

        public TestCaseBeatmapDetails()
        {
            Add(details = new BeatmapDetails
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(541f, 343f),
            });

            AddStep("beatmap", () => details.Beatmap = new BeatmapInfo
            {
                Version = "Labyrinth Oni",
                Metadata = new BeatmapMetadata
                {
                    Source = "",
                    Tags = "mmbk.com yuzu__rinrin charlotte",
                },
                Difficulty = new BeatmapDifficulty
                {
                    CircleSize = 5,
                    DrainRate = 5,
                    OverallDifficulty = 6,
                    ApproachRate = 10,
                },
                StarDifficulty = 5.08f,
                Metrics = new BeatmapMetrics
                {
                    Ratings = Enumerable.Range(0, 10),
                    Fails = Enumerable.Range(0, 100).Select(i => i % 12 - 6),
                    Retries = Enumerable.Range(-3, 100).Select(i => i % 12 - 6),
                },
            });
            AddStep("null beatmap", () => details.Beatmap = null);
        }
    }
}
