// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Modes.Square.Judgements;
using osu.Game.Modes.Square.Objects;
using osu.Game.Modes.Scoring;
using osu.Game.Modes.UI;

namespace osu.Game.Modes.Square.Scoring
{
    internal class SquareScoreProcessor : ScoreProcessor<SquareHit, SquareJudgment>
    {
        public SquareScoreProcessor()
        {
        }

        public SquareScoreProcessor(HitRenderer<SquareHit, SquareJudgment> hitRenderer)
            : base(hitRenderer)
        {
        }

        protected override void OnNewJudgement(SquareJudgment judgement)
        {
        }
    }
}
