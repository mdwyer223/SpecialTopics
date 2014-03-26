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

        int damage, delay, carryLimit,radius;
        bool manualDet = false;
        bool tripMine = false;
        bool smokeBomb = false;
        float scaleFactor;
        int enchantmentSlots = 1;

        public Bomb(float scaleFactor)
        {
            bombs = new List<BombObj>();
            pEngine = new ParticleEngine();

            damage = 50;
            delay = 3000;

            this.scaleFactor = scaleFactor;
            texture = Game1.GameContent.Load<Texture2D>("Weapons/Bomb");
        }
        public int Damage
        {
            get { return damage; }
        }

        public int Radius
        {
            get { return radius; }
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
        public UpgradeNode[,] GetTree(int displayWidth, int displayHeight)
        {
            Vector2 nodePosition;
            Texture2D nodeTexture = Game1.GameContent.Load<Texture2D>("Particle");
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

            UpgradeNode[,] bombTreeArray = new UpgradeNode[9, 3];
            //row1    
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[0, 1] = new BombDamageNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row2
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            bombTreeArray[1, 0] = new BombCarryNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[1, 1] = new BombESlotNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeBottomRow;
            bombTreeArray[1, 2] = new BombCarryNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);


            //row3
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[2, 1] = new UpgradeNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row4
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[3, 1] = new BombDamageNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost -(cost * .15));
            //row5
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            cost = (int)(cost - (cost * .01));
            bombTreeArray[4, 0] = new BombESlotNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[4, 1] = new BombCarryNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeBottomRow;
            bombTreeArray[4, 2] = new BombESlotNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row6
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[5, 1] = new BombDamageNode(nodeTexture, scalefactor, nodePosition, cost);
            //row7
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[6, 1] = new UpgradeNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row8
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            cost = (int)(cost * 1.465);
            bombTreeArray[7, 0] = new TripMineNode(nodeTexture, scalefactor, nodePosition, cost);
            nodePosition.Y = nodeMiddleRow;
            cost = (int)(cost -(cost * .02));
            bombTreeArray[7, 1] = new ManualDetNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.165);
            nodePosition.Y = nodeBottomRow;
            bombTreeArray[7, 2] = new SmokeBombNode(nodeTexture, scalefactor, nodePosition, cost);
            //row9
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[8, 1] = new BlankNode(nodeTexture, scalefactor, nodePosition, cost);////should be particle node
            return bombTreeArray;
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
                    bomb.Draw(spriteBatch);
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

        public void addBomb(Vector2 start)
        {
            bombs.Add(new BombObj(texture, scaleFactor, 
                start, delay));
        }

        public bool isManualActivated
        {
            get { return manualDet; }
        }

        public bool isTripMineActivated
        {
            get { return tripMine; }
        }

        public bool isSmokeActivated
        {
            get { return smokeBomb; }
        }
    }
}
