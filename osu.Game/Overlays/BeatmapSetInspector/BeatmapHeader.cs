// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Shapes;
using osu.Game.Database;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using OpenTK;
using osu.Game.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Game.Users;
using osu.Game.Graphics.Backgrounds;
using osu.Framework.Configuration;
using System.Linq;

namespace osu.Game.Overlays.BeatmapSetInspector
{
    public class BeatmapHeader : FillFlowContainer
    {
        private const float header_spacing = 10;

        private readonly Box modeBackground;
        private readonly BeatmapPicker picker;
        private readonly FavouriteButton favourite;

        public readonly Bindable<BeatmapInfo> SelectedBeatmap;
        public readonly BeatmapDetails Details;

        public BeatmapHeader(BeatmapSetInfo set)
        {
            SelectedBeatmap = new Bindable<BeatmapInfo>() { Value = set.Beatmaps.FirstOrDefault() };
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Masking = true;
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Colour = Color4.Black.Opacity(0.25f),
                Radius = 3,
            };

            FillFlowContainer buttons;
            Children = new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.X,
                    Height = 50,
                    Child = modeBackground = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                    },
                },
                new Container
                {
                    RelativeSizeAxes = Axes.X,
                    Height = 350,
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Children = new Drawable[]
                            {
                                new AsyncLoadWrapper(new BeatmapSetCover(set)
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    FillMode = FillMode.Fill,
                                    OnLoadComplete = d => d.FadeInFromZero(400),
                                })
                                {
                                    RelativeSizeAxes = Axes.Both,
                                },
                                new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    ColourInfo = ColourInfo.GradientVertical(Color4.Black.Opacity(0.3f), Color4.Black.Opacity(0.8f)),
                                },
                            },
                        },
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Padding = new MarginPadding { Horizontal = BeatmapSetInspectorOverlay.WIDTH_PADDING },
                            Children = new Drawable[]
                            {
                                new Container
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Padding = new MarginPadding { Top = 20, Bottom = 30, Left = 10 - BeatmapPicker.TILE_ICON_PADDING, Right = BeatmapSetInspectorOverlay.DETAILS_WIDTH + 20 },
                                    Children = new Drawable[]
                                    {
                                        new FillFlowContainer
                                        {
                                            Anchor = Anchor.BottomLeft,
                                            Origin = Anchor.BottomLeft,
                                            RelativeSizeAxes = Axes.X,
                                            AutoSizeAxes = Axes.Y,
                                            Direction = FillDirection.Vertical,
                                            Children = new Drawable[]
                                            {
                                                buttons = new FillFlowContainer
                                                {
                                                    Anchor = Anchor.BottomLeft,
                                                    Origin = Anchor.BottomLeft,
                                                    RelativeSizeAxes = Axes.X,
                                                    Height = 45,
                                                    Spacing = new Vector2(5f),
                                                    Margin = new MarginPadding { Left = BeatmapPicker.TILE_ICON_PADDING },
                                                    Children = new Button[]
                                                    {
                                                        favourite = new FavouriteButton(),
                                                    },
                                                },
                                                new AuthorInfo(set)
                                                {
                                                    Anchor = Anchor.BottomLeft,
                                                    Origin = Anchor.BottomLeft,
                                                    RelativeSizeAxes = Axes.X,
                                                    Margin = new MarginPadding { Bottom = header_spacing, Left = BeatmapPicker.TILE_ICON_PADDING },
                                                },
                                                new OsuSpriteText
                                                {
                                                    Anchor = Anchor.BottomLeft,
                                                    Origin = Anchor.BottomLeft,
                                                    Text = set.Metadata.Artist,
                                                    TextSize = 20,
                                                    Font = @"Exo2.0-BoldItalic",
                                                    Margin = new MarginPadding { Bottom = header_spacing * 2 + 5, Left = BeatmapPicker.TILE_ICON_PADDING },
                                                },
                                                new OsuSpriteText
                                                {
                                                    Anchor = Anchor.BottomLeft,
                                                    Origin = Anchor.BottomLeft,
                                                    Text = set.Metadata.Title,
                                                    TextSize = 30,
                                                    Font = @"Exo2.0-BoldItalic",
                                                    Margin = new MarginPadding { Left = BeatmapPicker.TILE_ICON_PADDING },
                                                },
                                                picker = new BeatmapPicker(set, SelectedBeatmap)
                                                {
                                                    Anchor = Anchor.BottomLeft,
                                                    Origin = Anchor.BottomLeft,
                                                },
                                            },
                                        },
                                    },
                                },
                                Details = new BeatmapDetails(set),
                            },
                        },
                    },
                },
            };

            favourite.Favourited.Value = false; //todo: use proper initial value
            SelectedBeatmap.ValueChanged += b => Details.Beatmap = b;

            if (set.OnlineInfo.HasVideo)
            {
                buttons.AddRange(new[]
                {
                    new DownloadButton("Download", "with Video"),
                    new DownloadButton("Download", "without Video"),
                });
            }
            else
            {
                buttons.Add(new DownloadButton("Download", ""));
            }
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            modeBackground.Colour = colours.Gray3;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            //trigger an initial display since there's always a beatmap selected
            SelectedBeatmap.TriggerChange();
        }

        //todo: remove me
        private class BeatmapSetCover : Sprite
        {
            private readonly BeatmapSetInfo set;
            public BeatmapSetCover(BeatmapSetInfo set)
            {
                this.set = set;
            }

            [BackgroundDependencyLoader]
            private void load(TextureStore textures)
            {
                string resource = set.OnlineInfo.Covers.Cover;

                if (resource != null)
                    Texture = textures.Get(resource);
            }
        }

        private class AuthorInfo : Container
        {
            private const float height = 50;

            public AuthorInfo(BeatmapSetInfo set)
            {
                Height = height;

                Child = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(5f),
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Size = new Vector2(height),
                            CornerRadius = 3,
                            Masking = true,
                            EdgeEffect = new EdgeEffectParameters
                            {
                                Type = EdgeEffectType.Shadow,
                                Colour = Color4.Black.Opacity(0.25f),
                                Radius = 3,
                            },
                            Child = new UpdateableAvatar
                            {
                                RelativeSizeAxes = Axes.Both,
                                User = set.OnlineInfo.Author,
                            }
                        },
                        new FillFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Direction = FillDirection.Vertical,
                            Children = new[]
                            {
                                new Field("made by", set.OnlineInfo.Author.Username, @"Exo2.0-RegularItalic"),
                                new Field("submitted on", set.OnlineInfo.Submitted.ToString(@"MMM d, yyyy"), @"Exo2.0-Bold")
                                {
                                    Margin = new MarginPadding { Top = 5 },
                                },
                                new Field("ranked on", set.OnlineInfo.Ranked.ToString(@"MMM d, yyyy"), @"Exo2.0-Bold"),
                            },
                        },
                    },
                };
            }

            private class Field : FillFlowContainer
            {
                public Field(string first, string second, string secondFont)
                {
                    AutoSizeAxes = Axes.Both;
                    Direction = FillDirection.Horizontal;

                    Children = new[]
                    {
                        new OsuSpriteText
                        {
                            Text = $"{first} ",
                            TextSize = 12,
                        },
                        new OsuSpriteText
                        {
                            Text = second,
                            TextSize = 12,
                            Font = secondFont,
                        },
                    };
                }
            }
        }

        private class Button : OsuClickableContainer
        {
            private readonly Container content;

            protected override Container<Drawable> Content => content;

            public Button()
            {
                CornerRadius = 3;
                Masking = true;

                InternalChildren = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = OsuColour.FromHex(@"094c5f"),
                    },
                    new Triangles
                    {
                        RelativeSizeAxes = Axes.Both,
                        ColourLight = OsuColour.FromHex(@"0f7c9b"),
                        ColourDark = OsuColour.FromHex(@"094c5f"),
                        TriangleScale = 1.5f,
                    },
                    content = new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                    },
                };
            }
        }

        private class DownloadButton : Button
        {
            public DownloadButton(string title, string subtitle)
            {
                Width = 120;
                RelativeSizeAxes = Axes.Y;

                Child = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Horizontal = 10 },
                    Children = new Drawable[]
                    {
                        new FillFlowContainer
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            AutoSizeAxes = Axes.Both,
                            Direction = FillDirection.Vertical,
                            Children = new[]
                            {
                                new OsuSpriteText
                                {
                                    Text = title,
                                    TextSize = 13,
                                    Font = @"Exo2.0-Bold",
                                },
                                new OsuSpriteText
                                {
                                    Text = subtitle,
                                    TextSize = 11,
                                    Font = @"Exo2.0-Bold",
                                },
                            },
                        },
                        new TextAwesome
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreRight,
                            Icon = FontAwesome.fa_download,
                            UseFullGlyphHeight = false,
                            TextSize = 16,
                            Margin = new MarginPadding { Right = 5 },
                        },
                    },
                };
            }
        }

        private class FavouriteButton : Button
        {
            public readonly Bindable<bool> Favourited = new Bindable<bool>();

            public FavouriteButton()
            {
                RelativeSizeAxes = Axes.Y;

                Container pink;
                TextAwesome icon;
                Children = new Drawable[]
                {
                    pink = new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Alpha = 0f,
                        Children = new Drawable[]
                        {
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = OsuColour.FromHex(@"9f015f"),
                            },
                            new Triangles
                            {
                                RelativeSizeAxes = Axes.Both,
                                ColourLight = OsuColour.FromHex(@"cb2187"),
                                ColourDark = OsuColour.FromHex(@"9f015f"),
                                TriangleScale = 1.5f,
                            },
                        },
                    },
                    icon = new TextAwesome
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Icon = FontAwesome.fa_heart_o,
                        TextSize = 18,
                        UseFullGlyphHeight = false,
                        Shadow = false,
                    },
                };

                Favourited.ValueChanged += value =>
                {
                    pink.FadeTo(value ? 1 : 0, 200);
                    icon.Icon = value ? FontAwesome.fa_heart : FontAwesome.fa_heart_o;
                };
                Action = () => Favourited.Value = !Favourited.Value;
            }

            protected override void UpdateAfterChildren()
            {
                base.UpdateAfterChildren();

                Width = DrawHeight;
            }
        }
    }
}
