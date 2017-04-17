// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;
using osu.Game.Modes.Square.Objects;
using System.Collections.Generic;
using osu.Game.Modes.Objects;
using osu.Framework.MathUtils;
using osu.Game.Modes.Objects.Types;

namespace osu.Game.Modes.Square.Beatmaps
{
    internal class SquareBeatmapConverter : IBeatmapConverter<SquareHitObject>
    {
        public Beatmap<SquareHitObject> Convert(Beatmap original)
        {
            var objs = new List<SquareHitObject>();
            foreach (HitObject obj in original.HitObjects)
            {
                var distanceData = obj as IHasDistance;
                var repeatsData = obj as IHasRepeats;
                var endTimeData = obj as IHasEndTime;

                if (distanceData != null && repeatsData != null && endTimeData != null) //slider, 2-3 simultaneous hits
                    objs.AddRange(hitsWithRange(obj, 2, 3));
                else if (endTimeData != null) //spinner, 4-5 simultaneous hits
                    objs.AddRange(hitsWithRange(obj, 4, 5));
                else //hit circle, 1 hit
                    objs.AddRange(hitsWithRange(obj, 1, 1));
            }

            return new Beatmap<SquareHitObject>(original)
            {
                HitObjects = objs
            };
        }

        private IEnumerable<SquareHitObject> hitsWithRange(HitObject baseObj, int lower, int higher)
        {
            int count = RNG.Next(lower, higher);
            List<SquareHitObject> objects = new List<SquareHitObject>();

            for (int i = 0; i < count; i++)
            {
                objects.Add(new SquareHitObject { StartTime = baseObj.StartTime, Column = RNG.Next(0, 4), Row = RNG.Next(0, 4) });
            }

            return objects;
        }
    }
}
