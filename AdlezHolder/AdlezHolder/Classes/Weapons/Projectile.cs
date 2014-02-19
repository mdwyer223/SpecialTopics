using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Projectile : BaseSprite
    {
        Vector2 velocity;
        int distance, maxRange, damage;
        bool noDamage;

        public bool MaxRange
        {
            get { return distance >= maxRange; }
        }
        
        public Projectile(int damage, int maxRange, float speed, float scaleFactor, 
            Vector2 velocity, Vector2 start, Texture2D texture)
            : base(texture, scaleFactor, Game1.DisplayWidth, 4, start)
        {
            this.damage = damage;
            this.maxRange = maxRange;
            this.velocity = velocity;
            noDamage = false;

            IsDead = false;
            IsVisible = true;
        }

        public override void Update(Map data, GameTime gameTime)
        {
            if (distance <= maxRange)
            {
                List<BaseSprite> sprites = data.CurrentData.Everything;
                position = new Vector2(position.X + velocity.X, position.Y + velocity.Y);

                distance = (int)(distance + Math.Abs(velocity.Y) + Math.Abs(velocity.X));

                for (int i = 0; i < sprites.Count; i++)
                {
                    if (sprites[i] != null)
                    {
                        if (sprites[i] != this && this.CollisionRec.Intersects(sprites[i].CollisionRec) 
                            && !sprites[i].IsDead)
                        {
                            if (sprites[i].GetType() == typeof(Character))
                            {
                                Character s = (Character)sprites[i];
                                s.damage(damage);
                            }
                            else if (sprites[i].GetType() == typeof(Skeleton))
                            {
                                Skeleton s = (Skeleton)sprites[i];
                                s.damage(data.CurrentData, damage);
                            }
                            IsVisible = false;
                            IsDead = true;
                        }
                    }
                }
            }       
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, CollisionRec, Color.White);
        }
    }
}
