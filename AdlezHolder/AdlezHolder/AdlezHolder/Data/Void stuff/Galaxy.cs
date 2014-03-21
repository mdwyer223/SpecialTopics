using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Galaxy
    {
        protected List<Particle> particles;
        List<Particle> head;
        protected Random rand;
        Vector2 center;
        int arms, centerRadius;

        float time;

        public Galaxy(int radiusOfCenter, int arms)
            : base()
        {
            this.arms = arms;
            this.centerRadius = radiusOfCenter;
            center = new Vector2(Game1.DisplayWidth / 2, Game1.DisplayHeight / 2);

            rand = new Random();
            particles = new List<Particle>();
            head = new List<Particle>();

            //spawnCenter();

            float angleChange = MathHelper.TwoPi / arms;

            for (int i = 0; i < arms; i++)
            {
                float angleToAdjust = angleChange * i;
                makeSpiralArm(angleToAdjust);
            }

            //swirl();
        }

        public virtual void Update(GameTime gameTime, MapDataHolder data)
        {
            time += (float)gameTime.ElapsedGameTime.TotalMinutes;

            for (int i = 0; i < particles.Count; i++)
            {
                if (time < 1)
                {
                    double radius = measureDistance(particles[i].Position, center);
                    float x = (float)((particles[i].Position.X - center.X));
                    float y = (float)((particles[i].Position.Y - center.Y));

                    Vector2 vecToMove = new Vector2(-y, x);
                    vecToMove.Normalize();
                    vecToMove *= .075f;
                    particles[i].adjustVelo(vecToMove);
                }

                else
                {
                    double radius = measureDistance(particles[i].Position, center);
                    float x = (float)((particles[i].Position.X - center.X));
                    float y = (float)((particles[i].Position.Y - center.Y));

                    double speed = (MathHelper.TwoPi / 5) * radius;
                    float angle = (float)(Math.Atan2((particles[i].Position.Y - center.Y), (particles[i].Position.X - center.X)));
                    float addedAngle = MathHelper.TwoPi * .0001f;
                        
                    x = (float)(Math.Cos(angle + addedAngle) * radius) + center.X;
                    y = (float)(Math.Sin(angle + addedAngle) * radius) + center.Y;

                    particles[i].Position = new Vector2(x, y);
                    particles[i].adjustVelo(new Vector2(0, 0));
                    
                }

                if (particles[i] != null)
                {
                    particles[i].Update(gameTime);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle p in particles)
            {
                if (p != null)
                {
                    p.Draw(spriteBatch);
                }
            }

            foreach (Particle h in head)
            {
                if (h != null)
                {
                    h.Draw(spriteBatch);
                }
            }

            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Random/Particle"), new Rectangle((int)center.X, (int)center.Y, 3, 3), Color.Yellow);
        }

        protected void spawnCenter()
        {
            for (int i = 0; i < 100; i++)
            {
                head.Add(new Particle(Color.White, 3, new Vector2(center.X + (float)(rand.Next(-centerRadius, centerRadius) * rand.NextDouble()),
                    center.Y + (float)(rand.Next(-centerRadius, centerRadius) * rand.NextDouble())), Vector2.Zero));
            }
        }

        protected void makeSpiralArm(float angle)
        {
            List<Particle> armParts = new List<Particle>();
            float curveY = 0f, distanceX = 0f;

            //400
            int numSectionsPerArm = 600;

            for (int j = 0; j < numSectionsPerArm; j++)
            {
                //one section of the arm per deltaX, 7 per each segment
                for (int i = 0; i < 2; i++)
                {
                    float changeinY = (float)(rand.Next(-100, 100) * rand.NextDouble());
                    float changeinX = (float)(rand.Next(-100, 100) * rand.NextDouble());

                    armParts.Add(new Particle(Color.White, 2, new Vector2(center.X + centerRadius + distanceX, center.Y + curveY + changeinY), Vector2.Zero));
                }
                distanceX += 1f;
                curveY += (float)(-.0001 * (j^2));
            }

            if (angle != 0)
            {
                foreach (Particle p in armParts)
                {
                    float radius = (float)measureDistance(p.Position, center);
                    float interiorAngle = (float)((angle - Math.PI) / 2f);
                    float changeInDist = (float)Math.Sqrt((2 * (Math.Pow(radius, 2f))) - ((2 * ((Math.Pow(radius, 2f)) * Math.Cos(angle)))));
                    float angleToChange = MathHelper.PiOver2 - interiorAngle;
                    float x = p.Position.X + (centerRadius * (float)(Math.Sin((double)angle)));
                    x = (float)(changeInDist * Math.Sin(angleToChange));
                    float y = p.Position.Y + (centerRadius * (float)(Math.Cos((double)angle)));
                    y = (float)(changeInDist * Math.Cos(angleToChange));
                    p.Position = new Vector2(p.Position.X - x, p.Position.Y - y);
                }
            }
            foreach (Particle p in armParts)
            {
                particles.Add(p);
            }
        }

        protected void swirl()
        {
            for (int a = 0; a < 5; a++)
            {
                for (int i = 0; i < particles.Count; i++)
                {
                    double radius = measureDistance(particles[i].Position, center);
                    float x = (float)((particles[i].Position.X - center.X));
                    float y = (float)((particles[i].Position.Y - center.Y));

                    //double speed = (MathHelper.TwoPi / 5) * radius;
                    //float angle = (float)(Math.Atan2((particles[i].Position.X - center.X), (particles[i].Position.Y - center.Y)));
                    //float newAngle = MathHelper.PiOver2 - angle;

                    //x = (float)(Math.Cos(newAngle) * speed);
                    //y = (float)(Math.Sin(newAngle) * speed);

                    Vector2 vecToMove = new Vector2(-y, x);
                    vecToMove.Normalize();
                    vecToMove *= 5f;
                    particles[i].adjustVelo(vecToMove);

                    if (particles[i] != null)
                    {
                        particles[i].Update(new GameTime());
                    }
                }
            }
        }

        protected double measureDistance(Vector2 Point1, Vector2 Point2)
        {
            double angle;
            double distance;
            if (Point1.X == Point2.X)
                return Math.Abs(Point1.Y - Point2.Y);
            if (Point1.Y == Point2.Y)
                return Math.Abs(Point1.X - Point2.X);


            Vector2 trig;
            trig.X = Math.Abs(Point1.X - Point2.X);
            trig.Y = Math.Abs(Point1.Y - Point2.Y);

            angle = Math.Atan2(trig.X, trig.Y);

            distance = trig.X / Math.Sin(angle);

            return distance;
        }
    }

    /// <summary>
    /// Not as good as a spiral galaxy, looks like a star field spinning, or bad
    /// </summary>
    public class RandomGalaxy : Galaxy
    {
        public RandomGalaxy(int radius)
            : base(radius, 0)
        {
            spawnField();
        }

        protected void spawnField()
        {
            for (int i = 0; i < 8000; i++)
            {
                Vector2 pos = new Vector2(rand.Next(-200, 1000), rand.Next(-100, 580));
                particles.Add(new Particle(Color.White, 2, pos, Vector2.Zero));
            }
        }
    }

    public class StarField
    {
        Random rand;
        List<Particle> particles;
        List<Comet> comets;

        int cometTimer, cometTime, minSpawnComet = 200, maxSpawnComet = 400;

        public StarField()
            : base()
        {
            particles = new List<Particle>();
            comets = new List<Comet>();

            rand = new Random();
            for (int i = 0; i < 4000; i++)
            {
                Color c;
                int value = rand.Next(1,4);
                if (value == 1)
                {
                    c = Color.Yellow;
                }
                else if (value == 2)
                {
                    c = Color.White;
                }
                else
                {
                    c = Color.White;
                }
                particles.Add(new Particle(c, rand.Next(1, 2), rand.Next(2,3),
                    new Vector2(rand.Next(0, Game1.DisplayWidth), rand.Next(0, Game1.DisplayHeight)), ParticleType.STAR));
            }
        }

        public void Update(GameTime gameTime, MapDataHolder data)
        {
            if (cometTimer <= cometTime)
            {
                cometTimer++;
            }
            else
            {
                spawnComet();
            }

            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i] != null)
                {
                    particles[i].Update(gameTime);
                    if (particles[i].OffScreen)
                    {
                        particles.RemoveAt(i);
                        if (particles[i].PType == ParticleType.STAR)
                        {
                            spawnParticle();
                        }
                    }
                }
            }

            for (int i = 0; i < comets.Count; i++)
            {
                if (comets[i] != null)
                {
                    comets[i].Update(gameTime, data);
                    if (comets[i].OffScreen)
                    {
                        comets.RemoveAt(i);
                        i--;
                        if (i < 0)
                            i = 0;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle p in particles)
            {
                if(p != null)
                    p.Draw(spriteBatch);
            }
        }

        protected void spawnParticle()
        {
            Color c;
            int value = rand.Next(1, 4);
            if (value == 1)
            {
                c = Color.Yellow;
            }
            else if (value == 2)
            {
                c = Color.White;
            }
            else
            {
                c = Color.White;
            }
            Particle p = new Particle(c, rand.Next(1, 3), rand.Next(2,3),
                    new Vector2(rand.Next(0, Game1.DisplayWidth), rand.Next(0, Game1.DisplayHeight)), ParticleType.STAR);
            particles.Add(p);
        }

        protected void spawnComet()
        {
            comets.Add(new Comet(new Vector2(0, rand.Next(50, 450))));
            cometTime = rand.Next(minSpawnComet, maxSpawnComet);
            cometTimer = 0;
        }
    }

    public class Comet
    {
        Vector2 position, velo, accel, jerk;
        Random rand;

        bool noHead = true;

        public bool OffScreen
        {
            get { return position.X > Game1.DisplayWidth || position.Y > Game1.DisplayHeight; }
        }

        public Comet(Vector2 start)
            :base()
        {
            rand = new Random();

            position = start;

            velo = new Vector2(rand.Next(3,7), rand.Next(-1,1));
            accel = new Vector2((float)rand.NextDouble() * .001f, (float)rand.NextDouble() * .01f);
        }

        public virtual void Update(GameTime gameTime, MapDataHolder data)
        {
            if (noHead)
            {
                spawnHead(data);
                noHead = false;
            }
            accel += jerk;
            velo += accel;
            position += velo;

            spawnTail(data);
        }

        public virtual void spawnTail(MapDataHolder data)
        {
            for (int i = 0; i < 5; i++)
            {
                data.addParticle(new Particle(new Color(255, rand.Next(175, 255), rand.Next(150, 255)), rand.Next(1, 2),
                    new Vector2(position.X , position.Y + 1), new Vector2((float)(velo.X * rand.NextDouble()) ,(float)(rand.Next(-1, 1) * rand.NextDouble())), ParticleType.COMET_TAIL));
            }

            for (int i = 0; i < 5; i++)
            {
                data.addParticle(new Particle(new Color(255, rand.Next(175, 255), rand.Next(150, 255)), rand.Next(1, 2),
                    new Vector2(position.X, position.Y - 1), new Vector2((float)(velo.X * rand.NextDouble()), (float)(rand.Next(-1, 1) * rand.NextDouble())), ParticleType.COMET_TAIL));
            }
        }

        public virtual void spawnHead(MapDataHolder data)
        {
            for (int i = 0; i < 100; i++)
            {
                data.addParticle(new Particle(Color.White, 2, new Vector2(position.X + (float)(rand.Next(-10, 10) * rand.NextDouble()), 
                    position.Y + (float)(rand.Next(-10, 10) * rand.NextDouble())), 10, velo, accel, ParticleType.TELEPORT));
            }
        }
    }
}
