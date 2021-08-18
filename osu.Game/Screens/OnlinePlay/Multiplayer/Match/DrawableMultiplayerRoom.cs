// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Online.API;
using osu.Game.Online.Rooms;
using osu.Game.Screens.OnlinePlay.Lounge.Components;
using osu.Game.Screens.OnlinePlay.Match.Components;
using osu.Game.Users;
using osuTK;

namespace osu.Game.Screens.OnlinePlay.Multiplayer.Match
{
    public class DrawableMultiplayerRoom : DrawableRoom
    {
        public Action OnEdit;

        [Resolved]
        private IAPIProvider api { get; set; }

        private readonly IBindable<User> host = new Bindable<User>();

        private Drawable editButton;

        public DrawableMultiplayerRoom(Room room)
            : base(room)
        {
            host.BindTo(room.Host);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            ButtonsContainer.Add(editButton = new PurpleTriangleButton
            {
                RelativeSizeAxes = Axes.Y,
                Size = new Vector2(100, 1),
                Text = "Edit",
                Action = () => OnEdit?.Invoke()
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            host.BindValueChanged(h => editButton.Alpha = h.NewValue?.Equals(api.LocalUser.Value) == true ? 1 : 0, true);
        }

        protected override bool ShouldBeConsideredForInput(Drawable child) => true;
    }
}
