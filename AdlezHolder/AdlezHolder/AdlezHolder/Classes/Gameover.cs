using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class Gameover
    {
        Rectangle backRec;
        int fadeCount, blinkCount;
        Texture2D texture;
        Vector2 gameOverSize, continueSize;
        KeyboardState keys, oldKeys;
        Boolean fading = false;
        public Boolean visible = false;

        public Gameover()
        {
            backRec = new Rectangle(Game1.DisplayWidth - (int)(Game1.DisplayWidth * 1.05), Game1.DisplayHeight - (int)(Game1.DisplayHeight * 1.05),
                (int)(Game1.DisplayWidth * 1.1), (int)(Game1.DisplayHeight * 1.1));

            texture = Game1.GameContent.Load<Texture2D>("Random/The best thing ever");
        }

        public void update(HealthBar healthBar, Character player)
        {
            if(visible)
            {
                keys = Keyboard.GetState();

                if (fadeCount < 255)
                {
                    fadeCount += 5;
                }

                if (fadeCount == 255)
                {
                    if (blinkCount < 255 && !fading)
                    {
                        blinkCount += 5;
                        if (blinkCount == 255)
                        {
                            fading = true;
                        }
                    }
                    else if (blinkCount > 0 && fading)
                    {
                        blinkCount -= 5;
                        if (blinkCount == 0)
                        {
                            fading = false;
                        }
                    }
                    if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
                    {
                        Game1.MainGameState = GameState.MAINMENU;
                        healthBar.IsDead = false;
                        MediaPlayer.Stop();
                        player.HitPoints = player.MaxHitPoints;
                        visible = false;
                        fadeCount = 0;
                    }

                    oldKeys = keys;
                }
            }
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont gameOverFont, SpriteFont continueFont)
        {
                spriteBatch.Draw(texture, backRec, new Color(0, 0, 0, fadeCount));

                if (fadeCount == 255)
                {
                    gameOverSize = gameOverFont.MeasureString("Game Over");
                    spriteBatch.DrawString(gameOverFont, "Game Over", new Vector2(backRec.X + (backRec.Width / 2) - (gameOverSize.X / 2), backRec.Y + (backRec.Height / 3)), Color.White);
                    continueSize = continueFont.MeasureString("Press Enter to continue");
                    spriteBatch.DrawString(continueFont, "Press Enter to continue", new Vector2(Game1.DisplayWidth - (int)(continueSize.X * 1.1), Game1.DisplayHeight - (int)(continueSize.Y * 1.25)), new Color(blinkCount, blinkCount, blinkCount));
                }
        }
    }
}
