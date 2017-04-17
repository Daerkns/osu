// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Modes.Square.Objects;
using osu.Game.Modes.UI;
using osu.Game.Modes.Square.Judgements;
using osu.Framework.Graphics.Containers;
using OpenTK;
using System.Linq;
using osu.Game.Modes.Objects.Drawables;
using osu.Game.Modes.Square.Objects.Drawables;
using System;

namespace osu.Game.Modes.Square.UI
{
    public class SquarePlayfield : Playfield<SquareHitObject, SquareJudgement>
    {
        private const int columns = 4;
        private const int rows = 4;
        private readonly Vector2 square_spacing = new Vector2(10f);
        private readonly Vector2 judgement_offset = new Vector2(0f, -5f);

        private FillFlowContainer squares;

        public SquarePlayfield()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            Children = new Drawable[]
            {
                squares = new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Direction = FillDirection.Vertical,
                    Spacing = square_spacing,
                },
            };

            for (int y = 0; y < rows; y++)
            {
                FillFlowContainer row = new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Spacing = square_spacing,
                };

                for (int x = 0; x < columns; x++)
                {
                    row.Add(new SquareBackground());
                }

                squares.Add(row);
            }
        }

        public override void Add(DrawableHitObject<SquareHitObject, SquareJudgement> h)
        {
            backgroundAt(h.HitObject.Column, h.HitObject.Row)?.Add(h);
        }

        public override void OnJudgement(DrawableHitObject<SquareHitObject, SquareJudgement> judgedObject)
        {
            DrawableSquareJudgement explosion = new DrawableSquareJudgement(judgedObject.Judgement)
        	{
        		Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Position = judgement_offset,
        	};

            Console.WriteLine($"{judgedObject.HitObject.Column}x{judgedObject.HitObject.Row} - {judgedObject.Judgement.ResultString}");
            backgroundAt(judgedObject.HitObject.Column, judgedObject.HitObject.Row).Add(explosion);
        }

        private SquareBackground backgroundAt(int column, int row)
        {
            return (squares.Children.ElementAtOrDefault(row) as FillFlowContainer)?.Children.ElementAtOrDefault(column) as SquareBackground;
        }
    }
}