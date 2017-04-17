// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Extensions;
using osu.Game.Modes.Judgements;
using osu.Game.Modes.Square.Objects.Drawable;

namespace osu.Game.Modes.Square.Judgements
{
    public class SquareJudgement : Judgement
    {
        public SquareHitResult Score;
        public override string ResultString => Score.GetDescription();
        public override string MaxResultString => SquareHitResult.Perfect.GetDescription();
    }
}
