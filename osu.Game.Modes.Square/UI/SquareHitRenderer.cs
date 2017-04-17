// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;
using osu.Game.Modes.Square.Beatmaps;
using osu.Game.Modes.Square.Judgements;
using osu.Game.Modes.Square.Objects;
using osu.Game.Modes.Square.Scoring;
using osu.Game.Modes.Objects.Drawables;
using osu.Game.Modes.Scoring;
using osu.Game.Modes.UI;
using osu.Game.Modes.Square.Objects.Drawable;

namespace osu.Game.Modes.Square.UI
{
    public class SquareHitRenderer : HitRenderer<SquareHitObject, SquareJudgment>
    {
        public SquareHitRenderer(WorkingBeatmap beatmap)
            : base(beatmap)
        {
        }

        public override ScoreProcessor CreateScoreProcessor() => new SquareScoreProcessor(this);

        protected override IBeatmapConverter<SquareHitObject> CreateBeatmapConverter() => new SquareBeatmapConverter();

        protected override IBeatmapProcessor<SquareHitObject> CreateBeatmapProcessor() => new SquareBeatmapProcessor();

        protected override Playfield<SquareHitObject, SquareJudgment> CreatePlayfield() => new SquarePlayfield();

        protected override DrawableHitObject<SquareHitObject, SquareJudgment> GetVisualRepresentation(SquareHitObject h)
        {
            return new DrawableSquare(h);
        }
    }
}
