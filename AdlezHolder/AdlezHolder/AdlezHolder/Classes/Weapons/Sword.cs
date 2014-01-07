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
        float speed;
        int damage, range, enchantmentSlots,size;
        int upgradeDamageLev, upgradeRangeLev, upgradeSpeedLev;
        bool dead = true;
        bool wave = false;
        UpgradeNode[,] multiNodeArray;

        Color color;
        Texture2D texture;
        Rectangle collisionRec;
        
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
        public int Size
        {
            get { return Size; }
        }
        public bool isDead
        {
            get { return dead; }
        }

        public bool isWaveActivated
        {
            get { return wave; }
        }

        public Rectangle CollisionRec
        {
            get { return collisionRec; }
        }

        public void increaseEnchantmentSlot(int numOfIncrease)
        {
            enchantmentSlots += numOfIncrease;
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
            damage = 5;
            range = 5;
            speed = 5;

            
        }

        public void UpgradeDamage(double multiplier)
        {
                damage =(int)(damage * multiplier);       
        }

        public void UpgradeWave(bool onOff)
        {
                wave = onOff;       
        }
        public void UpgradeSize(double multiplier)
        {
                size =(int)(size * multiplier);       
        }
        public void UpgradeRange(double multiplier)
        {
            range = (int)(range * multiplier);
        }

        public void UpgradeSpeed(double multiplier)
        {
                speed = (int)(speed * multiplier);
        }

        public void GetTree()
        {
            Texture2D nodeTexture = Game1.GameContent.Load<Texture2D>("Particle");
            float scalefactor = .3f;

            int displayWidth, displayHeight;
            int widthSeperation, heightSeparation, nodeTopRow, nodeMiddleRow, nodeBottomRow;
            int marginWidth = (int)(displayWidth * .05);
            int marginHeight = (int)(displayHeight * .05);
            Vector2 nodePosition;

            nodePosition.X = widthSeperation;
            nodePosition.Y = heightSeparation;

            overScan.Width = displayWidth - marginWidth;
            overScan.Height = displayHeight - marginHeight;

            overScan.X = displayWidth + marginWidth;
            overScan.Y = displayHeight - marginHeight;

            heightSeparation = overScan.Height / 6;
            widthSeperation = overScan.Width / 4;

            multiNodeArray = new UpgradeNode[7, 3];
            //Row1
            multiNodeArray[0, 0] = null;
            multiNodeArray[0, 1] = new SwordDamageNode(nodeTexture, scalefactor, nodePosition);
            multiNodeArray[0, 2] = null;
            //Row 2
            multiNodeArray[1, 0] = new SwordESlotNode(nodeTexture, scalefactor, nodePosition);//work on eSLots
            multiNodeArray[1, 1] = new SizeNode(nodeTexture, scalefactor, nodePosition);
            multiNodeArray[1, 2] = new SpeedSwordNode(nodeTexture, scalefactor, nodePosition);
            //Row 3
            multiNodeArray[2, 0] = null;
            multiNodeArray[2, 1] = new SwordDamageNode(nodeTexture, scalefactor, nodePosition);
            multiNodeArray[2, 2] = null;
            //Row 4
            multiNodeArray[3, 0] = null;
            multiNodeArray[3, 1] = new UpgradeNode(nodeTexture, scalefactor, nodePosition);
            multiNodeArray[3, 2] = null;
            //Row 5
            multiNodeArray[4, 0] = new SwordDamageNode(nodeTexture, scalefactor, nodePosition);
            multiNodeArray[4, 1] = new SpeedSwordNode(nodeTexture, scalefactor, nodePosition);
            multiNodeArray[4, 2] = new SwordESlotNode(nodeTexture, scalefactor, nodePosition);
            //Row 6
            multiNodeArray[5, 0] = new SpeedSwordNode(nodeTexture, scalefactor, nodePosition);
            multiNodeArray[5, 1] = new SizeNode(nodeTexture, scalefactor, nodePosition);
            multiNodeArray[5, 2] = new SwordDamageNode(nodeTexture, scalefactor, nodePosition);
            //Row 7
            multiNodeArray[6, 0] = new SwordESlotNode(nodeTexture, scalefactor, nodePosition);//work on eSLots
            multiNodeArray[6, 1] = new WaveNode(nodeTexture, scalefactor, nodePosition);
            multiNodeArray[6, 2] = new SizeNode(nodeTexture, scalefactor, nodePosition);
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
                        enemy.damage(data, (int)damage);
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

        public void toggle(bool newValue)
        {
            dead = newValue;
        }

        public string[] getStatBoxes()
        {
            string[] finishedArray;
            finishedArray = new string[3];
            finishedArray[0] = "Damage  " + damage;
            finishedArray[0] = "Speed  " + speed;
            finishedArray[0] = "Range  " + range;
            return finishedArray;
        }

    }
}
