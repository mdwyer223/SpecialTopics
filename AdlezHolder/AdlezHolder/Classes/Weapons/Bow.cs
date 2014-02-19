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
        int upgradeDamageLev, upgradeRangeLev, 
            upgradeSpeedLev;
        float speed;
        bool seekArrows = false;
        bool saveArrows = false;

        Color color;
        Vector2 velocity, start;
        Texture2D texture;
        
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

        public Bow(float scaleFactor)
        {   
            color = Color.White;

            upgradeDamageLev = 1;
            upgradeRangeLev = 1;
            upgradeSpeedLev = 1;
            speed = 3;
            damage = 5;
            range = 300;
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
        public void increaseEnchantmentSlot(int numOfIncrease)
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

            UpgradeNode[,] bowTreeArray = new UpgradeNode[7, 3];
            bowTreeArray = new UpgradeNode[7, 3];
            //row1    
            nodePosition.Y = nodeMiddleRow;
            bowTreeArray[0, 1] = new BowDamageNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeTopRow;
            bowTreeArray[0, 0] = new RangeNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeBottomRow;
            bowTreeArray[0, 2] = new BowESlotNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row2
            nodePosition.X = nodePosition.X + widthSeperation;
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[1, 1] = new ArrowsShotNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);


            //row3
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[2, 1] = new BowDamageNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row4
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            bombTreeArray[3, 0] = new BowESlotNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost - (cost * .1));
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[3, 1] = new UpgradeNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost - (cost * .15));
            nodePosition.Y = nodeBottomRow;
            bombTreeArray[3, 2] = new BowDamageNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row5
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            cost = (int)(cost - (cost * .01));
            bombTreeArray[4, 0] = new RangeNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[4, 1] = new BowDamageNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            nodePosition.Y = nodeBottomRow;
            bombTreeArray[4, 2] = new ArrowsShotNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.265);
            //row6
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeMiddleRow;
            bombTreeArray[5, 1] = new RangeNode(nodeTexture, scalefactor, nodePosition, cost);

            //row7
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            cost = (int)(cost * 1.465);
            bombTreeArray[6, 0] = new ArrowsShotNode(nodeTexture, scalefactor, nodePosition, cost);
            nodePosition.Y = nodeMiddleRow;
            cost = (int)(cost - (cost * .02));
            bombTreeArray[6, 1] = new UpgradeSeekArrowNode(nodeTexture, scalefactor, nodePosition, cost);
            cost = (int)(cost * 1.165);
            nodePosition.Y = nodeBottomRow;
            bombTreeArray[6, 2] = new UpgradeSaveArrowsNode(nodeTexture, scalefactor, nodePosition, cost);

            return bombTreeArray;
        }
        public void UpgradeRange(double multiplier)
        {
            range = (int)(range * multiplier);
        }
        public void UpgradeSpeed(double multiplier)
        {
            speed = (int)(speed * multiplier);
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

        public void addArrow(Map data)
        {
            data.CurrentData.addProjectile(new Projectile(damage, range, speed, .035f, 
                velocity, start, texture));
        }

        public void changeImage(Texture2D texture)
        {
            this.texture = texture;
        }
    }
}
