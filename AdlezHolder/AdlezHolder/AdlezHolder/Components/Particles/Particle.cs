using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class Particle
    {
        Texture2D blankTexture;
        Color color;
        Rectangle rec;
        Vector2 velo, position;

        float lifeLength;
        int lifeLengthTimer;

        float damage;
        float damageReduction;

        List<GemType> types;
        List<GemStruct> gemEffects;

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
        }

        public Particle(int r, int g, int b, int size, Vector2 start, Vector2 velocity)
        {
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

        public Particle(Color color, int size, int damage, float secondsAlive, Vector2 start, Vector2 velocity)
        {
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
            damage += damageReduction;
            lifeLengthTimer += gameTime.ElapsedGameTime.Milliseconds;
            position += velo;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(blankTexture, Rec, color);
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
