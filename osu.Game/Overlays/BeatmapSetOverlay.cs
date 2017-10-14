// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input;
using osu.Framework.Threading;
using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Online.API;
using osu.Game.Online.API.Requests;
using osu.Game.Overlays.BeatmapSet;

namespace osu.Game.Overlays
{
    public class BeatmapSetOverlay : WaveOverlayContainer
    {
        public const float X_PADDING = 40;
        public const float RIGHT_WIDTH = 275;

        private readonly Header header;
        private readonly Info info;

        private APIAccess api;
        private ScheduledDelegate pendingBeatmapSwitch;

        public BeatmapSetOverlay()
        {
            FirstWaveColour = OsuColour.Gray(0.4f);
            SecondWaveColour = OsuColour.Gray(0.3f);
            ThirdWaveColour = OsuColour.Gray(0.2f);
            FourthWaveColour = OsuColour.Gray(0.1f);

            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopCentre;
            RelativeSizeAxes = Axes.Both;
            Width = 0.85f;

            Masking = true;
            EdgeEffect = new EdgeEffectParameters
            {
                Colour = Color4.Black.Opacity(0),
                Type = EdgeEffectType.Shadow,
                Radius = 3,
                Offset = new Vector2(0f, 1f),
            };

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = OsuColour.Gray(0.2f)
                },
                new ScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    ScrollbarVisible = false,
                    Child = new ReverseChildIDFillFlowContainer<Drawable>
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Direction = FillDirection.Vertical,
                        Children = new Drawable[]
                        {
                            header = new Header(),
                            info = new Info(),
                        },
                    },
                },
            };

            header.Picker.Beatmap.ValueChanged += b =>
            {
                info.Beatmap = b;

                pendingBeatmapSwitch?.Cancel();
                pendingBeatmapSwitch = Schedule(() => updateStatistics(b));
            };
        }

        [BackgroundDependencyLoader]
        private void load(APIAccess api)
        {
            this.api = api;
        }

        protected override void PopIn()
        {
            base.PopIn();
            FadeEdgeEffectTo(0.25f, APPEAR_DURATION, Easing.In);
        }

        protected override void PopOut()
        {
            base.PopOut();
            header.Details.Preview.Playing = false;
            FadeEdgeEffectTo(0, DISAPPEAR_DURATION, Easing.Out);
        }

        protected override bool OnClick(InputState state)
        {
            State = Visibility.Hidden;
            return true;
        }

        public void ShowBeatmapSet(BeatmapSetInfo set)
        {
            header.BeatmapSet = info.BeatmapSet = set;
            Show();
        }

        private void updateStatistics(BeatmapInfo beatmap)
        {
            if (beatmap == null) return;

            if (beatmap.Metrics == null)
            {
                var lookup = new GetBeatmapDetailsRequest(beatmap);
                lookup.Success += result =>
                {
                    beatmap.Metrics = result;
                    if (beatmap != header.Picker.Beatmap.Value)
                        return;

                    Schedule(header.Picker.Beatmap.TriggerChange);
                };

                api.Queue(lookup);
            }
        }
    }
}
