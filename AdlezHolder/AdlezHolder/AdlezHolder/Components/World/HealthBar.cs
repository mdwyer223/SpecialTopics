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

namespace AdlezHolder
{
    public class HealthBar
    {
        int totalHealth, currentHealth;
        Game1 game1;
        Rectangle screenRectangle, health, fullHealth, damagedHealth;
        Texture2D healthBar, fullHealthBar;
        int inDisplayWidth, inDisplayHeight;
        Boolean dead = false;
        Gameover gameOver;

        public int MaxHealth
        {
            get { return totalHealth; }
            private set { totalHealth = value; }
        }

        public int CurrentHealth
        {
            get { return currentHealth; }
        }

        public Boolean IsDead
        {
            get { return dead; }
            set { dead = value; }
        }

        public HealthBar(Game1 game)
        {
            game1 = game;

            inDisplayWidth = game.GraphicsDevice.Viewport.Width;
            inDisplayHeight = game.GraphicsDevice.Viewport.Height;
            screenRectangle = new Rectangle((int)(inDisplayWidth * .1), (int)(inDisplayHeight * .1), (int)(inDisplayWidth * .97), (int)(inDisplayHeight * .97));
            healthBar = game.Content.Load<Texture2D>("Weapons/health_bar");
            fullHealthBar = game.Content.Load<Texture2D>("Weapons/health_backgroung");

            fullHealth = new Rectangle(screenRectangle.X, screenRectangle.Y, fullHealthBar.Width, fullHealthBar.Height);
            health = new Rectangle(screenRectangle.X, screenRectangle.Y, healthBar.Width, healthBar.Height);

            fullHealth = scale(.5f, fullHealth);
            health = scale(.5f, health);
            damagedHealth = health;

            gameOver = new Gameover();
        }

        public Rectangle scale(float scaleFactor, Rectangle rectangle)
        {
            rectangle.Width = (int)((inDisplayWidth * scaleFactor) + 0.5f);
            float aspectRatio = (float)fullHealthBar.Width / fullHealthBar.Height;
            rectangle.Height = (int)((rectangle.Width / aspectRatio) + 0.5f);

            return rectangle;
        }

        public Rectangle healthScale(float scaleFactor, Rectangle rectangle)
        {
            rectangle.Width = (int)((rectangle.Width * scaleFactor) + 0.5f);

            return rectangle;
        }

        public void update(int max, int current, Character player)
        {
            currentHealth = current;
            totalHealth = max;
            
            damagedHealth = health;
            damagedHealth = healthScale(currentHealth/(float)totalHealth, damagedHealth);         

            if (currentHealth == 0)
            {
                dead = true;
                currentHealth = max;
                gameOver.visible = true;
            }
                            
            gameOver.update(this, player);
        }

        public void draw(SpriteBatch spriteBatch)
        {   
            spriteBatch.Draw(fullHealthBar, fullHealth, Color.White);
            spriteBatch.Draw(healthBar, damagedHealth, Color.White);
            if (dead)
            {
                Game1.MainGameState = GameState.GAMEOVER;
                gameOver.draw(spriteBatch, Game1.GameContent.Load<SpriteFont>("GameOverFont"), Game1.GameContent.Load<SpriteFont>("SpriteFont1"));
            }
        }
    }
}
