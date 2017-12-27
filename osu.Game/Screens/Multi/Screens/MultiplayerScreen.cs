// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Screens.Multi.Screens
{
    public class MultiplayerScreen : Container
    {
        protected override Container<Drawable> Content => content;
        private readonly Container content;

        public MultiplayerScreen()
        {
            RelativeSizeAxes = Axes.Both;

            InternalChild = content = new Container { RelativeSizeAxes = Axes.Both };
        }
    }
}
