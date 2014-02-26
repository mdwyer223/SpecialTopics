using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace AdlezHolder
{
    public class InventoryMenu
    {
        public enum Items
        {
            POTIONS, POTIONM, POTIONL
        }

        Rectangle menuRect;
        KeyboardState keys, oldKeys;
        int slotHeight, slotWidth, currentIndex, optionIndex, maxSlots, optionHeight, numOptions;
        Rectangle selectedSlot, iconPos, optionsRec;
        Vector2 tagPos;
        Boolean selected;
        List<Rectangle> slots;
        List<Item> items;
        int[] counts;
        List<string> options;

        public InventoryMenu(Vector2 border)
        {
            items = new List<Item>();
            menuRect = new Rectangle((int)border.X + 20, (int)border.Y, (int)(Game1.DisplayWidth - border.X), Game1.DisplayHeight);
            slots = new List<Rectangle>();

            slotHeight = (int)(menuRect.Height / 4);
            slotWidth = (int)(menuRect.Width / 3);

            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    slots.Add(new Rectangle(menuRect.X + (slotWidth * j), menuRect.Y + (slotHeight * i), slotWidth, slotHeight));
                }
            }

            currentIndex = 0;
            optionIndex = 0;
        }

        public void update()
        {
            keys = Keyboard.GetState();
            if (items.Count == 0)
                selected = false;
            if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
            {
                if (!selected)
                {
                    if ((currentIndex - 3) >= 0)
                    {
                        currentIndex -= 3;
                    }
                }
                else
                {
                    optionIndex--;
                    if (optionIndex < 0)
                    {
                        optionIndex = options.Count - 1;
                    }
                }
            }

            if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
            {
                if (!selected)
                {
                    if ((currentIndex + 3) < maxSlots)
                    {
                        currentIndex += 3;
                    }
                }
                else
                {
                    optionIndex++;
                    if (optionIndex >= options.Count)
                    {
                        optionIndex = 0;
                    }
                }
            }

            if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
            {
                if (!selected)
                {
                    if ((currentIndex - 1) >= 0)
                    {
                        currentIndex--;
                    }
                }
            }

            if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
            {
                if (!selected)
                {
                    if ((currentIndex + 1) < maxSlots)
                    {
                        currentIndex++;
                    }
                }
            }

            if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
            {
                if (currentIndex < items.Count)
                {
                    if (!selected && items.Count > 0)
                    {
                        if (items[currentIndex] != null)
                        {
                            options = items[currentIndex].getOptions();
                            optionIndex = 0;
                            selected = true;
                            options.Add("Cancel");
                        }
                    }
                    else
                    {
                        if (optionIndex == options.Count - 1)
                        {
                            selected = false;
                            items[currentIndex].getOptions().Remove("Cancel");
                        }
                        else
                        {
                            if (options.Count > 0 && items.Count > 0)
                            {
                                items[currentIndex].chooseOption(options[optionIndex], items, this.counts);
                            }
                        }
                    }
                }
                else
                {
                    selected = false;
                }
            }

            if (keys.IsKeyDown(Keys.Q) && oldKeys.IsKeyUp(Keys.Q))
            {
                selected = false;
            }

            selectedSlot = slots[currentIndex];
            oldKeys = keys;
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), slots[i], Color.White);

                if (i >= maxSlots)
                {
                    spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Weapons/lock"), slots[i], Color.White);
                }

                if(i == currentIndex)
                {
                    spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Random/LightScreen"), selectedSlot, Color.White);
                }

                if (i < items.Count && items[i] != null)
                {
                    iconPos = new Rectangle((int)((slots[i].X + (slots[i].Width / 2)) - (items[i].DrawnRec.Width * 5 / 2)), 
                        (int)(slots[i].Y + (slots[i].Height / 2)), 
                        items[i].DrawnRec.Width * 5, items[i].DrawnRec.Height * 5);
                    tagPos = new Vector2((int)(slots[i].X + (slots[i].Width / 2)) - (spriteFont.MeasureString(items[i].ItemName).X / 2), 
                        (int)(slots[i].Y + (slots[i].Height / 4)));
                    
                    spriteBatch.Draw(items[i].Image, iconPos, Color.White);
                    spriteBatch.DrawString(spriteFont, items[i].ItemName, tagPos, Color.White);
                }
                else if (i < maxSlots)
                {
                    spriteBatch.DrawString(spriteFont, "Empty", new Vector2((int)(slots[i].X + (slots[i].Width / 2)) - (spriteFont.MeasureString("Empty").X / 2),
                        (int)(slots[i].Y + (slots[i].Height / 4))), Color.White);
                }

                if (selected)
                {
                    optionHeight = (int)(spriteFont.MeasureString(options[0]).Y) + 4;
                    Vector2 vec = new Vector2(selectedSlot.X, selectedSlot.Y);
                    numOptions = options.Count;

                    optionsRec = new Rectangle((int)vec.X, (int)vec.Y, 100, optionHeight * numOptions);
                    spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), optionsRec, Color.White);

                    for(int k = 0; k < options.Count; k++)
                    {
                        if (k == optionIndex)
                        {
                            spriteBatch.DrawString(spriteFont, options[k], new Vector2(vec.X, optionsRec.Y + (optionHeight * k)), Color.Red);
                        }
                        else
                        {
                            spriteBatch.DrawString(spriteFont, options[k], new Vector2(vec.X, optionsRec.Y + (optionHeight * k)), Color.White);
                        }
                    }
                }

            }
        }

        public void updateInvent(List<Item> newList, int[] counts, int slotsAvailable)
        {
            this.counts = counts;
            items = newList;
            maxSlots = slotsAvailable;
        }
    }
}
