using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace AdlezHolder
{
    public class InventoryMenu
    {
        public enum Items
        {
            POTIONS, POTIONM, POTIONL
        }

        List<Item> playerItems;

        public InventoryMenu(Game1 game)
        {

        }

        public void update()
        {
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            Vector2 vec = new Vector2(351, 208);
            //spriteBatch.DrawString(spriteFont, "Unavailable in the alpha.", new Vector2(351, 208), Color.White);
            for (int i = 0; i < playerItems.Count; i++)
            {
                if (playerItems[i] != null)
                {
                    
                    spriteBatch.DrawString(Game1.GameContent.Load<SpriteFont>("SpriteFont1"),
                        playerItems[i].ItemName, vec, Color.White);
                    vec.X += Game1.GameContent.Load<SpriteFont>("SpriteFont1").MeasureString(playerItems[i].ItemName).X;
                    spriteBatch.Draw(playerItems[i].Image, playerItems[i].DrawnRec, Color.White);
                    playerItems[i].Position = vec;
                    vec.X -= Game1.GameContent.Load<SpriteFont>("SpriteFont1").MeasureString(playerItems[i].ItemName).X;
                    vec.Y += 25;
                }
            }
        }

        public void updateInvent(List<Item> newList)
        {
            playerItems = newList;
        }
    }
}
