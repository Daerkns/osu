// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Input;
using osu.Game.Graphics.UserInterface;

namespace osu.Game.Overlays.Notifications
{
    public class ResponseNotification : SimpleNotification
    {
        public Func<string, bool> OnRespond;

        public ResponseNotification(string placeholder = @"")
        {
            NotificationWrapper.Height = 75;
            NotificationCloseButton.Margin = new MarginPadding { Bottom = 25f, Right = 5f };

            NotificationWrapper.Add(new ResponseTextBox
            {
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.BottomLeft,
                RelativeSizeAxes = Axes.X,
                PlaceholderText = placeholder,
                OnCommit = (sender, newText) =>
                {
                    if (OnRespond?.Invoke(sender.Text) ?? true)
                        Close();
                }    
            });
        }

        private class ResponseTextBox : OsuTextBox
        {
            protected override float LeftRightPadding => 5f;

            public ResponseTextBox()
            {
                Height = 20f;
                TextContainer.Height = 0.8f;
                CornerRadius = 5f;
            }

            protected override bool OnFocus(InputState state)
            {
                var s = base.OnFocus(state);
                BorderThickness = 0;
                return s;
            }
        }
    }
}
