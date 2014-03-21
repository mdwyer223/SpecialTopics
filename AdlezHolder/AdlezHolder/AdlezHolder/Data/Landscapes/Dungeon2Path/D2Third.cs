using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class D2Third : HorizontalCrossing
    {
        public D2Third(string id)
            : base(id)
        {
            TripWire t = new TripWire(.02f, new Rectangle(0, 0, 10, backgroundRec.Height));
            addTripWire(t);

            t = new TripWire(.02f, new Rectangle(backgroundRec.Width - 10, 0, 10, backgroundRec.Height));
            addTripWire(t);
        }

        public override void Update(Map map, GameTime gameTime)
        {
            bool previous = false, next = false;

            if (!changePos)
            {
                if (lastPlace.Equals("second"))
                {
                    map.Player.Position = new Vector2(map.Player.CollisionRec.Width, (backgroundRec.Height / 2) - map.Player.CollisionRec.Height);
                }
                else if (lastPlace.Equals(""))
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

                    if (i == 0 && map.Player.Direction == Orientation.LEFT)
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
                map.changeMap(new D2Second("third"));
            }
            else if (next)
            {
                //dungeon2
            }
            else
            {
                base.Update(map, gameTime);
            }
        }
    }
}
