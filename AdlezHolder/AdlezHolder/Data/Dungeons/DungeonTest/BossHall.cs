using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class BossHall : MapDataHolder
    {
        MainRoom mainRoom;
        BossRoom bossRoom;

        public BossHall(Character player)
            : base(player)
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Dungeons/FixedDungeon1/BossHallway");
            backgroundRec = new Rectangle((Game1.DisplayWidth/2) - (background.Width / 2), -(background.Height) + Game1.DisplayHeight, 
                background.Width, background.Height);

            Wall wall = new Wall(new Rectangle(((Game1.DisplayWidth/2) - (background.Width /2) + 67) - background.Width, (-background.Height + Game1.DisplayHeight + 225), 
                backgroundRec.Width, backgroundRec.Height), null, new Vector2(67, 225));
            addImmovable(wall);
            wall = new Wall(new Rectangle((Game1.DisplayWidth / 2) - (background.Width / 2) + 430, (-background.Height) + Game1.DisplayHeight + 224, background.Width, background.Height),
                null, new Vector2(0, 0));
            addImmovable(wall);
            wall = new Wall(new Rectangle(((Game1.DisplayWidth / 2) - (background.Width / 2)) + 67, (-background.Height) + Game1.DisplayHeight - background.Height + 224,
                background.Width, background.Height), null, Vector2.Zero);
            addImmovable(wall);
            wall = new Wall(new Rectangle(((Game1.DisplayWidth / 2) - (background.Width / 2)) + 67, Game1.DisplayHeight, background.Width, 
                background.Height), null, Vector2.Zero);
            addImmovable(wall);

            player.Position = new Vector2(Game1.DisplayWidth / 2, Game1.DisplayHeight - player.DrawnRec.Height - 10);

            TripWire newTrip = new TripWire(.02f, new Rectangle((Game1.DisplayWidth / 2) - 50, 
                Game1.DisplayHeight - 3, 100, 3));
            addTripWire(newTrip);

            newTrip = new TripWire(.02f, new Rectangle(147, 220, 300, 3));
            addTripWire(newTrip);

            music = Game1.GameContent.Load<Song>("Music/DungeonTheme1");
        }

        public override void Update(Map map, GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            bool changeMain = false;
            bool changeBoss = false;

            for (int i = 0; i < tripWires.Count; i++)
            {
                tripWires[i].Update(map.Player.CollisionRec);
                //adjust(map.Player, keys, tripWires[i]);

                if (i == 0 && tripWires[i].IfTripped && map.Player.Direction == Orientation.DOWN)
                {
                    mainRoom = new MainRoom(map.Player);
                    changeMain = true;
                }

                if (i == 1 && tripWires[i].IfTripped && map.Player.Direction == Orientation.UP)
                {
                    bossRoom = new BossRoom(map.Player);
                    changeBoss = true;
                }
            }

            if (changeMain)
            {
                map.changeMap(mainRoom);
            }
            else if (changeBoss)
            {
                map.changeMap(bossRoom);
            }

            base.Update(map, gameTime);
        }
    }
}
