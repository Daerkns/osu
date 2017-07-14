// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Database;
using osu.Game.Graphics;
using OpenTK;
using osu.Game.Graphics.Sprites;
using System.Linq;

namespace osu.Game.Overlays.BeatmapSetInspector
{
    public class BeatmapInfoArea : Container
    {
        private const float tags_width = 175;
        private const float spacing = 10;

        private readonly Section description;
        private readonly Box successRateBg;
        private readonly TextFlowContainer tags;

        public BeatmapInfoArea(BeatmapSetInfo set)
        {
            RelativeSizeAxes = Axes.X;
            Height = 220;

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.White,
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Horizontal = BeatmapSetInspectorOverlay.WIDTH_PADDING, Top = 15 },
                    Spacing = new Vector2(spacing),
                    Children = new Drawable[]
                    {
                        description = new Section("Description")
                        {
                            RelativeSizeAxes = Axes.Y,
                        },
                        new Section("Tags")
                        {
                            Width = tags_width,
                            RelativeSizeAxes = Axes.Y,
                            Child = tags = new TextFlowContainer
                            {
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                            },
                        },
                        new Container
                        {
                            Width = BeatmapSetInspectorOverlay.DETAILS_WIDTH,
                            RelativeSizeAxes = Axes.Y,
                            Children = new Drawable[]
                            {
                                successRateBg = new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                },
                            },
                        },
                    },
                },
            };

            tags.AddText(set.Beatmaps.FirstOrDefault()?.Metadata.Tags ?? string.Empty, s => s.TextSize = 13); //todo: change tags for selected difficulty
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            successRateBg.Colour = colours.GrayE;
            tags.Colour = colours.BlueDark;
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();

            description.Width = DrawWidth - (BeatmapSetInspectorOverlay.WIDTH_PADDING * 2 + BeatmapSetInspectorOverlay.DETAILS_WIDTH + spacing * 2 + tags_width);
        }

        private class Section : Container
        {
            private readonly Container content;
            private readonly OsuSpriteText title;

            protected override Container<Drawable> Content => content;

            public Section(string title)
            {
                AddInternal(new ScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    ScrollbarVisible = false,
                    Children = new[]
                    {
                        new FillFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Spacing = new Vector2(5f),
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
                                content = new Container
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                },
                            },
                        },
                    },
                });
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                title.Colour = colours.Gray5;
            }
        }
    }
}
