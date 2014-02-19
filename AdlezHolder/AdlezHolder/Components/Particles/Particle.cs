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

        public bool OffScreen
        {
            get { return position.Y > Game1.DisplayHeight || position.X > Game1.DisplayWidth || position.X < 0 || (lifeLengthTimer / 1000) >= lifeLength; }
        }

        public int Damage
        {
            get {
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
        }

        public void Update(GameTime gameTime)
        {
            damage -= .4f;
            lifeLengthTimer += gameTime.ElapsedGameTime.Milliseconds;
            position += velo;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(blankTexture, Rec, color);
        }
    }
}
