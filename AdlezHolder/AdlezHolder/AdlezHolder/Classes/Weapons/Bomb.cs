using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Bomb
    {
        Texture2D texture;
        ParticleEngine pEngine;
        List<BombObj> bombs;

        int damage, delay, speed, numParticles;
        float scaleFactor;

        public void upgradeDamage(float percent)
        {
            damage *= (int)(percent + 1);
        }
        public void upgradeSpeed(float percent)
        {
            speed *= (int)(percent + 1);
        }
        public void upgradeParticles(float percent)
        {
            numParticles *= (int)(percent + 1);
        }

        public Bomb(float scaleFactor)
        {
            bombs = new List<BombObj>();
            pEngine = new ParticleEngine();

            damage = 50;
            delay = 3000;

            this.scaleFactor = scaleFactor;
            texture = Game1.GameContent.Load<Texture2D>("Weapons/Bomb");
        }

        public void Update(Map data, GameTime gameTime)
        {
            for (int i = 0; i < bombs.Count; i++)
            {
                if (bombs[i] != null)
                {
                    bombs[i].Update(data, gameTime);

                    if (bombs[i].BlowUp)
                    {
                        blowUp(data, bombs[i].Center);
                        bombs.RemoveAt(i);
                        i--;
                        if (i < 0)
                            i = 0;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (BombObj bomb in bombs)
            {
                if (bomb != null)
                {
                    //bomb.Draw(spriteBatch);
                }
            }
        }

        public void blowUp(Map data, Vector2 start)
        {
            List<Particle> particles = pEngine.generateExplosion(start, damage);
            foreach (Particle p in particles)
            {
                data.CurrentData.addParticle(p);
            }

            particles = null;
        }

        public void addBomb(Vector2 start, MapDataHolder data)
        {
            BombObj obj = new BombObj(texture, scaleFactor,
                start, delay);
            bombs.Add(obj);
            data.addBaseSprite(obj);

        }
    }
}
