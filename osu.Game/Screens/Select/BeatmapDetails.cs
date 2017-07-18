// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Database;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics.UserInterface;
using System.Globalization;
using System.Linq;
using osu.Game.Online.API;
using osu.Game.Online.API.Requests;
using osu.Framework.Threading;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Extensions.Color4Extensions;
using osu.Game.Screens.Select.Details;

namespace osu.Game.Screens.Select
{
    public class BeatmapDetails : Container
    {
        private const float spacing = 10;

        private readonly FillFlowContainer top, statsFlow;
        private readonly AdvancedStats advanced;
        private readonly UserRatings ratings;
        private readonly ScrollContainer metadataScroll;
        private readonly MetadataSection description, source, tags;
        private readonly Container failRetryContainer;
        private readonly FailRetryGraph failRetryGraph;

        private BeatmapInfo beatmap;
        public BeatmapInfo Beatmap
        {
            get { return beatmap; }
            set
            {
                if (value == beatmap) return;
                beatmap = value;

                advanced.Beatmap = Beatmap;
                ratings.Metrics = Beatmap.Metrics;
                description.Text = Beatmap.Version;
                source.Text = Beatmap.Metadata.Source;
                tags.Text = Beatmap.Metadata.Tags;
                failRetryGraph.Metrics = Beatmap.Metrics;

                source.Alpha = string.IsNullOrEmpty(Beatmap.Metadata.Source) ? 0f : 1f;
            }
        }

        public BeatmapDetails()
        {
            Padding = new MarginPadding { Top = spacing };

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black.Opacity(0.5f),
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Horizontal = spacing },
                    Children = new Drawable[]
                    {
                        top = new FillFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Direction = FillDirection.Horizontal,
                            Children = new Drawable[]
                            {
                                statsFlow = new FillFlowContainer
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Width = 0.5f,
                                    Spacing = new Vector2(spacing),
                                    Padding = new MarginPadding { Right = spacing / 2 },
                                    Children = new[]
                                    {
                                        new DetailBox
                                        {
                                            Child = advanced = new AdvancedStats
                                            {
                                                RelativeSizeAxes = Axes.X,
                                                AutoSizeAxes = Axes.Y,
                                                Padding = new MarginPadding { Horizontal = spacing, Top = spacing * 2, Bottom = spacing },
                                            },
                                        },
                                        new DetailBox
                                        {
                                            Child = ratings = new UserRatings
                                            {
                                                RelativeSizeAxes = Axes.X,
                                                Height = 134,
                                                Padding = new MarginPadding { Horizontal = spacing, Top = spacing },
                                            },
                                        },
                                    },
                                },
                                metadataScroll = new ScrollContainer
                                {
                                    RelativeSizeAxes = Axes.X,
                                    Width = 0.5f,
                                    ScrollbarVisible = false,
                                    Padding = new MarginPadding { Left = spacing / 2 },
                                    Child = new FillFlowContainer
                                    {
                                        RelativeSizeAxes = Axes.X,
                                        AutoSizeAxes = Axes.Y,
                                        Spacing = new Vector2(spacing * 2),
                                        Margin = new MarginPadding { Top = spacing * 2 },
                                        Children = new[]
                                        {
                                            description = new MetadataSection("Description")
                                            {
                                                TextColour = Color4.White.Opacity(0.75f),
                                            },
                                            source = new MetadataSection("Source")
                                            {
                                                TextColour = Color4.White.Opacity(0.75f),
                                            },
                                            tags = new MetadataSection("Tags"),
                                        },
                                    },
                                },
                            },
                        },
                        failRetryContainer = new Container
                        {
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            RelativeSizeAxes = Axes.Both,
                            Children = new Drawable[]
                            {
                                new OsuSpriteText
                                {
                                    Text = "Points of Failure",
                                    Font = @"Exo2.0-Bold",
                                    TextSize = 14,
                                },
                                failRetryGraph = new FailRetryGraph
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Padding = new MarginPadding { Top = 14 + spacing / 2 },
                                },
                            },
                        },
                    },
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            tags.TextColour = colours.Yellow;
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();

            metadataScroll.Height = statsFlow.DrawHeight;
            failRetryContainer.Padding = new MarginPadding { Top = top.DrawHeight + spacing / 2 };
        }

        private class DetailBox : Container
        {
            private readonly Container content;
            protected override Container<Drawable> Content => content;

            public DetailBox()
            {
                RelativeSizeAxes = Axes.X;
                AutoSizeAxes = Axes.Y;

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

        private class MetadataSection : Container
        {
            private readonly OsuSpriteText title;
            private readonly TextFlowContainer textFlow;

            public string Text
            {
                set
                {
                    textFlow.Clear();
                    textFlow.AddText(value, s => s.TextSize = 14);
                }
            }

            public Color4 TextColour
            {
                get { return textFlow.Colour; }
                set { textFlow.Colour = value; }
            }

            public MetadataSection(string title)
            {
                RelativeSizeAxes = Axes.X;
                AutoSizeAxes = Axes.Y;

                InternalChild = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Spacing = new Vector2(spacing / 2),
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Child = this.title = new OsuSpriteText
                            {
                                Text = title,
                                Font = @"Exo2.0-Bold",
                                TextSize = 14,
                            },
                        },
                        textFlow = new TextFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                        },
                    },
                };
            }
        }
    }
}
