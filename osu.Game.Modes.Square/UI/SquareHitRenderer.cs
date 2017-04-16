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

namespace osu.Game.Modes.Square.UI
{
    public class SquareHitRenderer : HitRenderer<SquareHit, SquareJudgment>
    {
        public SquareHitRenderer(WorkingBeatmap beatmap)
            : base(beatmap)
        {
        }

        public override ScoreProcessor CreateScoreProcessor() => new SquareScoreProcessor(this);

        protected override IBeatmapConverter<SquareHit> CreateBeatmapConverter() => new SquareBeatmapConverter();

        protected override IBeatmapProcessor<SquareHit> CreateBeatmapProcessor() => new SquareBeatmapProcessor();

        protected override Playfield<SquareHit, SquareJudgment> CreatePlayfield() => new SquarePlayfield();

        protected override DrawableHitObject<SquareHit, SquareJudgment> GetVisualRepresentation(SquareHit h) => null;
    }
}
