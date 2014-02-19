using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class InformationScreen
    {
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.GameContent.Load<SpriteFont>("SpriteFont1"), "The game is WASD key movement, even to navigate through the menus\nEscape is to get to the pause menu in game, which has a quit button\nThe menus use enter to check for entry, but in game everything is done \nwith either space or WASD\nTo pull or push movable objects, hold Left Shift\n\nENJOY THE ALPHA!\nPress space to move on...",
                new Vector2(10, 20), Color.Blue);
        }
    }
}
