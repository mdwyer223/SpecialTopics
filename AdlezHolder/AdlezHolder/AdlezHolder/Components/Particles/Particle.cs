using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public enum ParticleType { RAIN, SNOW, SAND, NONE };

    public class Particle
    {
        Texture2D blankTexture, rainTexture;
        Texture2D[] rainAnimation;
        Color color;
        Rectangle rec;
        Vector2 velo, position, accel = Vector2.Zero;
        ParticleType pType = ParticleType.NONE;

        float lifeLength;
        int lifeLengthTimer;

        bool stopping;
        int secondsToTravel, travelTimer;
        int animeIndex = 0, animeTimer;

        float damage;
        float damageReduction;

        List<GemType> types;
        List<GemStruct> gemEffects;

        int maxAccel = 2, minAccel = -2;
        float accelPerTick;
        int accelTimer = 0, accelTime = 0;

        public List<GemType> Types
        {
            get { return types; }
        }

        public List<GemStruct> GemEffects
        {
            get { return gemEffects; }
        }

        public bool OffScreen
        {
            get { return (position.Y > Game1.DisplayHeight || position.X > Game1.DisplayWidth || position.X < 0)
                || lifeLengthTimer >= lifeLength * 1000; }
        }

        public int Damage
        {
            get 
            {
                if (damage >= 0)
                    return (int)damage;
                else
                    return 0;
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Rectangle Rec
        {
            get { return new Rectangle((int)position.X, (int) position.Y,
                rec.Width, rec.Height); }
        }

        public Particle(Color color, int size, Vector2 start, Vector2 velocity)
        {
            this.color = color;
            position = start;
            rec = new Rectangle((int)start.X, (int)start.Y, size, size);
            this.velo = velocity;

            blankTexture = Game1.GameContent.Load<Texture2D>("Random/Particle");

            lifeLength = int.MaxValue;
            lifeLengthTimer = 0;
            gemEffects = new List<GemStruct>();
            types = new List<GemType>();

            getAccel();
        }

        public Particle(int r, int g, int b, int size, Vector2 start, Vector2 velocity)
        {
            getAccel();
            this.color = new Color(r, g, b);
            position = start;
            rec = new Rectangle((int)start.X, (int)start.Y, size, size*2);
            this.velo = velocity;

            blankTexture = Game1.GameContent.Load<Texture2D>("Random/Particle");

            lifeLength = int.MaxValue;
            lifeLengthTimer = 0;
            gemEffects = new List<GemStruct>();
            types = new List<GemType>();
        }

        public Particle(Color color, int size, Vector2 start, Vector2 velocity, ParticleType t)
        {
            getAccel();
            this.color = color;
            position = start;
            rec = new Rectangle((int)start.X, (int)start.Y, size, size);
            this.velo = velocity;

            blankTexture = Game1.GameContent.Load<Texture2D>("Random/Particle");

            lifeLength = int.MaxValue;
            lifeLengthTimer = 0;
            gemEffects = new List<GemStruct>();
            types = new List<GemType>();

            pType = t;
        }

        public Particle(int r, int g, int b, int size, Vector2 start, Vector2 velocity, ParticleType t)
        {
            getAccel();
            this.color = new Color(r, g, b);
            position = start;
            rec = new Rectangle((int)start.X, (int)start.Y, size, size * 2);
            this.velo = velocity;

            blankTexture = Game1.GameContent.Load<Texture2D>("Random/Particle");

            lifeLength = int.MaxValue;
            lifeLengthTimer = 0;
            gemEffects = new List<GemStruct>();
            types = new List<GemType>();

            pType = t;
        }

        public Particle(Color color, int size, int damage, float secondsAlive, Vector2 start, Vector2 velocity)
        {
            getAccel();
            this.color = color;
            position = start;
            rec = new Rectangle((int)start.X, (int)start.Y, size, size * 2);
            this.velo = velocity;

            blankTexture = Game1.GameContent.Load<Texture2D>("Random/Particle");

            lifeLength = secondsAlive;
            lifeLengthTimer = 0;

            this.damage = damage;

            gemEffects = new List<GemStruct>();
            types = new List<GemType>();

            calcDamage(damage);
        }

        public Particle(int r, int g, int b, int size, int damage, float secondsAlive, Vector2 start, Vector2 velocity)
        {
            getAccel();
            this.color = new Color(r, g, b);
            position = start;
            rec = new Rectangle((int)start.X, (int)start.Y, size, size * 2);
            this.velo = velocity;

            blankTexture = Game1.GameContent.Load<Texture2D>("Random/Particle");

            lifeLength = secondsAlive;
            lifeLengthTimer = 0;

            this.damage = damage;

            gemEffects = new List<GemStruct>();
            types = new List<GemType>();

            calcDamage(damage);
        }

        public void Update(GameTime gameTime)
        {
            if (pType == ParticleType.NONE)
            {
                damage += damageReduction;
            }
            else if (pType == ParticleType.SNOW)
            {
                accelTimer++;
                accel.X += accelPerTick;
                velo += accel;
                if (accelTimer >= accelTime)
                {
                    accelTimer = 0;
                    getAccel();
                }
            }
            else if (pType == ParticleType.RAIN)
            {
                if (travelTimer < secondsToTravel)
                {
                    travelTimer++;
                }
                else
                {
                    travelTimer = secondsToTravel;
                    rainSploosh();
                }
            }

            lifeLengthTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (pType != ParticleType.RAIN)
            {
                position += velo;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(blankTexture, Rec, color);
        }

        protected void getAccel()
        {
            Random rand = new Random();
            accelTime = 50;
            accel.Y = 0;
            int scale = rand.Next(1, 3);

            if (scale == 1)
            {
                accelPerTick = (float)((((rand.NextDouble() * .00025f) * scale) / accelTime));
            }
            else if (scale == 2)
            {
                accelPerTick = (float)((((rand.NextDouble() * -.00025f) * scale) / accelTime));
            }
        }

        protected void rainSploosh()
        {
            if(animeTimer % 10 == 0)
            {
                if(animeIndex + 1 < rainAnimation.Length -1)
                {
                    animeIndex++;
                }
            }

        }

        public void addGemStruct(GemStruct newStruct)
        {
            this.gemEffects.Add(newStruct);
        }

        public void addGemType(GemType type)
        {
            this.types.Add(type);
        }

        public void calcDamage(int damage)
        {
            int endDamage = (int)((damage * .2f) + .5f);
            damageReduction = (endDamage - damage) / (lifeLength * 60);
        }
    }
}
