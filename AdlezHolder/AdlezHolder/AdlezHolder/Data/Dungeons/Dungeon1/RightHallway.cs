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
    public class RightHallway: MapDataHolder
    {
        static bool locked;
        MainRoom main;
        BottomRightCorner rightCorner;

        string lastPlace = "";
        bool changePos = false;

        public static bool Locked
        {
            get { return locked; }
            set { locked = value; }
        }

        public RightHallway()
            : base()
        {
        }

        public RightHallway(Character player)
            : base (player)
        {

            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Dungeons/FixedDungeon1/DungeonHallwayRight");
            backgroundRec = new Rectangle(0, 0, background.Width, background.Height);

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

            ArrowTrap aTrap = new ArrowTrap(.03f, new Vector2(150, 180), new Vector2(0, 3));
            addArrowTrap(aTrap);
            aTrap = new ArrowTrap(.03f, new Vector2(300, 180), new Vector2(0, 3));
            addArrowTrap(aTrap);
            aTrap = new ArrowTrap(.03f, new Vector2(450, 180), new Vector2(0, 3));
            addArrowTrap(aTrap);
            aTrap = new ArrowTrap(.03f, new Vector2(600, 180), new Vector2(0, 3));
            addArrowTrap(aTrap);
            aTrap = new ArrowTrap(.03f, new Vector2(750, 180), new Vector2(0, 3));
            addArrowTrap(aTrap);

            //algorithm for the player position: take the position they are currently at and flip them to the other side of the screen
            //for example, if he exits to the right, and enters on the left. exits top, enters bottom
            
            TripWire newTrip = new TripWire(.02f, new Rectangle(130, 339, 3, 60));
            addTripWire(newTrip);

            newTrip = new TripWire(.02f, new Rectangle(848, 339, 3, 30));
            addTripWire(newTrip);

            music = Game1.GameContent.Load<Song>("Music/DungeonTheme1");
        }

        public RightHallway(string id)
            : base()
        {

            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Dungeons/FixedDungeon1/DungeonHallwayRight");
            backgroundRec = new Rectangle(0, 0, background.Width, background.Height);

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

            ArrowTrap aTrap = new ArrowTrap(.03f, new Vector2(150, 180), new Vector2(0, 3));
            addArrowTrap(aTrap);
            aTrap = new ArrowTrap(.03f, new Vector2(300, 180), new Vector2(0, 3));
            addArrowTrap(aTrap);
            aTrap = new ArrowTrap(.03f, new Vector2(450, 180), new Vector2(0, 3));
            addArrowTrap(aTrap);
            aTrap = new ArrowTrap(.03f, new Vector2(600, 180), new Vector2(0, 3));
            addArrowTrap(aTrap);
            aTrap = new ArrowTrap(.03f, new Vector2(750, 180), new Vector2(0, 3));
            addArrowTrap(aTrap);

            //algorithm for the player position: take the position they are currently at and flip them to the other side of the screen
            //for example, if he exits to the right, and enters on the left. exits top, enters bottom

            TripWire newTrip = new TripWire(.02f, new Rectangle(130, 339, 3, 60));
            addTripWire(newTrip);

            newTrip = new TripWire(.02f, new Rectangle(848, 339, 3, 30));
            addTripWire(newTrip);

            music = Game1.GameContent.Load<Song>("Music/DungeonTheme1");

            lastPlace = id;
        }

        public override void Update(Map map, GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            bool changeMain = false;
            bool changeCorner = false;

            if (!changePos)
            {
                changePos = true;
                if (lastPlace.Equals("MainRoom"))
                {
                    map.Player.Position = new Vector2(130 + 10, 339);
                }
                else if (lastPlace.Equals("BottomRightCorner"))
                {
                    map.Player.Position = new Vector2(848 - map.Player.CollisionRec.Width - 10, map.Player.Position.Y);
                }
                lastPlace = "";
            }

            for (int i = 0; i < tripWires.Count; i++)
            {
                tripWires[i].Update(map.Player.CollisionRec);

                if (i == 0 && tripWires[i].IfTripped && map.Player.Direction == Orientation.LEFT)
                {
                    main = new MainRoom("RightHallway");
                    changeMain = true;
                }

                if(i == 1 && tripWires[i].IfTripped && map.Player.Direction == Orientation.RIGHT)
                {
                    rightCorner = new BottomRightCorner("RightHallway");
                    changeCorner = true;
                }
            }

            if (changeMain)
            {
                map.changeMap(main);
            }
            else if (changeCorner)
            {
                map.changeMap(rightCorner);
            }
            else
            {
                if (lastPlace.Equals(""))
                {
                    base.Update(map, gameTime);
                }
            }
        }
    }
}
