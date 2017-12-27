// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.Backgrounds;

namespace osu.Game.Screens.Multi
{
    public class Multiplayer : OsuScreen
    {
        public Multiplayer()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = OsuColour.FromHex(@"3E3A44"),
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Child = new Triangles
                    {
                        RelativeSizeAxes = Axes.Both,
                        TriangleScale = 5,
                        ColourLight = OsuColour.FromHex(@"3A3740"),
                        ColourDark = OsuColour.FromHex(@"39353F"),
                    },
                },
            };
        }
    }
}
