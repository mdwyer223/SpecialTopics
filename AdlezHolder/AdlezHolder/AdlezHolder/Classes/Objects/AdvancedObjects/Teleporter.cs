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
    public class Teleporter : ImmovableObject
    {
        List<Particle> particles;
        Rectangle modified;
        Random rand = new Random();
        string id;

        bool playerTouching;
        float spawnTime, spawnTimer,
            spawnTimeMax = 75, spawnTimeMin = 30,
            originMax = 75, originMin = 30;

        int brightness, lowValue = 75, lowOrigin = 75;
        bool countUp = true, countDown;

        // max when the player is not within the teleporter
        int maxParticles = 30;
        int delay = 50, delayTimer;

        public override Rectangle CollisionRec
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, modified.Width, modified.Height);
            }
            protected set
            {
                base.CollisionRec = value;
            }
        }

        public Teleporter(Rectangle newColl, string mapID)
            : base(null, .02f, 0, new Vector2(newColl.X, newColl.Y))
        {
            id = mapID;
            modified = newColl;

            this.color = Color.Cyan;
            particles = new List<Particle>();
            spawnParticle();
        }

        public override void Update(Map data, GameTime gametime)
        {
            playerTouching = data.Player.CollisionRec.Intersects(this.CollisionRec);

            if (countDown)
            {
                brightness -= 2;
                if (brightness <= lowValue)
                {
                    countUp = true;
                    countDown = false;
                }
            }
            else if (countUp)
            {
                brightness += 2;
                if (brightness >= 255)
                {
                    countDown = true;
                    countUp = false;
                }
            }

            if (spawnTimer < spawnTime)
            {
                spawnTimer++;
            }
            else
            {
                spawnTimer = spawnTime;
                if (!playerTouching)
                {
                    spawnTimeMax = originMax;
                    spawnTimeMin = originMin;
                    lowValue = lowOrigin;
                    if (particles.Count != maxParticles)
                    {
                        spawnParticle();
                    }
                }
                else
                {
                    data.Player.decreaseColor(true, false, false);
                    spawnTimer += 5;
                    spawnTime-=5;
                    if (spawnTime < 0)
                        spawnTime = 0;
                    spawnTimeMax-= 5;
                    if (spawnTimeMax < 1)
                    {
                        spawnTimeMax = 1;
                    }
                    spawnTimeMin-= 5;
                    if (spawnTimeMin < 0)
                    {
                        spawnTimeMin = 0;
                    }

                    spawnParticle();
                }
            }

            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i] != null)
                {
                    if (particles[i].OffScreen)
                    {
                        particles.RemoveAt(i);
                        if (i == particles.Count)
                        {
                            i--;
                            if (i < 0)
                                i = 0;
                        }
                    }
                    if (particles.Count != 0)
                    {
                        particles[i].Update(gametime);
                    }
                }
            }

            if (particles.Count > 800)
            {
                if (delayTimer < delay)
                {
                    delayTimer++;
                }
                else
                {
                    if (id.Equals("nwot"))
                    {
                        data.changeMap(new Nwot());
                    }
                    else if (id.Equals("mroom2"))
                    {
                        data.changeMap(new MainRoom2());
                    }
                    else if (id.Equals("mroom1"))
                    {
                        data.changeMap(new MainRoom());
                    }
                    else if (id.Equals("testingfield"))
                    {
                        data.changeMap(new TestingField());
                    }
                    data.Player.setTele(true);
                }
            }
            
            base.Update(data, gametime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Random/Particle"), CollisionRec, 
                this.color * ((float)Math.Abs(brightness) / 255f));
            //spriteBatch.Draw(texture, Rec,
              //  new Color(255, 215, 0) * (float)((Math.Abs(brightness) / 255f)));
            foreach (Particle p in particles)
            {
                if (p != null)
                    p.Draw(spriteBatch);
            }
        }

        protected void spawnParticle()
        {
            spawnTimer = 0;
            spawnTime = rand.Next((int)spawnTimeMin, (int)spawnTimeMax);

           particles.Add(new Particle(this.color, 2, new Vector2(rand.Next(CollisionRec.X, CollisionRec.X + CollisionRec.Width), CollisionRec.Y), 
                rand.Next(6,9), new Vector2(0, -rand.Next(0, 1)), new Vector2(0, -.002f), ParticleType.TELEPORT));
            if (playerTouching)
            {
                particles.Add(new Particle(this.color, 2, new Vector2(rand.Next(CollisionRec.X, CollisionRec.X + CollisionRec.Width), CollisionRec.Y),
                rand.Next(4, 8), new Vector2(0, -rand.Next(0, 1)), new Vector2(0, -.002f), ParticleType.TELEPORT));
                particles.Add(new Particle(this.color, 2, new Vector2(rand.Next(CollisionRec.X, CollisionRec.X + CollisionRec.Width), CollisionRec.Y),
                rand.Next(1,2), new Vector2(0, -rand.Next(0, 1)), new Vector2(.001f, -.002f), ParticleType.TELEPORT));
                particles.Add(new Particle(this.color, 2, new Vector2(rand.Next(CollisionRec.X, CollisionRec.X + CollisionRec.Width), CollisionRec.Y),
                rand.Next(1, 3), new Vector2(0, -rand.Next(0, 1)), new Vector2(-.001f, -.002f), ParticleType.TELEPORT));
                lowValue++;
                if (lowValue >= 255)
                    lowValue--;
            }
        }
    }
}
