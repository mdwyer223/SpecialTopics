using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class ParticleEngine
    {
        List<Particle> particles;
        Random rand;
        int spawnTimer, spawnTime;

        const int MAX_SIZE = 4, MIN_SIZE = 1,
            MAX_VELO = 4, MIN_VELO = 2,
            MAX_SPAWN = 2, MIN_SPAWN = 1;

        public ParticleEngine()
        {
            particles = new List<Particle>();
            rand = new Random();
            spawnTimer = spawnTime = rand.Next(MIN_SPAWN, MAX_SPAWN);
        }

        public void SimulateRain(GameTime gameTime)
        {
            int blue = rand.Next(50, 255);
            if (spawnTimer >= spawnTime)
            {
                int checkVelo = rand.Next(1, 10);

                if (checkVelo < 11)
                {
                    particles.Add(getParticle(0, 0, blue, false, true));
                    particles.Add(getParticle(0, 0, blue, false, true));
                    particles.Add(getParticle(0, 0, blue, false, true));
                }
                else
                {
                    particles.Add(getParticle(0, 0, blue, true, true));
                }

                spawnTimer = 0;
                spawnTime = rand.Next(MIN_SPAWN, MAX_SPAWN);
            }
            else
            {
                spawnTimer++;
            }


            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i] != null)
                {
                    particles[i].Update(gameTime);
                    if(particles[i].OffScreen)
                    {
                        particles.RemoveAt(i);
                        i--;
                        if (i < 0)
                            i = 0;
                    }
                }
            }
        }

        public Particle getParticle(Color color, bool xVelo, bool yVelo)
        {
            Particle p;

            if (xVelo && yVelo)
            {
                p = new Particle(color, rand.Next(MIN_SIZE, MAX_SIZE),
                    new Vector2(rand.Next(0, Game1.DisplayWidth), -5),
                    new Vector2(rand.Next(-MAX_VELO, MAX_VELO), rand.Next(MIN_VELO, MAX_VELO)));
            }
            else if (yVelo)
            {
                p = new Particle(color, rand.Next(MIN_SIZE, MAX_SIZE),
                    new Vector2(rand.Next(0, Game1.DisplayWidth), -5),
                    new Vector2(0, rand.Next(MIN_VELO, MAX_VELO)));
            }
            else if (xVelo)
            {
                p = new Particle(color, rand.Next(MIN_SIZE, MAX_SIZE),
                    new Vector2(-5, rand.Next(0, Game1.DisplayHeight)),
                    new Vector2(rand.Next(MIN_VELO, MAX_VELO), 0));
            }
            else
                p = null;

            return p;
        }

        public Particle getParticle(int r, int g, int b, bool xVelo, bool yVelo)
        {
            Particle p;

            if (xVelo && yVelo)
            {
                p = new Particle(new Color(r, g, b), rand.Next(MIN_SIZE, MAX_SIZE),
                    new Vector2(rand.Next(0, Game1.DisplayWidth), -5),
                    new Vector2(rand.Next(-MAX_VELO, MAX_VELO), rand.Next(MIN_VELO, MAX_VELO)));
            }
            else if (yVelo)
            {
                p = new Particle(new Color(r,g,b), rand.Next(MIN_SIZE, MAX_SIZE),
                    new Vector2(rand.Next(0, Game1.DisplayWidth), -5),
                    new Vector2(0, rand.Next(MIN_VELO, MAX_VELO)));
            }
            else if (xVelo)
            {
                p = new Particle(new Color(r,g,b), rand.Next(MIN_SIZE, MAX_SIZE),
                    new Vector2(-5, rand.Next(0, Game1.DisplayHeight)),
                    new Vector2(rand.Next(MIN_VELO, MAX_VELO), 0));
            }
            else
                p = null;

            return p;
        }

        public Particle getExplosionParticle(Vector2 start, Vector2 velocity, int damage)
        {
            Color color = new Color(255, rand.Next(40, 200), 0);
            Particle p = new Particle(color, rand.Next(2, 5), damage, (float)(rand.NextDouble() * 2) ,start, velocity);

            return p;
        }

        public List<Particle> generateExplosion(Vector2 exploStart, int damage)
        {
            List<Particle> pStuff = new List<Particle>();

            int numParticles = rand.Next(30, 60);
            for (int i = 0; i < numParticles; i++)
            {
                float xVelo = (float)rand.NextDouble() * rand.Next(-4, 4);
                float yVelo = (float)rand.NextDouble() * rand.Next(-4, 4);

                while (yVelo == 0 || xVelo == 0)
                {
                    xVelo = (float)rand.NextDouble() * rand.Next(-4, 4);
                    yVelo = (float)rand.NextDouble() * rand.Next(-4, 4);
                }

                pStuff.Add(getExplosionParticle(exploStart, 
                    new Vector2(xVelo, yVelo), damage));
            }

            return pStuff;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (particles.Count == 0)
                return;
            else
            {
                for (int i = 0; i < particles.Count; i++)
                {
                    if (particles[i] != null)
                    {

                        particles[i].Draw(spriteBatch);
                    }
                }
            }
        }

        public void clearList()
        {
            particles.Clear();
        }
    }
}
