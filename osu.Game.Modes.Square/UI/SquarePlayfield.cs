// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Modes.Square.Objects;
using osu.Game.Modes.UI;
using osu.Game.Modes.Square.Judgements;
using osu.Framework.Graphics.Containers;
using OpenTK;

namespace osu.Game.Modes.Square.UI
{
    public class SquarePlayfield : Playfield<SquareHit, SquareJudgment>
    {
        private const int columns = 4;
        private const int rows = 4;
        private readonly Vector2 square_spacing = new Vector2(10f);

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
    }
}