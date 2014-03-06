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
    public class LeftHallway : MapDataHolder
    {
        MainRoom main;
        BottomLeftCorner leftCorner;

        public LeftHallway()
            : base()
        {
        }

        public LeftHallway(Character player)
            : base (player)
        {
            backgroundDirectory = "BackgroundsAndFloors/Dungeons/FixedDungeon1/DungeonHallwayLeft";
            background = Game1.GameContent.Load<Texture2D>(backgroundDirectory);
            backgroundRec = new Rectangle(0, 0, background.Width, background.Height);

            Random rand = new Random();


            Skeleton sprite = new Skeleton(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"),
                .04f, 8, new Vector2(300, 200));
            addEnemy(sprite);
            sprite = new Skeleton(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"),
                .04f, 8, new Vector2(400, 200));
            addEnemy(sprite);
            sprite = new Skeleton(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"),
                .04f, 8, new Vector2(300, 300));
            addEnemy(sprite);
            sprite = new Skeleton(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"),
                .04f, 8, new Vector2(300, 400));
            addEnemy(sprite);
            sprite = new Skeleton(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"),
                .04f, 8, new Vector2(200, 200));
            addEnemy(sprite);
            sprite = new Skeleton(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"),
                .04f, 8, new Vector2(300, 500));
            addEnemy(sprite);

            addEnemy(new Thing(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"), .05f, new Vector2(300, 0))); 


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
            
            TripWire newTrip = new TripWire(.02f, new Rectangle(848, 339, 3, 30));
            addTripWire(newTrip);

            newTrip = new TripWire(.02f, new Rectangle(130, 339, 3, 52));
            addTripWire(newTrip);

            if (player.Direction == Orientation.LEFT)
            {
                adjustObjectsBackgroundTripWires(new Vector2(Game1.DisplayWidth - backgroundRec.Width, 0), true);
                player.Position = new Vector2(659 - player.CollisionRec.Width - 10, player.Position.Y);
            }
            else if (player.Direction == Orientation.RIGHT)
            {
                player.Position = new Vector2(130 + 10, 339);
            }

            music = Game1.GameContent.Load<Song>("Music/DungeonTheme1");
        }

        public override void Update(Map map, GameTime gameTime)
        {
            //tripwire logic???
            //MapData1 newMap = new MapData1();
            //map.changeMap(newMap);

            KeyboardState keys = Keyboard.GetState();
            bool changeMain = false;
            bool changeCorner = false;

            for (int i = 0; i < tripWires.Count; i++)
            {
                tripWires[i].Update(map.Player.CollisionRec);

                if (i == 0 && tripWires[i].IfTripped && map.Player.Direction == Orientation.RIGHT)
                {
                    main = new MainRoom(map.Player);
                    changeMain = true;
                }
                else if (i == 1 && tripWires[i].IfTripped && map.Player.Direction == Orientation.LEFT)
                {
                    leftCorner = new BottomLeftCorner(map.Player);
                    changeCorner = true;
                }
            }

            if (changeMain)
            {
                map.changeMap(main);
            }
            else if (changeCorner)
            {
                map.changeMap(leftCorner);
            }

            base.Update(map, gameTime);
        }
    }
}
