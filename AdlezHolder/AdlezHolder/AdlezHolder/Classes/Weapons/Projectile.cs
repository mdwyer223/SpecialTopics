using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public enum GemType { LS, FREEZE, FIRE, STUN, POISON, NONE }

    public class Projectile : BaseSprite
    {
        Vector2 velocity;
        int distance, maxRange, damage;
        bool noDamage;

        List<GemStruct> gemEffects;
        List<GemType> types;

        public bool MaxRange
        {
            get { return distance >= maxRange; }
        }

        public List<GemType> Types
        {
            get { return types; }
        }

        public List<GemStruct> GemEffects
        {
            get { return gemEffects; }
        }

        public Projectile(int damage, int maxRange, float speed, float scaleFactor,
            Vector2 velocity, Vector2 start, Texture2D texture)
            : base(texture, scaleFactor, Game1.DisplayWidth, 4, start)
        {
            this.damage = damage;
            this.maxRange = maxRange;
            this.velocity = velocity;
            noDamage = false;

            types = new List<GemType>();
            gemEffects = new List<GemStruct>();

            IsDead = false;
            IsVisible = true;
        }

        public Projectile(int damage, int maxRange, float speed, float scaleFactor,
            Vector2 velocity, Vector2 start, Texture2D texture, GemType type)
            : base(texture, scaleFactor, Game1.DisplayWidth, 4, start)
        {
            this.damage = damage;
            this.maxRange = maxRange;
            this.velocity = velocity;
            noDamage = false;

            IsDead = false;
            IsVisible = true;

            types = new List<GemType>();
            gemEffects = new List<GemStruct>();
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
                    if (sprites[i] != null && sprites[i].GetType() != typeof(ArrowTrap) && sprites[i].GetType() != typeof(Wall) &&
                        sprites[i] != this)
                    {
                        if (this.CollisionRec.Intersects(sprites[i].CollisionRec)
                            && !sprites[i].IsDead)
                        {
                            if (sprites[i].GetType() == typeof(Character))
                            {
                                Character s = (Character)sprites[i];
                                s.damage(damage);
                            }
                            else if (sprites[i].GetType().IsSubclassOf(typeof(Enemy)))
                            {
                                Enemy s = (Enemy)sprites[i];
                                s.damage(data.CurrentData, damage);
                                if (types.Count != 0)
                                {
                                    for (int j = 0; j < types.Count; j++)
                                    {
                                        if (types[j] == GemType.FIRE)
                                        {
                                            s.burn(gemEffects[j].damage, gemEffects[j].duration);
                                        }
                                        else if (types[j] == GemType.FREEZE)
                                        {
                                            s.freeze(gemEffects[j].damage, gemEffects[j].duration);
                                        }
                                        else if (types[j] == GemType.POISON)
                                        {
                                            s.poison(gemEffects[j].damage, gemEffects[j].duration);

                                        }
                                        else if (types[j] == GemType.STUN)
                                        {
                                            s.stun(gemEffects[j].duration);

                                        }
                                        else if (types[j] == GemType.LS)
                                        {
                                            data.Player.heal((int)(gemEffects[j].chance * damage));

                                        }
                                    }
                                }
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

        public void addGemStruct(GemStruct newStruct)
        {
            this.gemEffects.Add(newStruct);
        }

        public void addGemType(GemType type)
        {
            this.types.Add(type);
        }
    }
}
