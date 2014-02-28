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
            {
                ItemStruct[] savaData = new ItemStruct[MaxSlots];
                for(int i=0; i< items.Count; i++)
                    savaData[i] = items[i].SaveData;

                return savaData;
            }
            set
            {
                items = new List<Item>();
                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i].type == ItemType.ARROW)
                        items.Add(new Arrow(.02f, false, "Wooden Arrow", 10, Vector2.Zero));
                    else if (value[i].type == ItemType.CURRENCY)
                    {
                    }
                    else
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
            if (currentIndex != 0 && counts[currentIndex- 1] == 0)
            {
                while (counts[currentIndex] == 0)
                {
                    currentIndex--;
                }
            }

            bool sameObj = false;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] != null)
                {
                    if (item.ItemName.Equals(items[i].ItemName) && item.IsStackable)
                    {
                        counts[i]++;
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
                    counts[currentIndex]++;
                    currentIndex++;
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
