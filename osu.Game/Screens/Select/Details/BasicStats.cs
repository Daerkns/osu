// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using OpenTK;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Game.Database;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Screens.Select.Details
{
    public class BasicStats : Container
    {
        private const float stat_count = 4;

        private readonly Statistic length, bpm, circleCount, sliderCount;

        private BeatmapInfo beatmap;
        public BeatmapInfo Beatmap
        {
            get { return beatmap; }
            set
            {
                if (value == beatmap) return;
                beatmap = value;

                length.Value = TimeSpan.FromMilliseconds(Beatmap.OnlineInfo.Length).ToString(@"m\:ss");
                bpm.Value = Beatmap.OnlineInfo.BPM.ToString(@"0.##");
                circleCount.Value = beatmap.OnlineInfo.CircleCount.ToString();
                sliderCount.Value = beatmap.OnlineInfo.SliderCount.ToString();
            }
        }

        public BasicStats()
        {
            var statWidth = 1 / stat_count;
            Child = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Horizontal,
                Children = new[]
                {
                    length = new Statistic(FontAwesome.fa_clock_o, "Length") { Width = statWidth },
                    bpm = new Statistic(FontAwesome.fa_circle, "BPM") { Width = statWidth },
                    circleCount = new Statistic(FontAwesome.fa_circle_o, "Circle Count") { Width = statWidth },
                    sliderCount = new Statistic(FontAwesome.fa_circle, "Slider Count") { Width = statWidth },
                },
            };
        }

        private class Statistic : Container, IHasTooltip
        {
            private readonly string name;
            private readonly OsuSpriteText value;

            public string TooltipText => name;
            public string Value
            {
                get { return value.Text; }
                set { this.value.Text = value; }
            }

            public Statistic(FontAwesome icon, string name)
            {
                this.name = name;
                RelativeSizeAxes = Axes.X;
                AutoSizeAxes = Axes.Y;

                Children = new Drawable[]
                {
                    new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        AutoSizeAxes = Axes.Both,
                        Children = new Drawable[]
                        {
                            new TextAwesome
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.Centre,
                                Icon = FontAwesome.fa_square,
                                TextSize = 13,
                                Rotation = 45,
                                Colour = OsuColour.FromHex(@"441288"),
                            },
                            new TextAwesome
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.Centre,
                                Icon = icon,
                                TextSize = 13,
                                Colour = OsuColour.FromHex(@"f7dd55"),
                                Scale = new Vector2(0.8f),
                            },
                            value = new OsuSpriteText
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                TextSize = 13,
                                Font = @"Exo2.0-Bold",
                                Margin = new MarginPadding { Left = 10 },
                            },
                        },
                    },
                };
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colour)
            {
                value.Colour = colour.Yellow;
            }
        }
    }
}
