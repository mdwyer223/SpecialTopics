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
    public class LeftTop : MapDataHolder
    {
        bool changePos;
        public LeftTop(Character player)
        {
            backgroundDirectory = "BackgroundsAndFloors/Dungeons/Dungeon2/Left";
            background = Game1.GameContent.Load<Texture2D>(backgroundDirectory);
            backgroundRec = new Rectangle(0, 0, background.Width, background.Height);
            changePos = false;
            
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
             

            TripWire trip = new TripWire(.02f, new Rectangle(backgroundRec.Width - 156, 315, 3, 50));
            addTripWire(trip);
            
            adjustObjectsBackgroundTripWires(new Vector2(Game1.DisplayWidth - background.Width,  Game1.DisplayHeight - backgroundRec.Height), true);
        }

        public override void Update(Map map, GameTime gameTime)
        {
            if (!changePos)
            {
                map.Player.Position = new Vector2(659 - map.Player.CollisionRec.Width - 10, 315);
                changePos = true;
            }

            bool change = false;

            for (int i = 0; i < tripWires.Count; i++)
            {
                tripWires[i].Update(map.Player.CollisionRec);

                if (map.Player.Direction == Orientation.RIGHT)
                {
                    if (i == 0 && tripWires[i].IfTripped)
                    {
                        change = true;
                    }
                }
            }

            if (change)
            {
                map.changeMap(new MainRoom2("LeftTop"));
            }
            else
            {
                base.Update(map, gameTime);
            }
        }
    }
}
