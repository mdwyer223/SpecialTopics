using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Item : BaseSprite
    {
        protected int value, aliveTimer, flashTimer;
        protected bool pickUp, currency, stackable, drawing;

        protected const int MAX_TIME_ALIVE = 60000;
        protected const string DROP = "Drop", USE = "Use";

        protected string tag;

        protected List<string> options;

        int flashInterval = 70;

        string itemName, changesString;
        int cost;


        Color FADED = new Color(50, 50, 50, 50);
        Color NORMAL = new Color(255, 255, 255, 250);
        bool selected;

        public bool Selected
        {
            get { return selected; }
            set
            {
                if (value && ImageColor != Color.Black && ImageColor != Color.DarkSlateGray)
                    ImageColor = NORMAL;
                else
                {
                    if (ImageColor != Color.Black && ImageColor != Color.DarkSlateGray)
                        ImageColor = FADED;
                }
                if (value == false && ImageColor == Color.Black)
                {
                    ImageColor = Color.DarkSlateGray;
                }
                else if (value && ImageColor == Color.DarkSlateGray)
                {
                    ImageColor = Color.Black;
                }

                selected = value;
            }
        }


        public string ItemName
        {
            get { return tag; }
        }

        public virtual bool Dead
        {
            get { return aliveTimer >= MAX_TIME_ALIVE; }
        }

        public bool IsStackable
        {
            get { return stackable; }
            protected set { stackable = value; }
        }

        public bool IsCurrency
        {
            get { return currency; }
            protected set { currency = value; }
        }
        public bool IsPickUp
        {
            get {return pickUp;}
            protected set { pickUp = value;}
        }

        public void setCost(int x)
        {
            cost = x;
        }
        public virtual void setChangesString(string x)
        {
            changesString = x;
        }
        public void setitemName(string x)
        {
            itemName = x;
        }
        public int getCost
        {
            get { return cost; }
        }
        public string getChangesString
        {
            get { return changesString; }
        }
        public string getName
        {
            get { return itemName; }
        }
        public virtual string getEffectsString()
        {
            return " this is a temporary sentence for the items";
        }

        public Item(Texture2D texture, float scaleFactor, Vector2 startPosition, string tag, bool isPickUp, 
            bool isCurrency, bool isStackable, int value)
            : base(texture, scaleFactor, Game1.DisplayWidth, 0, startPosition)
        {
            this.pickUp = isPickUp;
            this.currency = isCurrency;
            this.stackable = isStackable;

            this.value = value;
            this.tag = tag;
            aliveTimer = 0;
            drawing = true;

            options = new List<string>();
            options.Add("Drop");

            DrawnRec = new Rectangle(0, 0, 35, 35);
            cost = value;
            itemName = "Item Name";

        }

        public override void Update(Map data, GameTime gameTime)
        {
            aliveTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (aliveTimer > MAX_TIME_ALIVE - (MAX_TIME_ALIVE / 10))
            {
                flashTimer++;

                if (flashTimer > flashInterval)
                {
                    drawing = !drawing;
                    flashTimer = 0;
                    flashInterval -= 9;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (drawing)
            {
                base.Draw(spriteBatch);
            }
        }

        
        //might have to return something, or pass in an object 
        //to directly modify it from the method
        public virtual List<string> getOptions()
        {
            return options;
        }

        public virtual void chooseOption(string s, List<Item> items)
        {
            //compare the string to the options chosen
            switch(s)
            {
                case "Drop":
                    {
                        this.drop(items);
                        break;
                    }
            }
        }

        public virtual Texture2D getItemImage()
        {
            return Game1.GameContent.Load <Texture2D>("Particle");
        }

        //********************************************
        //some sort of list should be passed to these methods
        //*********************************************
        public virtual void drop(List<Item> items)
        {
            for (int i = 0; i < items.Count; i++ )
            {
                if (items[i] == this)
                {
                    items.RemoveAt(i);
                    break;
                }
            }
        }


    }
}
