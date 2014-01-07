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
        int cost, money;
        string result;
        string nodeName;


        
        Color FADED = new Color(50, 50, 50, 50);
        Color NORMAL = new Color(255, 255, 255, 250);
        bool selected;

        public bool Selected
        {
            get { return selected; }
            set
            {
                if (value)
                    ImageColor = NORMAL;
                else
                    ImageColor = FADED;
                selected = value;
            }
        }

        public UpgradeNode(Texture2D texture, float scaleFactor, Vector2 startPosition)
            :base(texture,scaleFactor,Game1.DisplayWidth,5, startPosition)
        {
            DrawnRec = new Rectangle(0, 0, 20, 20);
            purchased = false;
            locked = true;
            cost = 0;
            nodeName = "";
        }

        public void purchaseItem()
        {
            if (money >= cost)
            {
                money -= cost;
                locked = true;
                purchased = true;
                result = "Purchase Made";
            }
            else
            {
                result = "Not Enough Cash!";
            }
        }
        public void lockItem()
        {
            locked = true;
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
        public int getCost
        {
            get{return cost;}
        }
        public bool isPurchased
        {
            get { return purchased; }
        }
        public bool isLocked
        {
            get { return locked; }
        }

        public void upgrade()
        {
        }
            
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        
    }
}
