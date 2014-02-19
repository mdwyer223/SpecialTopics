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
    class WepStats
    {
        Rectangle menuRect, innerRect, leftRect, rightRect, upgradesRect;
        Texture2D sword, bomb, bow, locked;
        List<Texture2D> weapons;
        int iconSize, currentIndex = 0, nextIndex = 1, lastIndex = 2;
        KeyboardState keys, oldKeys;
        Boolean bowLocked = false, bombLocked = false;

        public WepStats(Game1 game, Vector2 border)
        {
            sword = game.Content.Load<Texture2D>("Weapons/sword selected");
            bomb = game.Content.Load<Texture2D>("Weapons/bomb selected");
            bow = game.Content.Load<Texture2D>("Weapons/bow selected");
            locked = game.Content.Load<Texture2D>("Weapons/lock");

            menuRect = new Rectangle((int)border.X, (int)border.Y, (int)(game.GraphicsDevice.Viewport.Width - border.X), game.GraphicsDevice.Viewport.Height);
            innerRect = new Rectangle(menuRect.X + (int)((menuRect.Width / 2) - (menuRect.Width * .25)), (int)((menuRect.Height / 3) - (menuRect.Height * .25)), (int)(menuRect.Width * .5), (int)(menuRect.Height * .5));
            leftRect = new Rectangle(innerRect.X - (int)(innerRect.Width * .25), (int)(innerRect.Y + (innerRect.Height / 2) - (innerRect.Height * .25)), (int)(innerRect.Width * .5), (int)(innerRect.Height * .5));
            rightRect = new Rectangle(innerRect.X + innerRect.Width - (int)(innerRect.Width * .25), (int)(innerRect.Y + (innerRect.Height / 2) - (innerRect.Height * .25)), (int)(innerRect.Width * .5), (int)(innerRect.Height * .5));
            
            weapons = new List<Texture2D>();

            weapons.Add(sword);
            if (bombLocked)
            {
                weapons.Add(locked);
                weapons.Add(locked);
            }
            else if (bowLocked)
            {
                weapons.Add(bomb);
                weapons.Add(locked);
            }
            else
            {
                weapons.Add(bomb);
                weapons.Add(bow);
            }

            iconSize = (int)(innerRect.Height * 1.2);

            upgradesRect = new Rectangle(leftRect.X, menuRect.Y + iconSize, (rightRect.X + rightRect.Width), 500);
        }

        public void update()
        {
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
            {
                currentIndex--;
                nextIndex--;
                lastIndex--;
                if (currentIndex < 0 && !bowLocked)
                {
                    currentIndex = 2;
                }
                else if (currentIndex < 0 && !bombLocked)
                {
                    currentIndex = 1;
                }
                else if(bowLocked && bombLocked)
                {
                    currentIndex = 0;
                }
            }

            if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
            {
                currentIndex++;
                nextIndex++;
                lastIndex++;
                if(currentIndex > 1 && bowLocked)
                {
                    currentIndex = 0;
                }
                else if(bombLocked)
                {
                    currentIndex = 0;
                }
                else if (currentIndex > 2)
                {
                    currentIndex = 0;
                }
            }

            if (currentIndex == 0)
            {
                lastIndex = 2;
                nextIndex = currentIndex + 1;
            }
            else if (currentIndex == 2)
            {
                lastIndex = currentIndex - 1;
                nextIndex = 0;
            }
            else
            {
                lastIndex = currentIndex - 1;
                nextIndex = currentIndex + 1;
            }

            oldKeys = keys;
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            spriteBatch.DrawString(spriteFont, "These are the weapons that will be released.", new Vector2(upgradesRect.X, upgradesRect.Y), Color.White);
            spriteBatch.Draw(weapons[lastIndex], leftRect, Color.White);
            spriteBatch.Draw(weapons[nextIndex], rightRect, Color.White);
            spriteBatch.Draw(weapons[currentIndex], innerRect, Color.White);
        }
    }
}
