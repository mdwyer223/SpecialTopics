using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Bow
    {
        int damage, range;
        int eSlots, carryLimit, arrowShots;
        float speed;
        bool seekArrows = false;
        bool saveArrows = false;


        Color color;
        Vector2 velocity, start;
        Texture2D texture;

        List<Gem> gemList;
        int gemSlots;

        GemStruct lifestealStruct, freezeStruct, poisonStruct,
            lightningStruct, fireStruct;
        public new BowStruct SaveData
        {
            get
            {
                BowStruct myStruct = new BowStruct();
                myStruct.damage = Damage;
                myStruct.speed = speed;
                myStruct.range = Range;

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
        
        public Vector2 Velocity
        {
            get{ return velocity; }
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
        public void SaveArrows(bool onOff)
        {
            saveArrows = onOff;
        }
        public void SeekArrows(bool onOff)
        {
            seekArrows = onOff;
        }
        public void UpgradeDamage(double multiplier)
        {
            damage = (int)(damage * multiplier);
        }
        public void UpgradeRange(double multiplier)
        {
            range = (int)(range * multiplier);
        }
        public void UpgradeSpeed(double multiplier)
        {
            speed = (int)(speed * multiplier);
        }
        public void IncreaseEnchantmentSlots(int numOfIncrease)
        {
            eSlots += numOfIncrease;
        }

        public void increaseCarryLimit(int numOfIncrease)
        {
            carryLimit += numOfIncrease;
        }
        public void UpgradeArrowShots(int numOfIncrease)
        {
            arrowShots += numOfIncrease;
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

            UpgradeNode[,] bowTreeArray = new UpgradeNode[7, 3];
            bowTreeArray = new UpgradeNode[7, 3];
            //row1    
            nodePosition.Y = nodeMiddleRow;
            bowTreeArray[0, 1] = new BowDamageNode(Game1.GameContent.Load<Texture2D>("damage"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeTopRow;
            bowTreeArray[0, 0] = new RangeNode(Game1.GameContent.Load<Texture2D>("range"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeBottomRow;
            bowTreeArray[0, 2] = new BowESlotNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row2
            nodePosition.X = nodePosition.X + widthSeperation;
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeMiddleRow;
            bowTreeArray[1, 1] = new ArrowsShotNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);


            //row3
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeMiddleRow;
            bowTreeArray[2, 1] = new BowDamageNode(Game1.GameContent.Load<Texture2D>("damage"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row4
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            bowTreeArray[3, 0] = new BowESlotNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);
            cost = (int)(cost - (cost * .1));
            nodePosition.Y = nodeMiddleRow;
            bowTreeArray[3, 1] = new UpgradeNode(Game1.GameContent.Load<Texture2D>("blank"), scalefactor, nodePosition, cost);
            cost = (int)(cost - (cost * .15));
            nodePosition.Y = nodeBottomRow;
            bowTreeArray[3, 2] = new BowDamageNode(Game1.GameContent.Load<Texture2D>("damage"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row5
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            cost = (int)(cost - (cost * .01));
            bowTreeArray[4, 0] = new RangeNode(Game1.GameContent.Load<Texture2D>("range"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeMiddleRow;
            bowTreeArray[4, 1] = new BowDamageNode(Game1.GameContent.Load<Texture2D>("damage"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeBottomRow;
            bowTreeArray[4, 2] = new ArrowsShotNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row6
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeMiddleRow;
            bowTreeArray[5, 1] = new RangeNode(Game1.GameContent.Load<Texture2D>("range"), scalefactor, nodePosition, cost);

            //row7
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            cost = (int)(cost * 1.465);
            bowTreeArray[6, 0] = new ArrowsShotNode(Game1.GameContent.Load<Texture2D>("range"), scalefactor, nodePosition, cost);
            nodePosition.Y = nodeMiddleRow;
            cost = (int)(cost - (cost * .02));
            bowTreeArray[6, 1] = new UpgradeSeekArrowNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.165);
            nodePosition.Y = nodeBottomRow;
            bowTreeArray[6, 2] = new UpgradeSaveArrowsNode(Game1.GameContent.Load<Texture2D>("speical"), scalefactor, nodePosition, cost);

            return bowTreeArray;
        }
        public Bow(float scaleFactor)
        {   
            color = Color.White;
            gemList = new List<Gem>();
            gemSlots = 3;

            lifestealStruct = new GemStruct();
            freezeStruct = new GemStruct();
            fireStruct = new GemStruct();
            poisonStruct = new GemStruct();
            lightningStruct = new GemStruct();

            speed = 3;
            damage = 25;
            range = 300;

            gemList.Add(new IceStone(.02f, Vector2.Zero, 1, 1));
        }
       
        public void Update(Map data, GameTime gameTime)
        {
            if (data.Player.Direction == Orientation.DOWN)
            {
                velocity = new Vector2(0, speed);
                texture = Game1.GameContent.Load<Texture2D>("Projectiles/SteelArrowDown");
                start = new Vector2(data.Player.Position.X, data.Player.Position.Y + data.Player.CollisionRec.Height + 1);
            }
            else if (data.Player.Direction == Orientation.UP)
            {
                velocity = new Vector2(0, -speed);
                texture = Game1.GameContent.Load<Texture2D>("Projectiles/SteelArrowUp");
                start = new Vector2(data.Player.Position.X, data.Player.Position.Y - 30);
            }
            else if (data.Player.Direction == Orientation.RIGHT)
            {
                velocity = new Vector2(speed, 0);
                texture = Game1.GameContent.Load<Texture2D>("Projectiles/SteelArrowRight");
                start = new Vector2(data.Player.Position.X + data.Player.CollisionRec.Width + 1, data.Player.Position.Y);
            }
            else if (data.Player.Direction == Orientation.LEFT)
            {
                velocity = new Vector2(-speed, 0);
                texture = Game1.GameContent.Load<Texture2D>("Projectiles/SteelArrowLeft");
                start = new Vector2(data.Player.Position.X - 30, data.Player.Position.Y);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
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

        public void addArrow(Map data)
        {
            calcStats();
            Random rand = new Random();
            Projectile p = new Projectile(damage, range, speed, .035f,
                    velocity, start, texture);

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

            data.CurrentData.addProjectile(p); 
        }

        public void addGem(Gem gem)
        {
            if (gemList.Count == MaxGemSlots)
                return;

            this.gemList.Add(gem);
        }

        public void changeImage(Texture2D texture)
        {
            this.texture = texture;
        }
    }
}
