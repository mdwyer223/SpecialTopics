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

        protected int value, aliveTimer, flashTimer;
        protected bool pickUp, currency, stackable, drawing;

        protected const int MAX_TIME_ALIVE = 60000;
        protected const string DROP = "Drop", USE = "Use";

        protected string tag;

        protected List<string> options;

        int flashInterval = 70;

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

        protected Item()
        {
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

        public virtual void chooseOption(string s, List<Item> items, int[] counts)
        {
            //compare the string to the options chosen
            switch(s)
            {
                case "Drop":
                    {
                        this.drop(items, counts);
                        break;
                    }
            }
        }

        //********************************************
        //some sort of list should be passed to these methods
        //*********************************************
        public virtual void drop(List<Item> items, int[] counts)
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

        public virtual void addPlayer(Character player)
        {
            playerTemp = player;
        }
    }
}
