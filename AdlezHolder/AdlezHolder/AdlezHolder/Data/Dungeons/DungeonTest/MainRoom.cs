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
    public class MainRoom : MapDataHolder
    {
        BossHall bHall;
        RightHallway rHall;
        LeftHallway lHall;

        public MainRoom()
            : base()
        {
            backgroundDirectory = "BackgroundsAndFloors/Dungeons/FixedDungeon1/MainRoom";
            background = Game1.GameContent.Load<Texture2D>(backgroundDirectory);
            backgroundRec = new Rectangle(0, 0, (int)(background.Width), (int)(background.Height));

            updateCorners();

            Torch torch = new Torch(.02f, new Vector2(202, 139));
            addBaseSprite(torch);
            torch = new Torch(.02f, new Vector2(268, 139));
            addBaseSprite(torch);
            torch = new Torch(.02f, new Vector2(688, 139));
            addBaseSprite(torch);
            torch = new Torch(.02f, new Vector2(754, 139));
            addBaseSprite(torch);


            Wall wall = new Wall(new Rectangle(130, 215 - backgroundRec.Height, backgroundRec.Width, backgroundRec.Height), 
                Game1.GameContent.Load<Texture2D>("Box/MetalBox"), new Vector2(130, 215 - backgroundRec.Height));
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

            ImmovableObject block = new ImmovableObject(Game1.GameContent.Load<Texture2D>("Box/MetalBox"), .05f,
                0, new Vector2(150, 250));
            //addImmovable(block);

            MovableObject mover = new MovableObject(Game1.GameContent.Load<Texture2D>("Box/MetalBox"), .05f, Game1.DisplayWidth,
                7, new Vector2(150, 350));
            addMovable(mover);
                
            Texture2D[] box = new Texture2D[5];
            box[0] = Game1.GameContent.Load<Texture2D>("Box/Box");
            box[1] = Game1.GameContent.Load<Texture2D>("Box/BoxBreaking");
            box[2] = Game1.GameContent.Load<Texture2D>("Box/BoxBreaking2");
            box[3] = Game1.GameContent.Load<Texture2D>("Box/BoxBreaking3");
            box[4] = Game1.GameContent.Load<Texture2D>("Box/BoxBreaking4");

            HittableObject hittable = new HittableObject(box, .05f,
                7, new Vector2(500, 350), 25);
            addImmovable(hittable);

            TripWire newTrip = new TripWire(.02f, new Rectangle(848, 339, 3, 30));
            addTripWire(newTrip);

            newTrip = new TripWire(.02f, new Rectangle(130, 339, 3, 52));
            addTripWire(newTrip);

            newTrip = new TripWire(.02f, new Rectangle(404, 218, 191, 3));
            addTripWire(newTrip);

            int x = (Game1.DisplayWidth - background.Width) / 2;
            int y = (Game1.DisplayHeight - background.Height);
            adjustObjectsBackgroundTripWires(new Vector2(x, y));

            music = Game1.GameContent.Load<Song>("Music/DungeonTheme1");
  
        }

        public MainRoom(Character player)
            : base(player)
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Dungeons/FixedDungeon1/MainRoom");
            backgroundRec = new Rectangle(0, 0, (int)(background.Width), (int)(background.Height));

            updateCorners();

            Torch torch = new Torch(.02f, new Vector2(100, 100));
            addBaseSprite(torch);

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

            ImmovableObject block = new ImmovableObject(Game1.GameContent.Load<Texture2D>("Box/MetalBox"), .05f,
                0, new Vector2(180, 250));
            addImmovable(block);

            MovableObject mover = new MovableObject(Game1.GameContent.Load<Texture2D>("Box/MetalBox"), .05f, Game1.DisplayWidth,
                7, new Vector2(180, 350));
            addMovable(mover);

            Texture2D[] box = new Texture2D[5];
            box[0] = Game1.GameContent.Load<Texture2D>("Box/Box");
            box[1] = Game1.GameContent.Load<Texture2D>("Box/BoxBreaking");
            box[2] = Game1.GameContent.Load<Texture2D>("Box/BoxBreaking2");
            box[3] = Game1.GameContent.Load<Texture2D>("Box/BoxBreaking3");
            box[4] = Game1.GameContent.Load<Texture2D>("Box/BoxBreaking4");

            HittableObject hittable = new HittableObject(box, .05f,
                7, new Vector2(500, 350), 25);
            addImmovable(hittable);

            TripWire newTrip = new TripWire(.02f, new Rectangle(848, 339, 3, 30));
            addTripWire(newTrip);

            newTrip = new TripWire(.02f, new Rectangle(130, 339, 3, 52));
            addTripWire(newTrip);

            newTrip = new TripWire(.02f, new Rectangle(404, 218, 191, 3));
            addTripWire(newTrip);

            if (player.Direction == Orientation.LEFT)
            {
                adjustObjectsBackgroundTripWires(new Vector2(Game1.DisplayWidth - backgroundRec.Width, 0));
                player.Position = new Vector2(659 - player.CollisionRec.Width - 10, player.Position.Y);
            }
            else if (player.Direction == Orientation.RIGHT)
            {
                player.Position = new Vector2(140, 339);
            }
            else if (player.Direction == Orientation.DOWN)
            {
                player.Position = new Vector2(407, 211);
                adjustObjectsBackgroundTripWires(new Vector2(-94, -8));
            }
            else if (player.Direction == Orientation.UP)
            {
                player.Position = new Vector2((Game1.DisplayWidth / 2) - player.CollisionRec.Width,
                    Game1.DisplayHeight - player.CollisionRec.Height - 5);
            }

            music = Game1.GameContent.Load<Song>("Music/DungeonTheme1");
        }

        public override void Update(Map map, GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();   
            bool changeRight = false;
            bool changeLeft = false;
            bool changeBoss = false;

            for (int i = 0; i < tripWires.Count; i++)
            {
                tripWires[i].Update(map.Player.CollisionRec);

                if (i == 0 && tripWires[i].IfTripped && map.Player.Direction == Orientation.RIGHT)
                {
                    if (TopLeftCorner.OpenChest || keys.IsKeyDown(Keys.LeftControl))
                    {
                        rHall = new RightHallway(map.Player);
                        changeRight = true;
                    }
                    else
                    {
                        map.Player.addMessage(new Message("It's locked",
                            new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
                    }
                }

                else if (i == 1 && tripWires[i].IfTripped && map.Player.Direction == Orientation.LEFT)
                {
                    lHall = new LeftHallway(map.Player);
                    changeLeft = true;
                }

                else if (i == 2 && tripWires[i].IfTripped && map.Player.Direction == Orientation.UP)
                {
                    if (TopRightCorner.OpenChest || keys.IsKeyDown(Keys.LeftControl))
                    {
                        bHall = new BossHall(map.Player);
                        changeBoss = true;
                    }
                    else
                    {
                        map.Player.addMessage(new Message("It's locked",
                            new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
                    }
                }

            }

            if (changeRight)
            {
                map.changeMap(rHall);
            }
            else if (changeLeft)
            {
                map.changeMap(lHall);
            }
            else if (changeBoss)
            {
                map.changeMap(bHall);
            }

            base.Update(map, gameTime);
        }
    }
}
