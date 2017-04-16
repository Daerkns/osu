// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;
using osu.Game.Modes.Square.Beatmaps;
using osu.Game.Modes.Square.Objects;
using System.Collections.Generic;

namespace osu.Game.Modes.Square
{
    public class SquareDifficultyCalculator : DifficultyCalculator<SquareHit>
    {
        public SquareDifficultyCalculator(Beatmap beatmap) : base(beatmap)
        {
        }

        protected override double CalculateInternal(Dictionary<string, string> categoryDifficulty)
        {
            return 0;
        }

        protected override IBeatmapConverter<SquareHit> CreateBeatmapConverter() => new SquareBeatmapConverter();
    }
}