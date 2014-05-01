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
        int totalHealth, currentHealth, leftoverHealth, numSegments,prevCurrentHealth, segmentWidth;
        float healthSegments = 1;
        Rectangle screenRectangle, health, fullHealth, damagedHealth;
        Texture2D healthBar, fullHealthBar;
        int inDisplayWidth, inDisplayHeight, prevTotal, damage;
        bool dead = false, oldDead;
        List<Rectangle> segments;
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

        public HealthBar()
        {
            inDisplayWidth = Game1.DisplayWidth;
            inDisplayHeight = Game1.DisplayHeight;
            screenRectangle = new Rectangle((int)(inDisplayWidth * .1), (int)(inDisplayHeight * .1), (int)(inDisplayWidth * .97), (int)(inDisplayHeight * .97));
            healthBar = Game1.GameContent.Load<Texture2D>("Weapons/health_bar");
            fullHealthBar = Game1.GameContent.Load<Texture2D>("Weapons/health_backgroung");
            segments = new List<Rectangle>();

            fullHealth = new Rectangle(screenRectangle.X, screenRectangle.Y, fullHealthBar.Width, fullHealthBar.Height);
            health = new Rectangle(screenRectangle.X, screenRectangle.Y, (int)(healthBar.Width / healthSegments), healthBar.Height);

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

        public void update(int max, int current, Character player)
        {

            dead = current <= 0;


            if (dead)
            {
                gameOver.update(this, player);
            }

            if (dead && Game1.MainGameState == GameState.PLAYING)
            {
                Rectangle rec = segments[0];
                rec.Width = 0;
                segments[0] = rec;

                gameOver.visible = true;
                return;
            }
           
            totalHealth = max;
            currentHealth = current;

            if (prevTotal != totalHealth || dead == false && oldDead == true)
            {
                //full bar = 400;
                numSegments = totalHealth / 100;
                if (totalHealth % 100 > 0)
                {
                    numSegments++;
                }
                health.Width = segmentWidth = (int)(((fullHealth.Width - numSegments) / (totalHealth / 100.0f)) + .5f);

                segments = new List<Rectangle>();
                for (int i = 1; i <= numSegments; i++)
                {
                    segments.Add(health);

                    if (i == numSegments)
                    {// add in front 
                        leftoverHealth = totalHealth % 100;
                        Rectangle test = segments[segments.Count - 1];
                        test.Width = (int)(fullHealth.Width - segmentWidth * (numSegments - 1) - (numSegments - 1));
                        segments[segments.Count - 1] = test;
                    }
                }

                for (int i = 1; i < numSegments; i++)
                {
                    Rectangle seg = segments[i];
                    seg.X = segments[i - 1].X + segments[i - 1].Width + 1;

                    segments[i] = seg;
                }
                prevCurrentHealth = currentHealth = totalHealth;                                
            }


            if (prevCurrentHealth != currentHealth)
            {
                if (currentHealth > totalHealth)
                {
                    Rectangle test = segments[segments.Count - 1];
                    test.Width = (int)(fullHealth.Width - segmentWidth * (numSegments - 1) - (numSegments - 1));
                    segments[segments.Count - 1] = test;
                    currentHealth = totalHealth;
                    return;
                }

                damage = prevCurrentHealth - currentHealth;
                leftoverHealth = currentHealth % 100;
                if (currentHealth / 100 == totalHealth / 100)
                {                    
                    Rectangle test = segments[segments.Count - 1];
                    test.Width = (int)((fullHealth.Width - segmentWidth * (numSegments - 1) - (numSegments - 1)) * (leftoverHealth / (float)(totalHealth % 100.0f)));
                    segments[segments.Count - 1] = test;
                    if (segments[segments.Count - 1].Width == 0)
                        segments.RemoveAt(segments.Count - 1);
                }
                else
                {
                    Rectangle test = segments[segments.Count - 1];
                    test.Width = (int)(segmentWidth * (leftoverHealth / 100.0f));
                    segments[segments.Count - 1] = test;
                    if (segments[segments.Count - 1].Width == 0)
                        segments.RemoveAt(segments.Count - 1);
                }

                if (prevCurrentHealth % 100 != 0 || prevCurrentHealth == 0)
                    leftoverHealth = (prevCurrentHealth % 100) - damage;
             
                if (leftoverHealth < 0)
                {
                    int oldSeg = prevCurrentHealth / 100;
                    if (prevCurrentHealth % 100 > 0)
                        oldSeg++;

                    int currentSeg = currentHealth / 100;
                    if (currentHealth % 100 > 0)
                        currentSeg++;

                    for (int i = oldSeg; i > currentSeg; i--)
                    {
                        numSegments--;
                        segments.RemoveAt(segments.Count - 1);
                    }
                    if (leftoverHealth < 0)
                    {
                        leftoverHealth = currentHealth % 100;
                        if (segments.Count != 0)
                        {
                            Rectangle test = segments[segments.Count - 1];
                            test.Width = (int)(segmentWidth * (leftoverHealth / 100.0f));
                            segments[segments.Count - 1] = test;
                        }
                        
                    }
                }
                else if (leftoverHealth > 100)
                {
                    int oldSeg = prevCurrentHealth / 100;
                    if (prevCurrentHealth % 100 > 0)
                        oldSeg++;

                    int currentSeg = currentHealth / 100;
                    if (currentHealth % 100 > 0)
                        currentSeg++;

                    for (int i = oldSeg; i < currentSeg; i++)
                    {
                        Rectangle test = segments[i - 1];
                        test.Width = segmentWidth;
                        segments[i - 1] = test;

                        numSegments++;
                        health.X = segments[i - 1].X + segments[i - 1].Width + 1;
                        segments.Add(health);
                        health.X = fullHealth.X;
                    }
                    int leftOverHeal = currentHealth % 100;
                    if (leftOverHeal > 0)
                    {
                        Rectangle test = segments[segments.Count - 1];
                        if (currentHealth / 100 == totalHealth / 100)
                        {
                            test.Width = (int)((fullHealth.Width - segmentWidth * (numSegments - 1) - (numSegments - 1)) * (leftOverHeal / (float)(totalHealth % 100.0f)));                            
                        }
                        else
                        {                            
                            test.Width = (int)(segmentWidth * (leftOverHeal / 100.0f));                            
                        }
                        segments[segments.Count - 1] = test;
                    }
                } 
            }
            
            oldDead = dead;
            prevTotal = totalHealth;
            prevCurrentHealth = currentHealth;
        }

        public void draw(SpriteBatch spriteBatch)
        {   
            spriteBatch.Draw(fullHealthBar, fullHealth, Color.White);

            for (int i = 0; i < segments.Count; i++)
            {
                    spriteBatch.Draw(healthBar, segments[i], Color.White);           
            }
                
            if (dead)
            {
                Game1.MainGameState = GameState.GAMEOVER;
                gameOver.draw(spriteBatch, Game1.GameContent.Load<SpriteFont>("GameOverFont"), Game1.GameContent.Load<SpriteFont>("SpriteFont1"));
            }
        }
    }
}
