// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;
using osu.Game.Modes.Square.Objects;
using System.Collections.Generic;
using osu.Game.Modes.Objects;
using osu.Framework.MathUtils;

namespace osu.Game.Modes.Square.Beatmaps
{
    internal class SquareBeatmapConverter : IBeatmapConverter<SquareHitObject>
    {
        public Beatmap<SquareHitObject> Convert(Beatmap original)
        {
            var objs = new List<SquareHitObject>();
            foreach (HitObject obj in original.HitObjects)
            {
                //todo: Probably don't use random rows/columns
                objs.Add(new SquareHitObject { StartTime = obj.StartTime, Column = RNG.Next(0, 4), Row = RNG.Next(0, 4) });
            }

            return new Beatmap<SquareHitObject>(original)
            {
                HitObjects = objs
            };
        }
    }
}
