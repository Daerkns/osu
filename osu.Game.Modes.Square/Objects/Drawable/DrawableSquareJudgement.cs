// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Modes.Objects.Drawables;
using OpenTK;
using osu.Game.Modes.Judgements;
using osu.Game.Modes.Square.Judgements;

namespace osu.Game.Modes.Square.Objects.Drawables
{
    public class DrawableSquareJudgement : DrawableJudgement<SquareJudgement>
	{
        
        public DrawableSquareJudgement(SquareJudgement judgement)
            : base(judgement)
		{
		}

		protected override void LoadComplete()
		{
			if (Judgement.Result != HitResult.Miss)
				JudgementText.TransformSpacingTo(new Vector2(14, 0), 1800, EasingTypes.OutQuint);

            base.LoadComplete();
        }
	}
}
