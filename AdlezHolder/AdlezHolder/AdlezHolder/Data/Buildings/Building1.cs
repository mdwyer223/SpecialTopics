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
    public class Building1 : MapDataHolder
    {
        Nwot town;
        KeyboardState keys, oldKeys;

        public Building1(Character player)
        {
            //load up textures and positions and stuff
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Buildings/HOUSE4");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);

            player.Position = new Vector2((Game1.DisplayWidth) - player.CollisionRec.Width,
                Game1.DisplayHeight / 2);

            TripWire exit = new TripWire(.02f, new Rectangle(Game1.DisplayWidth - 43, 
                Game1.DisplayHeight / 2, 10, 30));
            addTripWire(exit);

            TripWire shop = new TripWire(.02f, new Rectangle(Game1.DisplayWidth - 300, 
                Game1.DisplayHeight / 2, 20, 20));
            addTripWire(shop);
        }

        //nothing SHOULD be overrided, unless there is a specific reason for it
        //actually update:  TripWire
        public override void Update(Map map, GameTime gameTime)
        {
            bool change = false;
            keys = Keyboard.GetState();

            for (int i = 0; i < tripWires.Count; i++)
            {
                tripWires[i].Update(map.Player.CollisionRec);                

                if (tripWires[0].IfTripped && map.Player.Direction == Orientation.RIGHT)
                {
                    change = true;
                    town = new Nwot();
                }

                if (tripWires[1].IfTripped && (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space)))
                {

                    changeGameState(GameState.UPGRADESHOP);
                }
            }
            oldKeys = keys;

            if (change)
            {
                map.Player.Position = new Vector2(170, 327);
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
                spriteBatch.DrawString(Game1.GameContent.Load<SpriteFont>("SpriteFont1"), "Shop", tripWires[1].Position, Color.White);
            }
        }

        private void changeGameState(GameState newState)
        {
            Game1.MainGameState = newState;
        }
    }
}
