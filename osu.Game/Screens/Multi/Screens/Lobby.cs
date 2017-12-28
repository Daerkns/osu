// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Game.Graphics;
using osu.Game.Online.Multiplayer;
using osu.Game.Overlays.SearchableList;
using osu.Game.Screens.Multi.Components;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Screens.Multi.Screens
{
    public class Lobby : MultiplayerScreen
    {
        private readonly FilterControl filter;
        private readonly Container content;
        private readonly FillFlowContainer<DrawableRoom> roomFlow;
        private readonly RoomInspector inspector;

        private IEnumerable<Room> rooms;
        public IEnumerable<Room> Rooms
        {
            get { return rooms; }
            set
            {
                if (value == rooms) return;
                rooms = value;

                roomFlow.ChildrenEnumerable = Rooms.Select(r => new DrawableRoom(r)
                {
                    State = SelectionState.NotSelected,
                    Action = room => SelectedRoom = room,
                });
            }
        }

        private Room selectedRoom;
        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                if (value == selectedRoom) return;
                selectedRoom = value;

                inspector.Room = value;
                roomFlow.Children.ForEach(c =>
                {
                    if (c.Room == SelectedRoom)
                    {
                        c.State = SelectionState.Selected;
                    }
                    else
                    {
                        c.State = SelectionState.NotSelected;
                    }
                });
            }
        }

        public Lobby()
        {
            Children = new Drawable[]
            {
                filter = new FilterControl(),
                content = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new ScrollContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            Width = 0.5f,
                            Child = roomFlow = new FillFlowContainer<DrawableRoom>
                            {
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Spacing = new Vector2(1),
                                Padding = new MarginPadding { Top = 30 },
                            },
                        },
                        new Container
                        {
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            RelativeSizeAxes = Axes.Both,
                            Width = 0.5f,
                            Padding = new MarginPadding { Left = 20 - DrawableRoom.SELECTION_OUTLINE_WIDTH },
                            Child = inspector = new RoomInspector
                            {
                                Room = new Room
                                {
                                    Status = { Value = new RoomStatusOpen() },
                                },
                            },
                        },
                    },
                },
            };

            filter.Search.Exit = Exit;
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();

            content.Padding = new MarginPadding
            {
                Top = filter.DrawHeight,
                Left = SearchableListOverlay.WIDTH_PADDING - DrawableRoom.SELECTION_OUTLINE_WIDTH,
                Right = SearchableListOverlay.WIDTH_PADDING
            };
        }

        protected override void OnFocus(InputState state)
        {
            GetContainingInputManager().ChangeFocus(filter.Search);
        }

        private class FilterControl : SearchableListFilterControl<RoomAvailability, RoomAvailability>
        {
            protected override Color4 BackgroundColour => OsuColour.FromHex(@"362E42");
            protected override RoomAvailability DefaultTab => RoomAvailability.Public;

            public FilterControl()
            {
                DisplayStyleControl.Hide();
            }
        }
    }
}
