// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Database;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using OpenTK;
using osu.Framework.Input;
using osu.Game.Screens.Select.Details;

namespace osu.Game.Overlays.BeatmapSetInspector
{
    public class BeatmapDetails : FillFlowContainer
    {
        private readonly BasicStats basic;
        private readonly AdvancedStats advanced;
        private readonly UserRatings ratings;

        private BeatmapInfo beatmap;
        public BeatmapInfo Beatmap
        {
            get { return beatmap; }
            set
            {
                if (value == beatmap) return;
                beatmap = value;

                advanced.Beatmap = basic.Beatmap = Beatmap;
                ratings.Metrics = Beatmap.Metrics;
            }
        }

        public BeatmapDetails(BeatmapSetInfo set)
        {
            Anchor = Anchor.BottomRight;
            Origin = Anchor.BottomRight;
            Width = BeatmapSetInspectorOverlay.DETAILS_WIDTH;
            AutoSizeAxes = Axes.Y;
            Spacing = new Vector2(1f);

            Children = new Drawable[]
            {
                new PlayButton(),
                new DetailSection
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Child = basic = new BasicStats
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Padding = new MarginPadding { Horizontal = 15, Vertical = 10 },
                    },
                },
                new DetailSection
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Child = advanced = new AdvancedStats
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Padding = new MarginPadding { Horizontal = 15, Vertical = 9 },
                    },
                },
                new DetailSection
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Child = ratings = new UserRatings
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 105,
                        Padding = new MarginPadding { Top = 10, Horizontal = 15 },
                    },
                },
            };
        }

        private class PlayButton : OsuClickableContainer
        {
            private readonly Box bg;

            public PlayButton()
            {
                RelativeSizeAxes = Axes.X;
                Height = 42;

                Children = new Drawable[]
                {
                    bg = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.Black.Opacity(0.25f),
                    },
                    new TextAwesome
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Icon = FontAwesome.fa_play,
                        TextSize = 18,
                        Shadow = false,
                        UseFullGlyphHeight = false,
                    },
                };
            }

            protected override bool OnHover(InputState state)
            {
                bg.FadeColour(Color4.Black.Opacity(0.5f), 100);
                return base.OnHover(state);
            }

            protected override void OnHoverLost(InputState state)
            {
                bg.FadeColour(Color4.Black.Opacity(0.25f), 100);
            }
        }

        private class DetailSection : Container
        {
            private readonly Container content;

            protected override Container<Drawable> Content => content;

            public DetailSection()
            {
                InternalChildren = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.Black.Opacity(0.5f),
                    },
                    content = new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                    },
                };
            }
        }
    }
}
