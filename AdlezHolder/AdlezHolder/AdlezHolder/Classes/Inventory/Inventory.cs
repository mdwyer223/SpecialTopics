using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class Inventory
    {
        List<Item> items;
        int[] counts = new int[12];
        int currentIndex;

        int arrowCount, bombCount;
        int maxSlots;

        public ItemStruct[] SaveData
        {
            get
            {//TODO: save counts
                ItemStruct[] savaData = new ItemStruct[items.Count];
                for(int i=0; i< items.Count; i++)
                    savaData[i] = items[i].SaveData;

                return savaData;
            }
            set
            {
                items = new List<Item>();
                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i].itemId == "Arr")
                    {
                        items.Add(new Arrow(value[i]));
                    }
                    else if (value[i].itemId == "Pot")
                    {
                        Potion pot = new Potion(value[i].baseStruct.position, 0, value[i].tier, value[i].count);
                        pot.SaveData = value[i];
                        items.Add(pot);
                    }
                    else if (value[i].itemId == "Rug")
                        items.Add(new RuggedLeather()); 
                }
            }
        }

        public bool Full
        {
            get { return items.Count == maxSlots; }
        }

        public int MaxSlots
        {
            get { return maxSlots; }
        }

        public List<Item> ItemList
        {
            get { return items; }
        }

        public int[] Counts
        {
            get { return counts; }
        }

        public Inventory()
        {
            items = new List<Item>();
            maxSlots = 6;
        }

        public Inventory(List<Item> oldItems, int newMaxSlots)
        {
            items = oldItems;
            this.maxSlots = newMaxSlots;
        }

        public void addItem(Item item, Character player)
        {
            bool sameObj = false;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] != null)
                {
                    if (item.ItemName.Equals(items[i].ItemName) && item.IsStackable)
                    {
                        items[i].Count++;
                        sameObj = true;
                    }
                }
            }

            if (!sameObj)
            {
                if (!Full)
                {
                    item.addPlayer(player);
                    items.Add(item);
                }
                else
                {
                    player.addMessage(new Message("Inventory is full",
                    new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
                }
            }

            /*
            if (!Full)
            {
                item.addPlayer(player);
                items.Add(item);
            }
            else
            {
                player.addMessage(new Message("Inventory is full",
                    new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
            }
             */
        }
    }
}
