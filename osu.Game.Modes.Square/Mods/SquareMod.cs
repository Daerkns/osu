// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;
using osu.Game.Modes.Mods;
using osu.Game.Modes.Scoring;
using osu.Game.Modes.Square.Objects;
using osu.Game.Users;

namespace osu.Game.Modes.Square.Mods
{
    public class SquareModNoFail : ModNoFail
    {

    }

    public class SquareModEasy : ModEasy
    {

    }

    public class SquareModHardRock : ModHardRock
    {
        public override double ScoreMultiplier => 1.12;
        public override bool Ranked => true;
    }

    public class SquareModSuddenDeath : ModSuddenDeath
    {

    }

    public class SquareModDoubleTime : ModDoubleTime
    {
        public override double ScoreMultiplier => 1.06;
    }

    public class SquareModHalfTime : ModHalfTime
    {
        public override double ScoreMultiplier => 0.5;
    }

    public class SquareModNightcore : ModNightcore
    {
        public override double ScoreMultiplier => 1.06;
    }

    public class SquareModPerfect : ModPerfect
    {

    }

    public class SquareModAutoplay : ModAutoplay<SquareHitObject>
    {
    	protected override Score CreateReplayScore(Beatmap<SquareHitObject> beatmap) => new Score
    	{
    		User = new User { Username = "mayumi" },
    		Replay = new SquareAutoReplay(beatmap)
    	};
    }
}
