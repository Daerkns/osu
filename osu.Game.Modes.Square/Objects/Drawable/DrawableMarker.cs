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
using System.ComponentModel;
using System;
using osu.Framework.Input;
using OpenTK.Input;
using OpenTK.Graphics;
using osu.Framework.Audio;

namespace osu.Game.Modes.Square.Objects.Drawable
{
    public class DrawableMarker : DrawableHitObject<SquareHitObject, SquareJudgement>
    {
        private const float enter_duration = 400;
        private const float exit_duration = 250;

        private Key[][] hitKeys = {
            new Key[] { Key.Number4, Key.Number5, Key.Number6, Key.Number7},
            new Key[] { Key.R, Key.T, Key.Y, Key.U },
            new Key[] { Key.F, Key.G, Key.H, Key.J },
            new Key[] { Key.V, Key.B, Key.N, Key.M },
        };

        private readonly Edge top, bottom;
        
        public DrawableMarker(SquareHitObject hitObject)
            : base(hitObject)
        {
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Alpha = 0f;
            Masking = true;
            CornerRadius = 5f;

            Children = new Framework.Graphics.Drawable[]
            {
                top = new Edge(),
                bottom = new Edge
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours, AudioManager audio)
        {
            Colour = colours.Blue;
            Samples.Add(audio.Sample.Get(@"Gameplay/drum-hitnormal")); //todo: Proper samples(maybe)

            top.PopIn(HitObject.StartTime, enter_duration);
            bottom.PopIn(HitObject.StartTime, enter_duration);
            Transforms.Add(new TransformAlpha { StartTime = HitObject.StartTime - enter_duration, EndTime = HitObject.StartTime - (enter_duration / 2), StartValue = Alpha, EndValue = 1 });
            Transforms.Add(new TransformAlpha { StartTime = HitObject.StartTime, EndTime = HitObject.StartTime + exit_duration, StartValue = 1f, EndValue = 0 });
            Expire(true);
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            if ((Judgement.Result == HitResult.None || !args.Repeat) && args.Key == hitKeys[HitObject.Row][HitObject.Column])
            {
                UpdateJudgement(true);
                //return true;
            }

            return false;
        }

        protected override void CheckJudgement(bool userTriggered)
        {
            if (!userTriggered)
            {
                if (Judgement.TimeOffset > HitObject.HitWindowFor(SquareHitResult.Early))
                    Judgement.Result = HitResult.Miss;
                return;
            }

            double hitOffset = Math.Abs(Judgement.TimeOffset);

            if (hitOffset < HitObject.HitWindowFor(SquareHitResult.Early))
            {
                Judgement.Result = HitResult.Hit;
                Judgement.Score = HitObject.HitResultForOffset(hitOffset);
            }
            else
                Judgement.Result = HitResult.Miss;

        }

        protected override void UpdateState(ArmedState state)
        {
            switch (state)
            {
                case ArmedState.Miss:
                    FadeColour(Color4.Red, exit_duration / 2);
                    break;
                case ArmedState.Hit:
                    FadeColour(Color4.White, exit_duration / 2);
                    break;
            }
        }

        protected override SquareJudgement CreateJudgement() => new SquareJudgement();

        private class Edge : Box
        {
            public Edge()
            {
                RelativeSizeAxes = Axes.Both;
                Height = 0.5f;
                Scale = new Vector2(1f, 0f);
            }

            public void PopIn(double startTime, double duration)
            {
                Transforms.Add(new TransformScale { StartTime = startTime - duration, EndTime = startTime, StartValue = Scale, EndValue = Vector2.One });
            }
        }
    }

    public enum SquareHitResult
    {
        [Description(@"Late")]
        Late,

        [Description(@"Early")]
        Early,

        [Description(@"Good")]
        Good,

        [Description(@"Perfect")]
        Perfect,

        [Description(@"Miss")]
        Miss
    }
}
