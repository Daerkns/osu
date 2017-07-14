﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
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

namespace osu.Game.Screens.Select.Details
{
    public class SuccessRate : Container
    {
        private readonly FillFlowContainer header;
        private readonly OsuSpriteText successRateLabel, successPercent, graphLabel;
        private readonly Bar successRate;
        private readonly Container percentContainer, graphContainer;
        private readonly BarGraph retryGraph, failGraph;

        private BeatmapInfo beatmap;
        public BeatmapInfo Beatmap
        {
            get { return beatmap; }
            set
            {
                if (value == beatmap) return;
                beatmap = value;

                successPercent.Text = $"{beatmap.OnlineInfo.SuccessRate}%";
                successRate.Length = beatmap.OnlineInfo.SuccessRate / 100;

                var retries = Beatmap.Metrics.Retries;
                var fails = Beatmap.Metrics.Fails;

                float maxValue = fails.Zip(retries, (fail, retry) => fail + retry).Max();
                failGraph.MaxValue = maxValue;
                retryGraph.MaxValue = maxValue;

                failGraph.Values = fails.Select(f => (float)f);
                retryGraph.Values = retries.Zip(fails, (retry, fail) => retry + MathHelper.Clamp(fail, 0, maxValue));
            }
        }

        public SuccessRate()
        {
            Children = new Drawable[]
            {
                header = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        successRateLabel = new OsuSpriteText
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Text = "Success Rate",
                            TextSize = 13,
                        },
                        successRate = new Bar
                        {
                            RelativeSizeAxes = Axes.X,
                            Height = 5,
                            Margin = new MarginPadding { Top = 5 },
                        },
                        percentContainer = new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Child = successPercent = new OsuSpriteText
                            {
                                Anchor = Anchor.TopRight,
                                Origin = Anchor.TopCentre,
                                TextSize = 13,
                            },
                        },
                        graphLabel = new OsuSpriteText
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Text = "Points of Failure",
                            TextSize = 13,
                            Margin = new MarginPadding { Vertical = 20 },
                        },
                    },
                },
                graphContainer = new Container
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    RelativeSizeAxes = Axes.Both,
                    Children = new[]
                    {
                        retryGraph = new BarGraph
                        {
                            RelativeSizeAxes = Axes.Both,
                        },
                        failGraph = new BarGraph
                        {
                            RelativeSizeAxes = Axes.Both,
                        },
                    },
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            successRateLabel.Colour = successPercent.Colour = graphLabel.Colour = colours.Gray5;
            successRate.AccentColour = colours.Green;
            successRate.BackgroundColour = colours.GrayD;
            retryGraph.Colour = colours.Yellow;
            failGraph.Colour = colours.YellowDarker;
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();

            graphContainer.Padding = new MarginPadding { Top = header.DrawHeight };
        }

        protected override void Update()
        {
            base.Update();

            percentContainer.Width = successRate.Length;
        }
    }
}
