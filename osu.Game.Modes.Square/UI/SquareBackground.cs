// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osu.Game.Modes.Square.Objects.Drawable;
using osu.Game.Modes.Square.Objects;

namespace osu.Game.Modes.Square.UI
{
    public class SquareBackground : Container
    {
        private const float size = 150f;

        public SquareBackground()
        {
            Size = new Vector2(size);
            Masking = true;
            CornerRadius = 5f;
            EdgeEffect = new EdgeEffect
            {
                Type = EdgeEffectType.Shadow,
                Colour = Color4.Black.Opacity(0.1f),
                Radius = 5,
            };

            Children = new Drawable[]
            {
                new Box
                {
                    Size = new Vector2(size),
                    Colour = Color4.Black.Opacity(0.6f),
                },
            };
        }
    }
}
