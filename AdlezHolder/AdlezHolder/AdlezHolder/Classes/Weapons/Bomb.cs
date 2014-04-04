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

        int carryLimit, radius;
        bool manualDet = false;
        bool tripMine = false;
        bool smokeBomb = false;
        int enchantmentSlots = 1;


        List<Gem> gemList;
        int gemSlots;

        GemStruct lifestealStruct, freezeStruct, poisonStruct,
            lightningStruct, fireStruct;

        public new BombStruct SaveData
        {
            get
            {
                BombStruct myStruct = new BombStruct();
                myStruct.damage = Damage;

                myStruct.maxGems = MaxGemSlots;
                myStruct.gemsData = new GemStruct[MaxGemSlots];
                for (int i = 0; i < gemList.Count; i++)
                    myStruct.gemsData[i] = this.gemList[i].SaveData;

                return myStruct;
            }
            set
            {
                damage = value.damage;
                speed = (int)(value.speed + .5f);
                gemSlots = value.maxGems;
                this.gemList = new List<Gem>();

                foreach (GemStruct gem in value.gemsData)
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
    
        public int Damage
        {
            get { return damage; }
        }

        public int Delay
        {
            get { return delay / 1000; }
        }

        public int MaxGemSlots
        {
            get { return gemSlots; }
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

            UpgradeNode[,] bombTreeArray = new UpgradeNode[7, 3];
            bombTreeArray = new UpgradeNode[7, 3];
            //row1    
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[0, 1] = new BombDamageNode(Game1.GameContent.Load<Texture2D>("damage"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeTopRow;
            bombTreeArray[0, 0] = new RadiusNode(Game1.GameContent.Load<Texture2D>("range"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeBottomRow;
            bombTreeArray[0, 2] = new BombCarryNode(Game1.GameContent.Load<Texture2D>("capacity"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row2
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            bombTreeArray[1, 0] = new BombCarryNode(Game1.GameContent.Load<Texture2D>("capacity"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[1, 1] = new RadiusNode(Game1.GameContent.Load<Texture2D>("range"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeBottomRow;
            bombTreeArray[1, 2] = new BombESlotNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);


            //row3
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[2, 1] = new UpgradeNode(Game1.GameContent.Load<Texture2D>("blank"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row4
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            bombTreeArray[3, 0] = new BombESlotNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);
            cost = (int)(cost - (cost * .1));
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[3, 1] = new BombDamageNode(Game1.GameContent.Load<Texture2D>("damage"), scalefactor, nodePosition, cost);
            cost = (int)(cost - (cost * .15));
            nodePosition.Y = nodeBottomRow;
            bombTreeArray[3, 2] = new BombESlotNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row5
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            cost = (int)(cost - (cost * .01));
            bombTreeArray[4, 0] = new RadiusNode(Game1.GameContent.Load<Texture2D>("range"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[4, 1] = new BombDamageNode(Game1.GameContent.Load<Texture2D>("damage"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeBottomRow;
            bombTreeArray[4, 2] = new BombCarryNode(Game1.GameContent.Load<Texture2D>("capacity"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row6
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[5, 1] = new UpgradeNode(Game1.GameContent.Load<Texture2D>("blank"), scalefactor, nodePosition, cost);

            //row7
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            cost = (int)(cost * 1.465);
            bombTreeArray[6, 0] = new TripMineNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);
            nodePosition.Y = nodeMiddleRow;
            cost = (int)(cost - (cost * .02));
            bombTreeArray[6, 1] = new ManualDetNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.165);
            nodePosition.Y = nodeBottomRow;
            bombTreeArray[6, 2] = new SmokeBombNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);

            return bombTreeArray;
        }

        public int CarryLimit
        {
            get { return carryLimit; }
        }
        public void UpgradeDamage(double multiplier)
        {
            damage = (int)(damage * multiplier);
        }
        public void UpgradeRadius(double multiplier)
        {
            radius = (int)(radius * multiplier);
        }
        public void UpgradeSpeed(double multiplier)
        {
            speed = (int)(speed * multiplier);
        }
        public void ManualDet(bool onOff)
        {
            manualDet = onOff;
        }
        public void TripMine(bool onOff)
        {
            tripMine = onOff;
        }
        public void SmokeBomb(bool onOff)
        {
            smokeBomb = onOff;
        }
        public void increaseEnchantmentSlot(int numOfIncrease)
        {
            enchantmentSlots += numOfIncrease;
        }

        public void increaseCarryLimit(int numOfIncrease)
        {
            carryLimit += numOfIncrease;
        }


        public Bomb(float scaleFactor)
        {
            bombs = new List<BombObj>();
            pEngine = new ParticleEngine();
            gemList = new List<Gem>();
            gemSlots = 3;

            lifestealStruct = new GemStruct();
            freezeStruct = new GemStruct();
            fireStruct = new GemStruct();
            poisonStruct = new GemStruct();
            lightningStruct = new GemStruct();

            damage = 50;
            delay = 3000;
            numParticles = 45;

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

        public void blowUp(Map data, Vector2 start)
        {
            List<Particle> particles = pEngine.generateExplosion(start, damage, numParticles);
            calcStats();
            Random rand = new Random();

            foreach (Particle p in particles)
            {
                if (rand.NextDouble() < lightningStruct.chance)
                {
                    p.addGemStruct(lightningStruct);
                    p.addGemType(GemType.STUN);
                }
                if (rand.NextDouble() < poisonStruct.chance)
                {
                    p.addGemStruct(poisonStruct);
                    p.addGemType(GemType.POISON);
                }
                if (rand.NextDouble() < freezeStruct.chance)
                {
                    p.addGemStruct(freezeStruct);
                    p.addGemType(GemType.FREEZE);
                }
                if (rand.NextDouble() < fireStruct.chance)
                {
                    p.addGemStruct(fireStruct);
                    p.addGemType(GemType.FIRE);
                }
                if (rand.NextDouble() < lifestealStruct.chance)
                {
                    p.addGemStruct(lifestealStruct);
                    p.addGemType(GemType.LS);
                }
                data.CurrentData.addParticle(p);
            }

            particles = null;
        }

        public void addGem(Gem gem)
        {
            if (gemList.Count == MaxGemSlots)
                return;

            this.gemList.Add(gem);
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
