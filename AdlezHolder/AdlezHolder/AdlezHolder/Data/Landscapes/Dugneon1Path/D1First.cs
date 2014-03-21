using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class D1First : VerticalCrossing
    {
        public D1First()
            : base()
        {
            TripWire t = new TripWire(.02f, new Rectangle(0, 0, backgroundRec.Width, 10));
            addTripWire(t);

            t = new TripWire(.02f, new Rectangle(0, backgroundRec.Height - 10, backgroundRec.Width, 10));
            addTripWire(t);
        }

        public D1First(string id)
            : base(id)
        {
            TripWire t = new TripWire(.02f, new Rectangle(0, 0, backgroundRec.Width, 10));
            addTripWire(t);

            t = new TripWire(.02f, new Rectangle(0, backgroundRec.Height - 10, backgroundRec.Width, 10));
            addTripWire(t);
        }

        public override void Update(Map map, GameTime gameTime)
        {
            bool town = false, next = false;

            if (!changePos)
            {
                if(lastPlace.Equals("nwot"))
                {
                    map.Player.Position = new Vector2((backgroundRec.Width / 2) - map.Player.CollisionRec.Width, map.Player.DrawnRec.Height + 10);
                }
                else if (lastPlace.Equals("second"))
                {
                    map.Player.Position = new Vector2((backgroundRec.Width / 2) - map.Player.CollisionRec.Width, backgroundRec.Height - map.Player.DrawnRec.Height);
                }
                lastPlace = "";
                changePos = true;
            }

            for (int i = 0; i < tripWires.Count; i++)
            {
                if (tripWires[i] != null)
                {
                    tripWires[i].Update(map.Player.CollisionRec);

                    if (i == 0 && map.Player.Direction == Orientation.UP)
                    {
                        if (tripWires[i].IfTripped)
                        {
                            town = true;
                        }
                    }
                    else if (i == 1 && map.Player.Direction == Orientation.DOWN)
                    {
                        if (tripWires[i].IfTripped)
                        {
                            next = true;
                        }
                    }
                }
            }
            
            if(town)
            {
                map.changeMap(new Nwot("d1first"));
            }
            else if (next)
            {
                map.changeMap(new D1Second("first"));
            }
            else
            {
                base.Update(map, gameTime);
            }
        }
    }
}
