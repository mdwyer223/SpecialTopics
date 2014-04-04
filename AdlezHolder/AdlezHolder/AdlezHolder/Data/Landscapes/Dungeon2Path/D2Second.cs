using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class D2Second : VTurnEast
    {
        public D2Second(string id)
            : base(id)
        {
            backgroundDirectory = "BackgroundsAndFloors/Landscapes/Grass";
            TripWire t = new TripWire(.02f, new Rectangle(0, backgroundRec.Height - 10, backgroundRec.Width, 10));
            addTripWire(t);

            t = new TripWire(.02f, new Rectangle(backgroundRec.Width -10, 0, 10, backgroundRec.Height));
            addTripWire(t);

            //301,126
            Random rand = new Random();
            Vector2 pos = new Vector2(301, 126);
            for (int i = 0; i < 5; i++)
            {
                ImmovableObject io;
                if (rand.Next(1, 3) == 1)
                {
                    io = new ImmovableObject(Game1.GameContent.Load<Texture2D>("NatureObjects/RockLeft"), .04f, 0, pos);
                    addImmovable(io);
                }
                else
                {
                    io = new ImmovableObject(Game1.GameContent.Load<Texture2D>("NatureObjects/RockRight"), .04f, 0, pos);
                    addImmovable(io);
                }

                pos.X += io.CollisionRec.Width;
            }
        }

        public override void Update(Map map, GameTime gameTime)
        {
            bool previous = false, next = false;

            if (!changePos)
            {
                if (lastPlace.Equals("first"))
                {
                    map.Player.Position = new Vector2((backgroundRec.Width / 2) - map.Player.CollisionRec.Width, backgroundRec.Height - map.Player.DrawnRec.Height);
                }
                else if (lastPlace.Equals("third"))
                {
                    map.Player.Position = new Vector2(backgroundRec.Width - map.Player.CollisionRec.Width, (backgroundRec.Height / 2) - map.Player.CollisionRec.Height);
                }
                lastPlace = "";
                changePos = true;
            }

            for (int i = 0; i < tripWires.Count; i++)
            {
                if (tripWires[i] != null)
                {
                    tripWires[i].Update(map.Player.CollisionRec);

                    if (i == 0 && map.Player.Direction == Orientation.DOWN)
                    {
                        if (tripWires[i].IfTripped)
                        {
                            previous = true;
                        }
                    }
                    else if (i == 1 && map.Player.Direction == Orientation.RIGHT)
                    {
                        if (tripWires[i].IfTripped)
                        {
                            next = true;
                        }
                    }
                }
            }

            if (previous)
            {
                map.changeMap(new D2First("second"));
            }
            else if (next)
            {
                map.changeMap(new D2Third("second"));
            }
            else
            {
                base.Update(map, gameTime);
            }
        }
    }
}
