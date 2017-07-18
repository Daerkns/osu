// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Database;
using OpenTK;
using osu.Game.Screens.Select.Details;

namespace osu.Game.Overlays.BeatmapSetInspector
{
    public class BeatmapDetails : FillFlowContainer
    {
        private readonly BasicStats basic;
        private readonly AdvancedStats advanced;
        private readonly UserRatings ratings;

        public readonly PreviewButton PreviewButton;

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
                new AsyncLoadWrapper(PreviewButton = new PreviewButton(set))
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                },
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
