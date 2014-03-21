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
        protected Character playerTemp;

        protected int tier;
        protected int value, aliveTimer, flashTimer, count = 1;
        protected bool pickUp, currency, stackable, drawing;

        protected const int MAX_TIME_ALIVE = 60000;
        protected const string DROP = "Drop", USE = "Use";

        protected string tag;

        protected List<string> options;

        int flashInterval = 70;

        public new ItemStruct SaveData
        {
            get
            {
                ItemStruct itemData = new ItemStruct();
                itemData.baseStruct = base.SaveData;

                //if (this.GetType() == typeof(Arrow))
                //    itemData.itemId = "Arr";
                //else if (this.GetType() == typeof(Money))
                //    itemData.itemId = "Mon";
                //else if (this.GetType() == typeof(Potion))
                //    itemData.itemId = "Pot";
                //else if(this.GetType() == typeof(RuggedLeather))
                //    itemData.itemId = "Rug"; 

                itemData.price = this.value;
                itemData.isStackable = this.IsStackable;
                itemData.tier = this.tier;
                itemData.itemName = this.tag;
                itemData.count = count;
                return itemData;
            }
            set
            {
                base.SaveData = value.baseStruct;
                this.tier = value.tier;
                this.IsStackable = value.isStackable;
                this.value = value.price;
                this.tag = value.itemName;
                this.count = value.count;
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

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public virtual void load(ItemStruct itemStruct)
        {
            this.SaveData = itemStruct;
        }

        public Item(ItemStruct itemStruct)
            : base(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"),0,0,0,Vector2.Zero)
        {
            options = new List<string>();
            options.Add("Drop");

            this.load(itemStruct);
        }

        public Item(Texture2D texture, float scaleFactor, Vector2 startPosition, string tag, bool isPickUp, 
            bool isCurrency, bool isStackable, int value, int numberOf)
            : base(texture, scaleFactor, Game1.DisplayWidth, 0, startPosition)
        {
            this.pickUp = isPickUp;
            this.currency = isCurrency;
            this.stackable = isStackable;

            this.value = value;
            this.tag = tag;
            aliveTimer = 0;
            this.count = numberOf;
            drawing = true;

            options = new List<string>();
            options.Add("Drop");
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

        //********************************************
        //some sort of list should be passed to these methods
        //*********************************************
        public virtual void drop(List<Item> items)
        {
            for (int i = 0; i < items.Count; i++ )
            {
                if (items[i] == this)
                {
                    if (this.count <= 1)
                    {
                        items.RemoveAt(i);
                        this.count--;
                    }
                    else
                    {
                        this.count--;
                    }
                    break;
                }
            }
        }

        public virtual void addPlayer(Character player)
        {
            playerTemp = player;
        }
    }
}
