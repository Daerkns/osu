// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.Backgrounds;
using osu.Game.Online.Multiplayer;
using osu.Game.Screens.Multi.Components;
using osu.Game.Screens.Multi.Screens;

namespace osu.Game.Screens.Multi
{
    public class Multiplayer : OsuScreen
    {
        private readonly Lobby lobby;

        public IEnumerable<Room> Rooms
        {
            get { return lobby.Rooms;  }
            set { lobby.Rooms = value; }
        }

        public Multiplayer()
        {
            Header header;
            Container screenContainer;
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
                header = new Header(),
                screenContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = lobby = new Lobby(),
                },
            };

            screenContainer.Padding = new MarginPadding { Top = header.DrawHeight };
            lobby.Exited += screen => Exit();
        }
    }
}
