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
        float speed, reductionTime;
        int damage, range, originalDamage, reductionTimer;
        bool dead = true, hit = false;
        bool wave = false;
        int size, enchantmentSlots = 0;

        Color color;
        Texture2D texture;
        Rectangle collisionRec;

        List<Gem> gemList;
        int gemSlots;

        GemStruct lifestealStruct, freezeStruct, poisonStruct, 
            lightningStruct, fireStruct;

        public new SwordStruct SaveData
        {
            get
            {
                SwordStruct myStruct = new SwordStruct();
                myStruct.damage = Damage;
                myStruct.speed = Speed;
                myStruct.range = Range;

                myStruct.maxGems = MaxGemSlots;
                myStruct.gemsData = new GemStruct[MaxGemSlots];
                for(int i=0; i < gemList.Count; i++)
                    myStruct.gemsData[i] = this.gemList[i].SaveData;

                return myStruct;
            }
            set
            {
                damage = value.damage;
                speed = value.speed;
                range = value.range;
                gemSlots = value.maxGems;
                this.gemList = new List<Gem>();

                foreach(GemStruct gem in value.gemsData)
                    switch (gem.type)
                    {
                        case GemType.FIRE:
                            gemList.Add(new FireStone(gem));
                            break;
                        case GemType.FREEZE:
                            gemList.Add(new IceStone(gem));
                            break;
                        case GemType.LS:
                            gemList.Add(new VampiricStone(gem));
                            break;
                        case GemType.POISON:
                            gemList.Add(new PoisonStone(gem));
                            break;
                        case GemType.STUN:
                            gemList.Add(new LightningStone(gem));
                            break;
                    }

            }
        }
        public void UpgradeDamage(double multiplier)
        {
            damage = (int)(damage * multiplier);
        }

        public void UpgradeWave(bool onOff)
        {
            wave = onOff;
        }
        public void UpgradeSize(double multiplier)
        {
            size = (int)(size * multiplier);
        }
        public void UpgradeRange(double multiplier)
        {
            range = (int)(range * multiplier);
        }

        public void UpgradeSpeed(double multiplier)
        {
            speed = (int)(speed * multiplier);
        }


        public void increaseEnchantmentSlot(int numOfIncrease)
        {
            enchantmentSlots += numOfIncrease;
        }
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
        public int ESlots
        {
            get { return enchantmentSlots; }
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

            VampiricStone vStone = new VampiricStone(.02f, new Vector2(0, 0), 1, 1);
            IceStone iStone = new IceStone(.02f, Vector2.Zero, 1, 1);
            LightningStone lStone = new LightningStone(.02f, Vector2.Zero, 5, 1);
            gemList.Add(lStone);
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
                    if (collisionRec.Intersects(enemy.CollisionRec) && !enemy.IsDead && !hit)
                    {
                        damageEnemy(enemy, data, player);
                        hit = true;
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
        public UpgradeNode[,] GetTree(int displayWidth, int displayHeight)
        {
            Vector2 nodePosition;
            float scalefactor = .3f;
            Rectangle overScan;
            int widthSeperation, heightSeparation, nodeTopRow, nodeMiddleRow, nodeBottomRow;

            displayWidth = Game1.DisplayWidth;
            displayHeight = Game1.DisplayHeight;

            int marginWidth = (int)(displayWidth * .05);
            int marginHeight = (int)(displayHeight * .05);


            overScan.Width = displayWidth - marginWidth;
            overScan.Height = displayHeight - marginHeight;

            overScan.X = displayWidth + marginWidth;
            overScan.Y = displayHeight - marginHeight;

            heightSeparation = overScan.Height / 8;
            widthSeperation = overScan.Width / 4;

            nodePosition.X = widthSeperation;
            nodePosition.Y = heightSeparation;

            nodeBottomRow = heightSeparation * 4;
            nodeMiddleRow = heightSeparation * 3;
            nodeTopRow = heightSeparation * 2;
            int cost = 100;
            UpgradeNode[,] multiNodeArray;
            multiNodeArray = new UpgradeNode[7, 3];
            //Row1
            multiNodeArray[0, 0] = null;
            nodePosition.Y = nodeMiddleRow;
            multiNodeArray[0, 1] = new SwordDamageNode(Game1.GameContent.Load<Texture2D>("damage"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.25);
            multiNodeArray[0, 2] = null;
            //Row 2
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            multiNodeArray[1, 0] = new SwordESlotNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);//work on eSLots
            cost = (int)(cost * 1.25);
            nodePosition.Y = nodeMiddleRow;
            multiNodeArray[1, 1] = new SizeNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.25);
            nodePosition.Y = nodeBottomRow;
            multiNodeArray[1, 2] = new SpeedSwordNode(Game1.GameContent.Load<Texture2D>("speed"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.25);

            //Row 3
            nodePosition.X = nodePosition.X + widthSeperation;
            multiNodeArray[2, 0] = null;
            nodePosition.Y = nodeMiddleRow;
            multiNodeArray[2, 1] = new SwordDamageNode(Game1.GameContent.Load<Texture2D>("damage"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.25);
            multiNodeArray[2, 2] = null;
            //Row 4
            nodePosition.X = nodePosition.X + widthSeperation;
            multiNodeArray[3, 0] = null;
            nodePosition.Y = nodeMiddleRow;
            multiNodeArray[3, 1] = new UpgradeNode(Game1.GameContent.Load<Texture2D>("blank"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.25);
            multiNodeArray[3, 2] = null;
            //Row 5
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            multiNodeArray[4, 0] = new SwordDamageNode(Game1.GameContent.Load<Texture2D>("damage"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.25);
            nodePosition.Y = nodeMiddleRow;
            multiNodeArray[4, 1] = new SpeedSwordNode(Game1.GameContent.Load<Texture2D>("speed"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.25);
            nodePosition.Y = nodeBottomRow;
            multiNodeArray[4, 2] = new SwordESlotNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.25);
            //Row 6
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            cost = (int)(cost * 1.25);
            multiNodeArray[5, 0] = new SpeedSwordNode(Game1.GameContent.Load<Texture2D>("speed"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.25);
            nodePosition.Y = nodeMiddleRow;
            multiNodeArray[5, 1] = new SizeNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.25);
            nodePosition.Y = nodeBottomRow;
            multiNodeArray[5, 2] = new SwordDamageNode(Game1.GameContent.Load<Texture2D>("damage"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.25);
            //Row 7
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            multiNodeArray[6, 0] = new SwordESlotNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);//work on eSLots
            cost = (int)(cost * 1.35);
            nodePosition.Y = nodeMiddleRow;
            multiNodeArray[6, 1] = new WaveNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);
            cost = (int)((cost - (cost * .35)));
            nodePosition.Y = nodeBottomRow;
            multiNodeArray[6, 2] = new SizeNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);

            return multiNodeArray;
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

            enemy.damage(data, damage);

            if (vampirePercent > 0)
            {
                player.heal((int)(vampirePercent * damage));
            }

            enemy.damage(data, damage);

            Random rand = new Random();
            //check all the stats with a new randy every time
            double chance = rand.NextDouble();
            if (chance < poisonChance)
            {
                enemy.poison(poisonDamage, poisonDuration);
            }
            chance = rand.NextDouble();
            if (chance < burnChance)
            {
                enemy.burn(burnDamage, burnDuration);
            }
            chance = rand.NextDouble();
            if (chance < freezeChance && !enemy.Frozen)
            {
                enemy.freeze(freezeDamage, freezeDuration);
            }
            chance = rand.NextDouble();
            if (chance <= lightningChance && !enemy.Stunned)
            {
                enemy.stun(stunDuration);
            }
        }

        public void toggle(bool newValue)
        {
            dead = newValue;
            if (newValue)
            {
                hit = false;
            }
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
