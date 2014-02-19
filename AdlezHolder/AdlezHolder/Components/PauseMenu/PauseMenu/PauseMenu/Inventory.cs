using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PauseMenu
{
    class Inventory
    {
        public enum Items
        {
            POTIONS, POTIONM, POTIONL
        }

        List<List<Items>> items;

        public Inventory(Game1 game)
        {

        }

        public void update()
        {
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            spriteBatch.DrawString(spriteFont, "Unavailable in the alpha.", new Vector2(351, 208), Color.White);
        }
    }
}
