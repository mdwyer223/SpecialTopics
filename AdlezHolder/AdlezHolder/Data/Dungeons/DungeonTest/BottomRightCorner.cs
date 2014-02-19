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
    public class BottomRightCorner : MapDataHolder
    {
        RightHallway rightHall;
        TopRightCorner topRight;

        public BottomRightCorner(Character player)
            : base (player)
        {

            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Dungeons/FixedDungeon1/DungeonRoomBottomRight");
            backgroundRec = new Rectangle(0, 0, background.Width, background.Height);

            Wall wall = new Wall(new Rectangle(130, 215 - backgroundRec.Height, backgroundRec.Width, backgroundRec.Height),
                Game1.GameContent.Load<Texture2D>("The best thing ever"), new Vector2(130, 215 - backgroundRec.Height));
            addImmovable(wall);

            wall = new Wall(new Rectangle(129 - backgroundRec.Width, 215, backgroundRec.Width, backgroundRec.Width),
                Game1.GameContent.Load<Texture2D>("The best thing ever"), new Vector2(129 - backgroundRec.Width, 215));
            addImmovable(wall);

            wall = new Wall(new Rectangle(852, 216, backgroundRec.Width, backgroundRec.Height),
                Game1.GameContent.Load<Texture2D>("The best thing ever"), new Vector2(852, 216));
            addImmovable(wall);

            wall = new Wall(new Rectangle(130, backgroundRec.Height, backgroundRec.Width, backgroundRec.Height),
                Game1.GameContent.Load<Texture2D>("The best thing ever"), new Vector2(130, backgroundRec.Height));
            addImmovable(wall);

            //algorithm for the player position: take the position they are currently at and flip them to the other side of the screen
            //for example, if he exits to the right, and enters on the left. exits top, enters bottom

            TripWire newTrip = new TripWire(.02f, new Rectangle(130, 339, 3, 52));
            addTripWire(newTrip);

            newTrip = new TripWire(.02f, new Rectangle(472, 218, 60, 3));
            addTripWire(newTrip);

            if (player.Direction == Orientation.DOWN)
            {
                player.Position = new Vector2(407, 211);
                adjustObjectsBackgroundTripWires(new Vector2(-94, -8));  
            }
            else if (player.Direction == Orientation.RIGHT)
            {
                player.Position = new Vector2(140, 339);
            }

            music = Game1.GameContent.Load<Song>("Music/DungeonTheme1");
        }

        public override void Update(Map map, GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            bool changeHall = false;
            bool changeCorner = false;

            for (int i = 0; i < tripWires.Count; i++)
            {
                tripWires[i].Update(map.Player.CollisionRec);

                if (i == 0 && tripWires[i].IfTripped && map.Player.Direction == Orientation.LEFT)
                {
                    rightHall = new RightHallway(map.Player);
                    changeHall = true;
                }

                if(i == 1 && tripWires[i].IfTripped && map.Player.Direction == Orientation.UP)
                {
                    topRight = new TopRightCorner(map.Player);
                    changeCorner = true;
                }
            }

            if (changeHall)
            {
                map.changeMap(rightHall);
            }
            else if (changeCorner)
            {
                map.changeMap(topRight);
            }

            base.Update(map, gameTime);
        }
    }
}
