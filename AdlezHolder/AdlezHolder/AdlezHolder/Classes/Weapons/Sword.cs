using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public struct GemStruct
    {
        public float duration;
        public int damage;
        public float chance;
    }

    public class Sword
    {
        float speed, reductionTime;
        int damage, range, originalDamage, reductionTimer;
        bool dead = true;

        Color color;
        Texture2D texture;
        Rectangle collisionRec;

        List<Gem> gemList;
        int gemSlots;

        GemStruct lifestealStruct, freezeStruct, poisonStruct, 
            lightningStruct, fireStruct;

        public float Speed
        {
            get{ return speed; }
        }

        public int Damage
        {
            get { return damage; }
        }
       
        public int Range
        {
            get{ return range; }
        }

        public int MaxGemSlots
        {
            get { return gemSlots; }
        }

        public bool isDead
        {
            get { return dead; }
        }

        public Rectangle CollisionRec
        {
            get { return collisionRec; }
        }

        public List<Gem> Gems
        {
            get { return gemList; }
        }

        public GemStruct LifeStealStruct
        {
            get { return lifestealStruct; }
        }

        public GemStruct FireStruct
        {
            get { return fireStruct; }
        }

        public GemStruct PoisonStruct
        { 
            get { return poisonStruct; } 
        }

        public GemStruct StunStruct
        {
            get { return lightningStruct; }
        }

        public GemStruct IceStruct
        {
            get { return freezeStruct; }
        }

        private Sword()
        {
        }

        public Sword(float scaleFactor)
        {
            gemList = new List<Gem>();
            texture = Game1.GameContent.Load<Texture2D>("AlistarSwordAttack/LeftSwoosh");
            color = Color.White;

            collisionRec.Width = (int)(Game1.DisplayWidth * scaleFactor + 0.5f);
            float aspectRatio = (float)texture.Width / texture.Height;
            collisionRec.Height = (int)(collisionRec.Width / aspectRatio + 0.5f);

            originalDamage = damage = 10;
            range = 5;
            speed = 5;

            gemList = new List<Gem>();
            gemSlots = 3;

            lifestealStruct = new GemStruct();
            freezeStruct = new GemStruct();
            fireStruct = new GemStruct();
            poisonStruct = new GemStruct();
            lightningStruct = new GemStruct();

            VampiricStone vStone = new VampiricStone(.02f, new Vector2(0, 0), 1);
            IceStone iStone = new IceStone(.02f, Vector2.Zero, 1);
            gemList.Add(iStone);
            gemList.Add(vStone);
        }

        public void Update(MapDataHolder data, Character player,GameTime gameTime)
        {
            //add in a check for direction
            //load different texture for each direction, just a simple set thing
            if (player.Direction == Orientation.DOWN)
            {
                collisionRec.X = (int)player.Position.X;
                collisionRec.Y = (int)(player.Position.Y + player.CollisionRec.Height);
                texture = Game1.GameContent.Load<Texture2D>("AlistarSwordAttack/FrontSwoosh");
            }
            else if (player.Direction == Orientation.UP)
            {
                collisionRec.X = (int)player.Position.X;
                collisionRec.Y = (int)(player.Position.Y - collisionRec.Height);
                texture = Game1.GameContent.Load<Texture2D>("AlistarSwordAttack/BackSwoosh");
            }
            else if (player.Direction == Orientation.RIGHT)
            {
                collisionRec.X = (int)(player.Position.X + player.CollisionRec.Width);
                collisionRec.Y = (int)player.Position.Y;
                texture = Game1.GameContent.Load<Texture2D>("AlistarSwordAttack/RightSwoosh");
            }
            else if (player.Direction == Orientation.LEFT)
            {
                collisionRec.X = (int)(player.Position.X - player.CollisionRec.Width);
                collisionRec.Y = (int)(player.Position.Y);
                texture = Game1.GameContent.Load<Texture2D>("AlistarSwordAttack/LeftSwoosh");
            }

            if (!dead)
            {
                foreach (Enemy enemy in data.Enemies)
                {
                    if (collisionRec.Intersects(enemy.CollisionRec) && !enemy.IsDead)
                    {
                        damageEnemy(enemy, data, player);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!dead)
            {
                spriteBatch.Draw(texture, collisionRec, color);
            }
        }

        public void damageEnemy(Enemy enemy, MapDataHolder data, Character player)
        {
            float burnChance = 0f, poisonChance = 0f, lightningChance = 0f,
                freezeChance = 0f, vampirePercent = 0f;
            float burnDuration = 0f, poisonDuration = 0f, stunDuration = 0f, 
                freezeDuration = 0f;
            int burnDamage = 0, poisonDamage = 0, freezeDamage = 0;

            for (int i = 0; i < gemList.Count; i++)
            {
                if (gemList[i].GetType() == typeof(FireStone))
                {
                    burnChance += gemList[i].Chance;
                    burnDamage += gemList[i].Damage;
                    burnDuration += gemList[i].Duration;
                }
                if (gemList[i].GetType() == typeof(VampiricStone))
                {
                    vampirePercent += gemList[i].Chance;
                }
                if (gemList[i].GetType() == typeof(PoisonStone))
                {
                    poisonChance += gemList[i].Chance;
                    poisonDamage += gemList[i].Damage;
                    poisonDuration += gemList[i].Duration;
                }
                if (gemList[i].GetType() == typeof(IceStone))
                {
                    freezeChance += gemList[i].Chance;
                    IceStone stone = (IceStone)(gemList[i]);
                    freezeDamage += (int)(stone.CritDamage * damage) + damage;
                    freezeDuration += gemList[i].Duration;
                    stone = null;
                }
                if (gemList[i].GetType() == typeof(LightningStone))
                {
                    lightningChance += gemList[i].Chance;
                    stunDuration += gemList[i].Duration;
                } 
            }

            if (reductionTime > 0)
            {
                reductionTimer++;
                if (reductionTimer >= reductionTime * 60)
                {
                    reductionTime = 0;
                    reductionTimer = 0;
                }
                else
                    damage = (int)((originalDamage * .75f) +.5f);
            }
            else
                damage = originalDamage;

            if (vampirePercent > 0)
            {
                player.heal((int)(vampirePercent * damage));
            }

            enemy.damage(data, damage);

            Random rand = new Random();
            //check all the stats with a new randy every time

            if (rand.NextDouble() < poisonChance)
            {
                enemy.poison(poisonDamage, poisonDuration);
            }

            if (rand.NextDouble() < burnChance)
            {
                enemy.burn(burnDamage, burnDuration);
            }

            if (rand.NextDouble() < freezeChance && !enemy.Frozen)
            {
                enemy.freeze(freezeDamage, freezeDuration);
            }

            if (rand.NextDouble() <= lightningChance)
            {
                enemy.stun(stunDuration);
            }
        }

        public void toggle(bool newValue)
        {
            dead = newValue;
        }

        public void addGem(Gem gem)
        {
            if (gemList.Count == MaxGemSlots)
                return;

            this.gemList.Add(gem);
        }

        public void calcStats()
        {
            float burnChance = 0f, poisonChance = 0f, lightningChance = 0f,
               freezeChance = 0f, vampirePercent = 0f;
            float burnDuration = 0f, poisonDuration = 0f, stunDuration = 0f,
                freezeDuration = 0f;
            int burnDamage = 0, poisonDamage = 0, freezeDamage = 0;

            for (int i = 0; i < gemList.Count; i++)
            {
                if (gemList[i].GetType() == typeof(FireStone))
                {
                    burnChance += gemList[i].Chance;
                    burnDamage += gemList[i].Damage;
                    burnDuration += gemList[i].Duration;
                }
                if (gemList[i].GetType() == typeof(VampiricStone))
                {
                    vampirePercent += gemList[i].Chance;
                }
                if (gemList[i].GetType() == typeof(PoisonStone))
                {
                    poisonChance += gemList[i].Chance;
                    poisonDamage += gemList[i].Damage;
                    poisonDuration += gemList[i].Duration;
                }
                if (gemList[i].GetType() == typeof(IceStone))
                {
                    freezeChance += gemList[i].Chance;
                    IceStone stone = (IceStone)(gemList[i]);
                    freezeDamage += (int)(stone.CritDamage * damage) + damage;
                    freezeDuration += gemList[i].Duration;
                    stone = null;
                }
                if (gemList[i].GetType() == typeof(LightningStone))
                {
                    lightningChance += gemList[i].Chance;
                    stunDuration += gemList[i].Duration;
                }
            }

            lifestealStruct.chance = vampirePercent;

            fireStruct.chance = burnChance;
            fireStruct.damage = burnDamage;
            fireStruct.duration = burnDuration;

            poisonStruct.chance = poisonChance;
            poisonStruct.damage = poisonDamage;
            poisonStruct.duration = poisonDuration;

            freezeStruct.chance = freezeChance;
            freezeStruct.damage = freezeDamage;
            freezeStruct.duration = freezeDuration;

            lightningStruct.chance = lightningChance;
            lightningStruct.duration = stunDuration;
        }

        public void reduceDamage(GemStruct gem)
        {
            reductionTime = gem.duration;
            damage = (int)(originalDamage * .75f);
        }
    }
}
