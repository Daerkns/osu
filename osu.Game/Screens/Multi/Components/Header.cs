// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Extensions;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics.UserInterface;
using osu.Game.Overlays.SearchableList;
using Container = osu.Framework.Graphics.Containers.Container;

namespace osu.Game.Screens.Multi.Components
{
    public class Header : Container
    {
        private const float title_size = 30;
        private const float title_spacing = 10;

        private readonly BreadcrumbControl<MultiplayerTab> breadcrumbs;

        public Bindable<MultiplayerTab> SelectedTab => breadcrumbs.Current;

        public Header()
        {
            RelativeSizeAxes = Axes.X;
            Height = 121;

            FillFlowContainer<ScreenName> nameContainer;
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = OsuColour.FromHex(@"2F2043"),
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Horizontal = SearchableListOverlay.WIDTH_PADDING },
                    Children = new Drawable[]
                    {
                        new FillFlowContainer
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.BottomLeft,
                            Position = new Vector2(-(title_size + title_spacing), 5f),
                            AutoSizeAxes = Axes.Both,
                            Direction = FillDirection.Horizontal,
                            Spacing = new Vector2(title_spacing, 0f),
                            Children = new Drawable[]
                            {
                                new SpriteIcon
                                {
                                    Size = new Vector2(title_size),
                                    Icon = FontAwesome.fa_osu_multi,
                                },
                                new FillFlowContainer
                                {
                                    AutoSizeAxes = Axes.Both,
                                    Direction = FillDirection.Horizontal,
                                    Children = new Drawable[]
                                    {
                                        new OsuSpriteText
                                        {
                                            Text = "multiplayer ",
                                            TextSize = title_size,
                                        },
                                        nameContainer = new FillFlowContainer<ScreenName>
                                        {
                                            AutoSizeAxes = Axes.Both,
                                            Direction = FillDirection.Horizontal,
                                            LayoutDuration = 500,
                                            LayoutEasing = Easing.OutQuint,
                                        },
                                    },
                                },
                            },
                        },
                        breadcrumbs = new BreadcrumbControl<MultiplayerTab>
                        {
                            Anchor = Anchor.BottomRight,
                            Origin = Anchor.BottomRight,
                            RelativeSizeAxes = Axes.X,
                            AccentColour = Color4.White,
                        },
                    },
                },
            };

            foreach (MultiplayerTab tab in Enum.GetValues(typeof(MultiplayerTab)))
            {
                nameContainer.Add(new ScreenName(tab));
            }

            breadcrumbs.Current.ValueChanged += t =>
            {
                nameContainer.Children.ForEach(c =>
                {
                    if (c.RepresentedTab == t)
                    {
                        c.FadeIn(500, Easing.OutQuint);
                    }
                    else
                    {
                        c.FadeOut(500, Easing.OutQuint);
                    }
                });
            };
        }

        private class ScreenName : OsuSpriteText
        {
            public readonly MultiplayerTab RepresentedTab;

            public ScreenName(MultiplayerTab tab)
            {
                RepresentedTab = tab;
                Text = tab.GetDescription().ToLower() + @" ";
                TextSize = title_size;
                Font = @"Exo2.0-Light";
                Alpha = 0;
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                Colour = colours.Yellow;
            }
        }
    }

    public enum MultiplayerTab
    {
        Lobby,
        Room,
    }
}
