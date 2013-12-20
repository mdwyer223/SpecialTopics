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
    public class Building3 : MapDataHolder
    {
        Nwot town;

        public Building3(Character player)
            : base(player)
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Buildings/HOUSE4");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);

            player.Position = new Vector2((Game1.DisplayWidth) - player.CollisionRec.Width,
                Game1.DisplayHeight / 2);

            TripWire exit = new TripWire(.02f, new Rectangle(Game1.DisplayWidth - 15,
                Game1.DisplayHeight / 2, 10, 30));
            addTripWire(exit);
        }

        public override void Update(Map map, GameTime gameTime)
        {
            bool change = false;

            for (int i = 0; i < tripWires.Count; i++)
            {
                tripWires[i].Update(map.Player.CollisionRec);

                if (tripWires[i].IfTripped && map.Player.Direction == Orientation.RIGHT)
                {
                    change = true;
                    town = new Nwot();
                }
            }

            if (change)
            {
                map.Player.Position = new Vector2(165, 137
                    );
                map.Player.Direction = Orientation.RIGHT;
                map.changeMap(town);
            }

            base.Update(map, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (tripWires.Count > 0 && tripWires[0] != null)
            {
                spriteBatch.DrawString(Game1.GameContent.Load<SpriteFont>("SpriteFont1"), "Exit", tripWires[0].Position, Color.White);
            }
        }
    }
}
