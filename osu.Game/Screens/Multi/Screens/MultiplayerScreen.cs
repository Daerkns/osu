// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Screens;

namespace osu.Game.Screens.Multi.Screens
{
    public class MultiplayerScreen : OsuScreen
    {
        public override bool RequestsFocus => IsCurrentScreen;

        public override bool AcceptsFocus => true;

        protected override void OnEntering(Screen last)
        {
            base.OnEntering(last);

            Schedule(() => GetContainingInputManager().TriggerFocusContention(this));
        }

        protected override bool OnExiting(Screen next)
        {
            return base.OnExiting(next);

            if (HasFocus)
                GetContainingInputManager().ChangeFocus(null);
        }

        protected override void OnResuming(Screen last)
        {
            base.OnResuming(last);

            Schedule(() => GetContainingInputManager().TriggerFocusContention(this));
        }

        protected override void OnSuspending(Screen next)
        {
            base.OnSuspending(next);

            if (HasFocus)
                GetContainingInputManager().ChangeFocus(null);
        }
    }
}
