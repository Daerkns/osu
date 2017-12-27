// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Screens.Multi.Components;

namespace osu.Game.Tests.Visual
{
    public class TestCaseMultiplayerScreens : OsuTestCase
    {
        public TestCaseMultiplayerScreens()
        {
            // Add(new Multiplayer());

            var h = new Header();
            Add(h);

            AddStep(@"select lobby", () => h.SelectedTab.Value = MultiplayerTab.Lobby);
            AddStep(@"select room", () => h.SelectedTab.Value = MultiplayerTab.Room);
        }
    }
}
