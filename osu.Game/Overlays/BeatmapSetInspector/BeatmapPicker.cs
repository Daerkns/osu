// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Database;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using OpenTK;
using osu.Game.Graphics.Sprites;
using osu.Framework.Input;
using System.Linq;
using osu.Game.Beatmaps.Drawables;
using osu.Framework.Configuration;
using System;

namespace osu.Game.Overlays.BeatmapSetInspector
{
    public class BeatmapPicker : FillFlowContainer
    {
        public const float TILE_ICON_PADDING = 7;
        private const float difficulty_height = 54;

        private readonly OsuSpriteText difficultyName, starDifficulty;

        public BeatmapPicker(BeatmapSetInfo set, Bindable<BeatmapInfo> bindable)
        {
            RelativeSizeAxes = Axes.X;
            Height = 120;

            FillFlowContainer tiles;
            Children = new Drawable[]
            {
                tiles = new TilesFillFlowContainer
                {
                    AutoSizeAxes = Axes.X,
                    Height = difficulty_height,
                    Spacing = new Vector2(2f),
                    OnLostHover = () =>
                    {
                        showDifficulty(bindable.Value);
                        starDifficulty.FadeOut(100);
                    },
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(5f),
                    Margin = new MarginPadding { Top = 10, Left = TILE_ICON_PADDING },
                    Children = new[]
                    {
                        difficultyName = new OsuSpriteText
                        {
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            TextSize = 17,
                            Font = @"Exo2.0-Bold",
                        },
                        starDifficulty = new OsuSpriteText
                        {
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            TextSize = 13,
                            Font = @"Exo2.0-Bold",
                        },
                    },
                },
                new Container
                {
                    RelativeSizeAxes = Axes.X,
                    Height = 20,
                    Margin = new MarginPadding { Left = TILE_ICON_PADDING },
                    Child = new FillFlowContainer
                    {
                        Anchor = Anchor.BottomLeft,
                        Origin = Anchor.BottomLeft,
                        AutoSizeAxes = Axes.Both,
                        Direction = FillDirection.Horizontal,
                        Spacing = new Vector2(10f),
                        Children = new[]
                        {
                            new Statistic(FontAwesome.fa_play_circle, set.OnlineInfo.PlayCount),
                            new Statistic(FontAwesome.fa_heart, set.OnlineInfo.FavouriteCount),
                        },
                    },
                },
            };

            bindable.ValueChanged += showDifficulty;
            tiles.ChildrenEnumerable = set.Beatmaps.Select(b => new DifficultyTile(bindable, b)
            {
                OnHovered = beatmap =>
                {
                    showDifficulty(beatmap);
                    starDifficulty.Text = string.Format("Star Difficulty {0:N2}", beatmap.StarDifficulty);
                    starDifficulty.FadeIn(100);
                },
            });
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            starDifficulty.Colour = colours.Yellow;
        }

        private void showDifficulty(BeatmapInfo beatmap) => difficultyName.Text = beatmap.Version;

        private class TilesFillFlowContainer : FillFlowContainer
        {
            public Action OnLostHover; //todo: naming

            protected override void OnHoverLost(InputState state)
            {
                OnLostHover?.Invoke();
            }
        }

        private class DifficultyTile : OsuClickableContainer
        {
            private const float transition_duration = 100;
            private const float icon_fade = 0.7f;

            private readonly Bindable<BeatmapInfo> bindable;
            private readonly BeatmapInfo beatmap;
            private readonly Container bg;
            private readonly DifficultyIcon icon;

            public Action<BeatmapInfo> OnHovered; //todo: naming

            public DifficultyTile(Bindable<BeatmapInfo> bindable, BeatmapInfo beatmap)
            {
                this.bindable = bindable;
                this.beatmap = beatmap;
                Size = new Vector2(difficulty_height);

                Children = new Drawable[]
                {
                    bg = new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        CornerRadius = 3,
                        Alpha = 0f,
                        Masking = true,
                        Child = new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Black.Opacity(0.5f),
                        },
                    },
                    icon = new DifficultyIcon(beatmap)
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Position = new Vector2(0f, -1f),
                        Size = new Vector2(difficulty_height - TILE_ICON_PADDING * 2),
                        Alpha = icon_fade,
                    },
                };

                bindable.ValueChanged += bindable_ValueChanged;
                Action = () => bindable.Value = beatmap;
            }

            protected override bool OnHover(InputState state)
            {
                fadeIn();
                OnHovered?.Invoke(beatmap);
                return base.OnHover(state);
            }

            protected override void OnHoverLost(InputState state)
            {
                if (bindable.Value != beatmap)
                    fadeOut();
            }

            protected override void Dispose(bool isDisposing)
            {
                if (bindable != null)
                    bindable.ValueChanged -= bindable_ValueChanged;
                base.Dispose(isDisposing);
            }

            private void bindable_ValueChanged(BeatmapInfo value)
            {
                if (value == beatmap)
                    fadeIn();
                else
                    fadeOut();
            }

            private void fadeIn()
            {
                bg.FadeIn(transition_duration);
                icon.FadeIn(transition_duration);
            }

            private void fadeOut()
            {
                bg.FadeOut(transition_duration);
                icon.FadeTo(icon_fade, transition_duration);
            }
        }

        private class Statistic : FillFlowContainer
        {
            private readonly SpriteText text;

            private int value;
            public int Value
            {
                get { return value; }
                set
                {
                    this.value = value;
                    text.Text = Value.ToString(@"N0");
                }
            }

            public Statistic(FontAwesome icon, int value = 0)
            {
                AutoSizeAxes = Axes.Both;
                Direction = FillDirection.Horizontal;
                Spacing = new Vector2(2f);

                Children = new Drawable[]
                {
                    new TextAwesome
                    {
                        Icon = icon,
                        Shadow = true,
                        TextSize = 11,
                        Margin = new MarginPadding { Top = 1 },
                    },
                    text = new OsuSpriteText
                    {
                        Font = @"Exo2.0-SemiBoldItalic",
                        TextSize = 13,
                    },
                };

                Value = value;
            }
        }
    }
}
