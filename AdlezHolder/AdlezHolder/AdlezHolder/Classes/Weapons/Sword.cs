using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Sword
    {
        float speed, lifeSteal = 0f;
        int damage, range;
        int upgradeDamageLev, upgradeRangeLev, 
            upgradeSpeedLev;
        bool dead = true;
        bool stoleLife = false;

        Color color;
        Texture2D texture;
        Rectangle collisionRec;

        List<Gem> gemList;
        int gemSlots;
        
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

        public int getDamageUpgradeLev
        {
            get{return upgradeDamageLev;}
        }

        public int getRangeUpgradeLev
        {
            get{return upgradeRangeLev;}
        }

        public int getSpeedUpgradeLev
        {
            get{return upgradeSpeedLev;}
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

        public Sword(float scaleFactor)
        {   
            texture = Game1.GameContent.Load<Texture2D>("AlistarSwordAttack/LeftSwoosh");
            color = Color.White;

            collisionRec.Width = (int)(Game1.DisplayWidth * scaleFactor + 0.5f);
            float aspectRatio = (float)texture.Width / texture.Height;
            collisionRec.Height = (int)(collisionRec.Width / aspectRatio + 0.5f);

            upgradeDamageLev = 1;
            upgradeRangeLev = 1;
            upgradeSpeedLev = 1;
            damage = 10;
            range = 5;
            speed = 5;

            gemList = new List<Gem>();
        }

        public void UpgradeDamage()
        {
            if (upgradeDamageLev <= 5)
            {
                damage = (int)(damage * 1.25);
                upgradeDamageLev++;
            }
            
        }
           
        public void UpgradeRange()
        {
            if (upgradeRangeLev <= 5)
            {
                range = (int)(range * 1.25);
                upgradeRangeLev++;
            }

        }

        public void UpgradeSpeed()
        {
            if (upgradeSpeedLev <= 5)
            {
                speed = (int)(speed * 1.25);
                upgradeSpeedLev++;
            }
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
                    if (collisionRec.Intersects(enemy.CollisionRec))
                    {
                        damageEnemy(enemy, data, player);
                    }
                }
            }
            else
            {
                stoleLife = false;
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
            float burnChance = .25f, poisonChance = .25f, lightningChance = .25f,
                freezeChance = .15f, vampirePercent = .10f;
            float burnDuration = 3f, poisonDuration = 20f, stunDuration = 1f, 
                freezeDuration = 3f;
            int burnDamage = 45, poisonDamage = 60, freezeDamage = damage;

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
                    vampirePercent = gemList[i].Chance;
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

            enemy.damage(data, damage);
            if (vampirePercent > 0)
            {
                if (!stoleLife)
                {
                    player.heal((int)(vampirePercent * damage));
                    stoleLife = true;
                }
            }
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
    }
}
