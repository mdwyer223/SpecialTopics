using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class MainRoom2 : MapDataHolder
    {
        TopLeft tL;
        TopRight tR;

        LeftTop lT;
        LeftBot lB;

        RightTop rT;
        RightBot rB;

        public MainRoom2()
            : base()
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Dungeons/Dungeon2/MainRoom");
            backgroundRec = new Rectangle(0, 0, background.Width, background.Height);

            updateCorners();

            //Top
            Wall wall = new Wall(new Rectangle(148, 215 - backgroundRec.Height, backgroundRec.Width, backgroundRec.Height),
                Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), new Vector2(130, 215 - backgroundRec.Height));
            addImmovable(wall);

            //Bottom
            wall = new Wall(new Rectangle(148, backgroundRec.Height, backgroundRec.Width, backgroundRec.Width),
                Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), new Vector2(129 - backgroundRec.Width, 215));
            addImmovable(wall);

            //Right
            wall = new Wall(new Rectangle(backgroundRec.Width - 148, 215, backgroundRec.Width, backgroundRec.Height),
                Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), new Vector2(852, 216));
            addImmovable(wall);

            //Left
            wall = new Wall(new Rectangle(148 - backgroundRec.Width, 215, backgroundRec.Width, backgroundRec.Height),
                Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), new Vector2(130, backgroundRec.Height));
            addImmovable(wall);

            //Topleft
            TripWire trip = new TripWire(.02f, new Rectangle(412, 231, 88, 3));
            addTripWire(trip);

            //Topright
            trip = new TripWire(.02f, new Rectangle(background.Width - 627, 231, 88, 3));
            addTripWire(trip);

            //Lefttop
            trip = new TripWire(.02f, new Rectangle(155, 350, 3, 50));
            addTripWire(trip);

            //Leftbot
            trip = new TripWire(.02f, new Rectangle(156, background.Height - 200, 3, 50));
            addTripWire(trip);

            //Righttop
            trip = new TripWire(.02f, new Rectangle(background.Width - 156, 349, 3, 50));
            addTripWire(trip);

            //Rightbot
            trip = new TripWire(.02f, new Rectangle(background.Width - 156, background.Height - 200, 3, 50));
            addTripWire(trip);
        
          }

        public MainRoom2(Character player)
            : base(player)
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Dungeons/Dungeon2/MainRoom");
            backgroundRec = new Rectangle(0, 0, background.Width, background.Height);

            /*
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
             */
        }

        public override void Update(Map map, GameTime gameTime)
        {
            bool changeLeftT = false, changeLeftB = false;
            bool changeRightT = false, changeRightB = false;
            bool changeTopL = false, changeTopR = false;

            for (int i = 0; i < tripWires.Count; i++)
            {
                tripWires[i].Update(map.Player.CollisionRec);

                if (map.Player.Direction == Orientation.UP)
                {
                    if (i == 0 && tripWires[i].IfTripped)
                    {
                        changeTopL = true;
                        tL = new TopLeft(map.Player);
                    }

                    if (i == 1 && tripWires[i].IfTripped)
                    {
                        changeTopR = true;
                        tR = new TopRight(map.Player);
                    }
                }
                else if (map.Player.Direction == Orientation.LEFT)
                {
                    if (i == 2 && tripWires[i].IfTripped)
                    {
                        changeLeftT = true;
                        lT = new LeftTop(map.Player);
                    }

                    if (i == 3 && tripWires[i].IfTripped)
                    {
                        changeLeftB = true;
                        lB = new LeftBot(map.Player);
                    }
                }
                else if (map.Player.Direction == Orientation.RIGHT)
                {
                    if (i == 4 && tripWires[i].IfTripped)
                    {
                        changeRightT = true;
                        rT = new RightTop(map.Player);
                    }

                    if (i == 5 && tripWires[i].IfTripped)
                    {
                        changeRightB = true;
                        rB = new RightBot(map.Player);
                    }
                }
                else if (map.Player.Direction == Orientation.DOWN)
                {

                }

                if (changeLeftT)
                {
                    map.changeMap(lT);
                }
            }

            base.Update(map, gameTime);
        }
    }
}
