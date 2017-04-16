// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;
using osu.Game.Modes.Square.Objects;
using System.Collections.Generic;

namespace osu.Game.Modes.Square.Beatmaps
{
    internal class SquareBeatmapConverter : IBeatmapConverter<SquareHit>
    {
        public Beatmap<SquareHit> Convert(Beatmap original)
        {
            return new Beatmap<SquareHit>(original)
            {
                HitObjects = new List<SquareHit>() //todo: Convert HitObjects
            };
        }
    }
}
