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
    public class BottomLeftCorner : MapDataHolder
    {
        LeftHallway leftHall;
        TopLeftCorner topLeft;

        public BottomLeftCorner(Character player)
            : base (player)
        {

            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Dungeons/FixedDungeon1/DungeonRoomBottomLeft");
            backgroundRec = new Rectangle(0, 0, background.Width, background.Height);

            Skeleton sprite = new Skeleton(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"),
                .04f, 8, new Vector2(300, 200));
            addEnemy(sprite);
            sprite = new Skeleton(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"),
                .04f, 8, new Vector2(400, 200));
            addEnemy(sprite);
            sprite = new Skeleton(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"),
                .04f, 8, new Vector2(300, 300));
            addEnemy(sprite);

            Wall wall = new Wall(new Rectangle(130, 215 - backgroundRec.Height, backgroundRec.Width, backgroundRec.Height),
                Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), new Vector2(130, 215 - backgroundRec.Height));
            addImmovable(wall);

            wall = new Wall(new Rectangle(129 - backgroundRec.Width, 215, backgroundRec.Width, backgroundRec.Width),
                Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), new Vector2(129 - backgroundRec.Width, 215));
            addImmovable(wall);

            wall = new Wall(new Rectangle(852, 216, backgroundRec.Width, backgroundRec.Height),
                Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), new Vector2(852, 216));
            addImmovable(wall);

            wall = new Wall(new Rectangle(130, backgroundRec.Height, backgroundRec.Width, backgroundRec.Height),
                Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), new Vector2(130, backgroundRec.Height));
            addImmovable(wall);

            //algorithm for the player position: take the position they are currently at and flip them to the other side of the screen
            //for example, if he exits to the right, and enters on the left. exits top, enters bottom
            

            TripWire newTrip = new TripWire(.02f, new Rectangle(848, 339, 3, 30));
            addTripWire(newTrip);
            
            newTrip = new TripWire(.02f ,new Rectangle(472, 218, 60, 3));
            addTripWire(newTrip);
            
            if (player.Direction == Orientation.DOWN)
            {
                player.Position = new Vector2(407, 211);
                adjustObjectsBackgroundTripWires(new Vector2(-94, -8));  
            }
            else if (player.Direction == Orientation.LEFT)
            {
                adjustObjectsBackgroundTripWires(new Vector2(Game1.DisplayWidth - backgroundRec.Width, 0));
                player.Position = new Vector2(659 - player.CollisionRec.Width, 339);
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

                if (i == 0 && tripWires[i].IfTripped && map.Player.Direction == Orientation.RIGHT)
                {
                    leftHall = new LeftHallway(map.Player);
                    changeHall = true;
                }

                if(i == 1 && tripWires[i].IfTripped && map.Player.Direction == Orientation.UP)
                {
                    topLeft = new TopLeftCorner(map.Player);
                    changeCorner = true;
                }
            }

            if (changeHall)
            {
                map.changeMap(leftHall);
            }
            else if (changeCorner)
            {
                map.changeMap(topLeft);
            }

            base.Update(map, gameTime);
        }
    }
}
