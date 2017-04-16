// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK.Input;
using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Modes.Square.Mods;
using osu.Game.Modes.Square.UI;
using osu.Game.Modes.Mods;
using osu.Game.Modes.UI;
using osu.Game.Screens.Play;
using System.Collections.Generic;
using osu.Game.Modes.Square.Scoring;
using osu.Game.Modes.Scoring;

namespace osu.Game.Modes.Square
{
    public class SquareRuleset : Ruleset
    {
        public override HitRenderer CreateHitRendererWith(WorkingBeatmap beatmap) => new SquareHitRenderer(beatmap);

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.DifficultyReduction:
                    return new Mod[]
                    {
                        new SquareModEasy(),
                        new SquareModNoFail(),
                        new SquareModHalfTime(),
                    };

                case ModType.DifficultyIncrease:
                    return new Mod[]
                    {
                        new SquareModHardRock(),
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new SquareModSuddenDeath(),
                                new SquareModPerfect(),
                            },
                        },
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new SquareModDoubleTime(),
                                new SquareModNightcore(),
                            },
                        },
                    };

                case ModType.Special:
                    return new Mod[]
                    {
                        null,
                        null,
                        null,
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new ModAutoplay(),
                                new ModCinema(),
                            },
                        },
                    };

                default:
                    return new Mod[] { };
            }
        }

        protected override PlayMode PlayMode => PlayMode.Square;

        public override string Description => "osu!square";

        public override FontAwesome Icon => FontAwesome.fa_square;

        public override IEnumerable<KeyCounter> CreateGameplayKeys() => new KeyCounter[]
        {
            new KeyCounterMouse(MouseButton.Left),
            new KeyCounterMouse(MouseButton.Right)
        };

        public override DifficultyCalculator CreateDifficultyCalculator(Beatmap beatmap) => new SquareDifficultyCalculator(beatmap);

        public override ScoreProcessor CreateScoreProcessor() => new SquareScoreProcessor();
    }
}
