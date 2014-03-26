using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AdlezHolder
{
    public class UpgradeNode : BaseSprite
    {
        bool purchased, locked;
        string nodeName, changesString;
        int cost;

        
        Color FADED = new Color(50, 50, 50, 50);
        Color NORMAL = new Color(255, 255, 255, 250);
        bool selected;

        public bool Selected
        {
            get { return selected; }
            set
            {
                if (value && ImageColor != Color.OrangeRed)
                    ImageColor = NORMAL;
                else
                {
                    if (ImageColor != Color.OrangeRed)
                        ImageColor = FADED;
                }
               
                selected = value;
            }
        }

        public UpgradeNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            :base(texture,scaleFactor,Game1.DisplayWidth,5, startPosition)
        {
            DrawnRec = new Rectangle(0, 0, 20, 20);
            purchased = false;
            locked = true;
            cost = price;
            nodeName = "Node Name";
        }

        public void lockItem()
        {
            locked = true;
        }
        public void purchaseItem()
        {
            purchased = true;
            this.ImageColor = Color.OrangeRed;
        }
        public void setRec(int moveAmount)
        {
            this.position.X += moveAmount;
        }
        public void unlockItem()
        {
            locked = false;
        }
        public void setCost(int x)
        {
            cost = x;
        }
        public virtual void setChangesString(string x)
        {
            changesString = x;
        }
        public void setNodeName(string x)
        {
            nodeName = x;
        }
        public int getCost
        {
            get{return cost;}
        }
        public string getChangesString
        {
            get { return changesString; }
        }
        public bool isPurchased
        {
            get { return purchased; }
        }
        public bool isLocked
        {
            get { return locked; }
        }
        public string getName
        {
            get { return nodeName; }
        }

        public virtual void  upgradeSword(Sword x)
        {
        }
        public virtual void upgradeBomb(Bomb x)
        {
        }
        public virtual void upgradeBow(Bow x)
        {
        }
            
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        
    }
}
