// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;
using osu.Game.Modes.Replays;
using osu.Game.Modes.Square.Objects;

namespace osu.Game.Modes.Square
{
    public class SquareAutoReplay : Replay
    {
        private readonly Beatmap<SquareHitObject> beatmap;

        public SquareAutoReplay(Beatmap<SquareHitObject> beatmap)
        {
        	this.beatmap = beatmap;
        	createAutoReplay();
        }

        private void createAutoReplay()
        {
            
        }
    }
}
