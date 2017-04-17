// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
using OpenTK;
using osu.Framework.Graphics.Transforms;
using osu.Game.Modes.Objects.Drawables;
using osu.Game.Modes.Square.Judgements;

namespace osu.Game.Modes.Square.Objects.Drawable
{
    public class DrawableSquare : DrawableHitObject<SquareHitObject, SquareJudgment>
    {
        
        public DrawableSquare(SquareHitObject hitObject)
            : base(hitObject)
        {
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Scale = Vector2.Zero;
            Alpha = 1f;
            Masking = true;
            CornerRadius = 5f;

            Children = new Framework.Graphics.Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            Colour = colours.Blue;

            Transforms.Add(new TransformScale { StartTime = HitObject.StartTime - 400, EndTime = HitObject.StartTime, StartValue = Scale, EndValue = Vector2.One });
            Transforms.Add(new TransformAlpha { StartTime = HitObject.StartTime - 400, EndTime = HitObject.StartTime - 200, StartValue = Alpha, EndValue = 1 });
            Transforms.Add(new TransformAlpha { StartTime = HitObject.StartTime, EndTime = HitObject.StartTime + 100, StartValue = Alpha, EndValue = 0, Easing = EasingTypes.Out });
            Expire(true);
        }

        protected override SquareJudgment CreateJudgement()
        {
            return new SquareJudgment();
        }

        protected override void UpdateState(ArmedState state)
        {
            
        }
    }
}
