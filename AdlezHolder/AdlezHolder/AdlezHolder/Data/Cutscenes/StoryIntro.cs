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
    public class StoryIntro
    {
        Random rand;

        List<Particle> ambient;
        int ambienceTimer = 0, spawnTime;
        bool burst = false;

        const int textTime = 7;
        int textTimer = 0, textIndex = 0;
        List<string> messages;
        string currentMessage;
        bool over = false;

        Color particleColor;
        float textBrightness;
        bool brightenText = true, darkenText;

        KeyboardState keys, oldKeys;

        public bool Over
        {
            get { return over; }
        }

        public StoryIntro()
        {
            rand = new Random();
            messages = new List<string>();
            keys = oldKeys = Keyboard.GetState();

            //load messages
            messages.Add("The land, Adlez, lived in harmony for centuries.");
            messages.Add("There were little skirmishes between nations,\n but had little interest in starting a war.");
            messages.Add("However, there were mages who had a lust for power, the four of them\n were to create a device that would be able to shape the world as\n they see fit.");
            messages.Add("The nations did not agree that one person, let alone one nation, be in\n control of this power. A crown was created made up of four\n separate parts:  Power, Wisdom, Courage, and Virtue");
            messages.Add("Each nation held a piece of the crown to not have an imbalance in power.\n As time wore on the nations, they became greedy...");
            messages.Add("There was a large war that erupted where each nation tried to gain\n all 4 pieces to the crown.");
            messages.Add("Eventually, after decades of fighting, supplies and men were low.");
            messages.Add("New leaders gathered at a summit to seal away each piece of the crown\n into sacred places to never be disturbed again.");
            messages.Add("The war took a mighty toll on the people of Adlez, the poor became\n very poor, while the rich used them to their advantage.");
            messages.Add("The poor were sick and tired... and the rich did not seem to care.");
            messages.Add("In a rebellion against the rich, the poor took down the walls\n and barriers of money and made everyone an eqaul citizen, which\n is where Adlez has been for many years.");
            messages.Add("Only time will tell if this will last...");

            currentMessage = messages[0];

            ambient = new List<Particle>();
        }

        public void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
            {
                nextText();
                textBrightness = 0f;
                if (textIndex >= messages.Count)
                {
                    textIndex = messages.Count - 1;
                }
            }

            if (textTimer <= textTime * 1000 && !brightenText)
            {
                textTimer += gameTime.ElapsedGameTime.Milliseconds;
            }
            else if (textTimer >= textTime * 1000)
            {
                textTimer = textTime * 1000;
                darkenText = true;
            }

            if (brightenText)
            {
                textBrightness += 2f;
                if (textBrightness >= 255f)
                {
                    textBrightness = 255f;
                    brightenText = false;
                }
            }
            else if (darkenText)
            {
                textBrightness -= 2f;
                if (textBrightness <= 0)
                {
                    textBrightness = 0f;
                    darkenText = false;
                    nextText();
                }
            }

            if (textIndex == 5 && !burst)
            {
                burst = true;
                for (int j = 0; j < 60; j++)
                {
                    spawnParticle(Color.Red);
                }
            }
            else if (textIndex == 10 && !burst)
            {
                burst = true;
                for (int k = 0; k < 100; k++)
                {
                    spawnParticle(Color.Yellow);
                }
            }
            else
            {
                if (ambienceTimer <= spawnTime)
                {
                    ambienceTimer++;
                }
                else
                {
                    ambienceTimer = 0;
                    spawnParticle(Color.White);
                }
            }

            for (int i = 0; i < ambient.Count; i++)
            {
                if (ambient[i] != null)
                {
                    ambient[i].Update(gameTime);
                    ambient[i].adjustVelo(new Vector2((float)(rand.Next(-1, 2) * rand.NextDouble()), 
                        (float)(rand.Next(-1, 1) * rand.NextDouble() * .1f)));
                    if (ambient[i].OffScreen)
                    {
                        ambient.RemoveAt(i);
                        if (i >= ambient.Count)
                        {
                            i--;
                            if (i < 0)
                                i = 0;
                        }
                    }
                }
            }
                oldKeys = keys;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle p in ambient)
            {
                if (p != null)
                    p.Draw(spriteBatch);
            }

            Color c = Color.White;
            spriteBatch.DrawString(Game1.GameContent.Load<SpriteFont>("SpriteFont1"), 
                currentMessage, new Vector2(10, 100), c * ((float)Math.Abs(textBrightness) / 255f));
        }

        protected void nextText()
        {
            brightenText = true;
            burst = false;
            textTimer = 0;
            textIndex++;

            if (textIndex < messages.Count)
            {
                currentMessage = messages[textIndex];
            }
            else
            {
                over = true;
            }
        }

        protected void spawnParticle(Color color)
        {
            spawnTime = rand.Next(20, 35);
            int num = rand.Next(1, 3);

            for (int i = 0; i < num; i++)
            {
                Particle p = new Particle(color, rand.Next(2, 4), new Vector2(0, rand.Next(80, 400)), Vector2.Zero, ParticleType.AMBIENT);
                p.adjustAccel(new Vector2((float)(rand.NextDouble() * .95f), 
                    (float)(rand.Next(-2, 2) * rand.NextDouble()) * .95f));
                ambient.Add(p);
            }
        }
    }
}
