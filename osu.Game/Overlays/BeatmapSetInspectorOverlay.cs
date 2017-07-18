// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Database;
using osu.Game.Graphics.Containers;
using osu.Game.Overlays.BeatmapSetInspector;

namespace osu.Game.Overlays
{
    public class BeatmapSetInspectorOverlay : WaveOverlayContainer
    {
        public const float WIDTH_PADDING = 30;
        public const float DETAILS_WIDTH = 275;

        private readonly BeatmapHeader header;

        public BeatmapSetInspectorOverlay(BeatmapSetInfo set)
        {
            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopCentre;
            RelativeSizeAxes = Axes.Both;
            Width = 0.8f;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Colour = Color4.Black.Opacity(0f),
                Radius = 3,
            };

            //todo: wave colours

            BeatmapInfoArea info;
            Children = new Drawable[]
            {
                new ReverseChildIDFillFlowContainer<Drawable>
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        header = new BeatmapHeader(set),
                        info = new BeatmapInfoArea(set),
                    },
                },
            };

            header.SelectedBeatmap.ValueChanged += b => info.Beatmap = b;
        }

        protected override void PopIn()
        {
            base.PopIn();

            FadeEdgeEffectTo(0.25f, APPEAR_DURATION, EasingTypes.In);
        }

        protected override void PopOut()
        {
            base.PopOut();

            header.Details.PreviewButton.Playing = false;
            FadeEdgeEffectTo(0, DISAPPEAR_DURATION, EasingTypes.Out);
        }
    }
}
