using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class TopRightCorner : MapDataHolder
    {
        static bool locked;
        BottomRightCorner bottomCorner;
        static bool open;
        static bool entered;

        string lastPlace = "";
        bool changePos = false;

        public static bool OpenChest
        {
            get { return open; }
            protected set { open = value; }
        }

        public static bool Locked
        {
            get { return locked; }
            set { locked = value; }
        }

        public TopRightCorner(Character player)
            : base (player)
        {
            if (!entered)
            {
                open = false;
            }
            entered = true;

            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Dungeons/FixedDungeon1/DungeonRoomTopRight");
            int x = (Game1.DisplayWidth - background.Width) / 2;
            int y = (Game1.DisplayHeight - background.Height);
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

            Texture2D metalCrate = Game1.GameContent.Load<Texture2D>("Box/MetalBox");

            ImmovableObject hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(129, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(179, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(229, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(279, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(329, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(379, 390));
            addImmovable(hittable);

            MovableObject mover = new MovableObject(metalCrate, .05f, 
                Game1.DisplayWidth, 7, new Vector2(429, 390));
            addMovable(mover);

            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(479, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(529, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(579, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(629, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(679, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(729, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(779, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(829, 390));
            addImmovable(hittable);

            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(129, 297));
            addImmovable(hittable);

           MovableObject mover2 = new MovableObject(metalCrate, .05f, Game1.DisplayWidth,
                7, new Vector2(170, 297));
            addMovable(mover2);

            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(229, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(279, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(329, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(379, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(429, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(479, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(529, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(579, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(629, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(679, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(729, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(779, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(829, 297));
            addImmovable(hittable);



            Chest chest = new Chest(.1f, new Vector2(350, 200), ChestType.DUNGEON);
            chest.Open = OpenChest;
            addChest(chest);

            TripWire newTrip = new TripWire(.02f, 
                new Rectangle((backgroundRec.Width / 2) - 30, Game1.DisplayHeight - 3, 30, 3));
            addTripWire(newTrip);

            music = Game1.GameContent.Load<Song>("Music/DungeonTheme1");
        }

        public TopRightCorner(string id)
            : base()
        {
            if (!entered)
            {
                open = false;
            }
            entered = true;

            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Dungeons/FixedDungeon1/DungeonRoomTopRight");
            int x = (Game1.DisplayWidth - background.Width) / 2;
            int y = (Game1.DisplayHeight - background.Height);
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

            Texture2D metalCrate = Game1.GameContent.Load<Texture2D>("Box/MetalBox");

            ImmovableObject hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(129, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(179, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(229, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(279, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(329, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(379, 390));
            addImmovable(hittable);

            MovableObject mover = new MovableObject(metalCrate, .05f,
                Game1.DisplayWidth, 7, new Vector2(429, 390));
            addMovable(mover);

            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(479, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(529, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(579, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(629, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(679, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(729, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(779, 390));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(829, 390));
            addImmovable(hittable);

            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(129, 297));
            addImmovable(hittable);

            MovableObject mover2 = new MovableObject(metalCrate, .05f, Game1.DisplayWidth,
                 7, new Vector2(170, 297));
            addMovable(mover2);

            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(229, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(279, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(329, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(379, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(429, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(479, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(529, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(579, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(629, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(679, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(729, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(779, 297));
            addImmovable(hittable);
            hittable = new ImmovableObject(metalCrate, .05f,
                7, new Vector2(829, 297));
            addImmovable(hittable);



            Chest chest = new Chest(.1f, new Vector2(350, 200), ChestType.DUNGEON);
            chest.Open = OpenChest;
            addChest(chest);

            TripWire newTrip = new TripWire(.02f,
                new Rectangle((backgroundRec.Width / 2) - 30, Game1.DisplayHeight - 3, 30, 3));
            addTripWire(newTrip);

            music = Game1.GameContent.Load<Song>("Music/DungeonTheme1");

            lastPlace = id;
        }

        public override void Update(Map map, GameTime gameTime)
        {
            bool changeMain = false;
            //bool changeCorner = false;

            if (!changePos)
            {
                changePos = true;
                if (lastPlace.Equals("BottomRightCorner"))
                {
                    map.Player.Position = new Vector2((backgroundRec.Width / 2) - map.Player.CollisionRec.Width,
                        Game1.DisplayHeight - map.Player.CollisionRec.Height - 5);
                }
                lastPlace = "";
            }

            for (int i = 0; i < tripWires.Count; i++)
            {
                tripWires[i].Update(map.Player.CollisionRec);

                if (i == 0 && tripWires[i].IfTripped && map.Player.Direction == Orientation.DOWN)
                {
                    bottomCorner = new BottomRightCorner("TopRightCorner");
                    changeMain = true;
                    OpenChest = chests[0].Open;
                }
            }

            if (changeMain)
            {
                map.changeMap(bottomCorner);
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
