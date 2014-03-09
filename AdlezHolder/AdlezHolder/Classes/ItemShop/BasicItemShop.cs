﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AdlezHolder
{
    class BasicItemShop
    {
        Item[,] itemArray;
        int price;
        int inflation;
        Texture2D itemImage;
        int displayHeight = Game1.DisplayHeight;
        int displayWidth = Game1.DisplayWidth;
        BaseSprite itemPicture;
        NodeMessageBox infoBox;
        Item selectedItem, lastItem;
        int itemRowIndex, itemColumnIndex;
        string itemMessage, enoughCash;
        KeyboardState keys, oldKeys;
        int playersCash;
        Character tempCharacter;
        int moveItems;
        bool wasJustPurchased = false;

        public BasicItemShop(Character Character)
        {
            Vector2 itemImageVector;
            keys = Keyboard.GetState();
            oldKeys = keys;
            itemColumnIndex = 0;
            itemRowIndex = 1;
            infoBox = new NodeMessageBox(1);
            itemMessage = "";
            infoBox.receiveMessage("Players Cash:  $" + playersCash + "  " + itemMessage);

            itemArray = new Item[4, 3];
            this.getTownsItems(1);

            itemImageVector.X = (float)(Game1.DisplayHeight * .05);
            itemImageVector.Y = (float)(Game1.DisplayWidth * .05);
            itemColumnIndex = 0;
            itemRowIndex = 1;

            itemArray[0, 0].Selected = false;
            tempCharacter = Character;
            playersCash = tempCharacter.Money;
            itemImage = itemArray[0, 0].getItemImage();

            itemPicture = new BaseSprite(itemImage, .2f, displayWidth, 0,itemImageVector) ;
        }


        public void Update(GameTime gameTime)
        {
            bool wasJustPurchased = false;
            playersCash = tempCharacter.Money;
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
            {
                wasJustPurchased = false;
                if (itemArray[itemColumnIndex, itemRowIndex] != null)
                {
                    lastItem = itemArray[itemColumnIndex, itemRowIndex];
                    if (itemRowIndex != 2)
                    {
                        itemRowIndex = (itemRowIndex + 1) % 3;
                    }
                    else
                    {
                        itemRowIndex = 0;
                        itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                    }
                    if (itemArray[itemColumnIndex, itemRowIndex] != null)
                    {
                        itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                        itemMessage = "\nPress Enter to Purchase" + "\n" + itemArray[itemColumnIndex, itemRowIndex].getEffectsString();
                        itemImage = itemArray[itemColumnIndex, itemRowIndex].getItemImage();


                    }

                    else
                    {
                        itemRowIndex = 1;
                        itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                        lastItem = itemArray[1, 0];
                        itemMessage = "\nPress Enter to Purchase" + "\n" + itemArray[itemColumnIndex, itemRowIndex].getEffectsString();
                        itemImage = itemArray[itemColumnIndex, itemRowIndex].getItemImage();
                    }
                }
                itemPicture.setImage(itemImage);
            }

            if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
            {
                if (itemArray[itemColumnIndex, itemRowIndex] != null)
                {
                    wasJustPurchased = false;
                    lastItem = itemArray[itemColumnIndex, itemRowIndex];
                    if (itemRowIndex != 0)
                    {
                        itemRowIndex = (itemRowIndex - 1) % 3;

                    }
                    else
                    {
                        itemRowIndex = 2;
                    }
                    if (itemArray[itemColumnIndex, itemRowIndex] != null)
                    {
                        itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                        itemMessage = "\nPress Enter to Purchase" + "\n" + itemArray[itemColumnIndex, itemRowIndex].getEffectsString();
                        itemImage = itemArray[itemColumnIndex, itemRowIndex].getItemImage();
                    }
                    else
                    {
                        itemRowIndex = 1;
                        itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                        lastItem = itemArray[1, 0];
                        itemMessage = "\nPress Enter to Purchase" + "\n" + itemArray[itemColumnIndex, itemRowIndex].getEffectsString();
                        itemImage = itemArray[itemColumnIndex, itemRowIndex].getItemImage();
                    }
                }
                itemPicture.setImage(itemImage);
            }

            if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
            {
                if (itemColumnIndex != 0)
                {
                    lastItem = itemArray[itemColumnIndex, itemRowIndex];
                    itemColumnIndex--;
                    itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                    wasJustPurchased = false;
                    itemMessage = "\nPress Enter to Purchase" + "\n" + itemArray[itemColumnIndex, itemRowIndex].getEffectsString();
                    itemImage = itemArray[itemColumnIndex, itemRowIndex].getItemImage();
                    itemPicture.setImage(itemImage);
                }



            }

            if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
            {
                if (itemColumnIndex != 3)
                {
                    lastItem = itemArray[itemColumnIndex, itemRowIndex];
                    itemColumnIndex++;
                    itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                    wasJustPurchased = false;
                    itemMessage = "\nPress Enter to Purchase"   +  "\n" + itemArray[itemColumnIndex, itemRowIndex].getEffectsString();
                    itemImage = itemArray[itemColumnIndex, itemRowIndex].getItemImage();
                    itemPicture.setImage(itemImage);
                }
            }


            if (lastItem != null)
            {
                lastItem.Selected = true;
            }

            if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
            {
               
                    if (tempCharacter.Money >= itemArray[itemColumnIndex, itemRowIndex].getCost)
                    {
                        if (tempCharacter.PlayerInvent.Full != true)
                        {
                            itemMessage = "\nPurchase Complete"  + itemArray[itemColumnIndex, itemRowIndex].getChangesString;
                            wasJustPurchased = true;
                            tempCharacter.PlayerInvent.addItem(itemArray[itemColumnIndex, itemRowIndex], tempCharacter);
                            tempCharacter.subtractFunds(itemArray[itemColumnIndex, itemRowIndex].getCost);
                        }
                        else
                        {
                            itemMessage ="\nYour Inventory is Full!!! Please Sell or Use Items if You Wish to Purchase More!";
                        }


                    }
                    else
                    {
                        itemMessage = "\nYou do not have enough cash!";
                    }
                

            }

            selectedItem = itemArray[itemColumnIndex, itemRowIndex];

            if (wasJustPurchased)
            {
                itemMessage =  ":  $" + itemArray[itemColumnIndex, itemRowIndex].getCost + "\n" +  itemArray[itemColumnIndex, itemRowIndex].getChangesString;
            }


            infoBox.deleteMessage();
            infoBox.receiveMessage("Players Cash:  $" + tempCharacter.Money +  "        " + itemArray[itemColumnIndex, itemRowIndex].getName + ": $" + itemArray[itemColumnIndex, itemRowIndex].getCost + itemMessage);
            infoBox.update();

            selectedItem = itemArray[itemColumnIndex, itemRowIndex];

            oldKeys = keys;
        }

        //private void changeItemGameState(ItemGameState newState)
        //{
        //}

   
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (itemArray.GetValue(i, x) != null)
                    {
                        itemArray[i, x].Draw(spriteBatch);

                    }
                }
            }
            infoBox.draw(spriteBatch);
            itemPicture.Draw(spriteBatch);
        }

        public void getTownsItems(int x)
        {
            Vector2 itemPosition;
            float scalefactor = .3f;
            Rectangle overScan;
            int widthSeperation, heightSeparation, itemTopRow, itemMiddleRow, itemBottomRow;

            displayWidth = Game1.DisplayWidth;
            displayHeight = Game1.DisplayHeight;

            int marginWidth = (int)(displayWidth * .05);
            int marginHeight = (int)(displayHeight * .05);


            overScan.Width = displayWidth - marginWidth;
            overScan.Height = displayHeight - marginHeight;

            overScan.X = displayWidth + marginWidth;
            overScan.Y = displayHeight - marginHeight;

            heightSeparation = overScan.Height / 8;
            widthSeperation = overScan.Width / 8;

            itemPosition.X = widthSeperation * 4;
            itemPosition.Y = heightSeparation;

            itemBottomRow = heightSeparation * 4;
            itemMiddleRow = heightSeparation * 3;
            itemTopRow = heightSeparation * 2;

            if (x == 1)
            {
                int price = 5;
                string name = "blank";
                itemPosition.Y = itemTopRow;
                itemArray[0, 0] = new Item(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, name, false, false, false, price);
                itemPosition.Y = itemMiddleRow;
                itemArray[0, 1] = new Arrow(.3f,false,"Wood Arrow",100,itemPosition);
                itemPosition.Y = itemBottomRow;
                itemArray[0, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, price);
                itemPosition.Y = itemTopRow;
                itemPosition.X = itemPosition.X + widthSeperation;
                itemArray[1, 0] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, price);
                itemPosition.Y = itemMiddleRow;
                itemArray[1, 1] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, price);
                itemPosition.Y = itemBottomRow;
                itemArray[1, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, price);
                itemPosition.Y = itemTopRow;
                itemPosition.X = itemPosition.X + widthSeperation;
                itemArray[2, 0] = new Arrow(.3f, false, "Wood Arrow", price * 2, itemPosition);
                itemPosition.Y = itemMiddleRow;
                itemArray[2, 1] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, price);
                itemPosition.Y = itemBottomRow;
                itemArray[2, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, price);
                itemPosition.Y = itemTopRow;
                itemPosition.X = itemPosition.X + widthSeperation;
                itemArray[3, 0] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, price);
                itemPosition.Y = itemMiddleRow;
                itemArray[3, 1] = new Arrow(.3f, true, "Steel Arrow", price * 5, itemPosition);
                itemPosition.Y = itemBottomRow;
                itemArray[3, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, price);
               

            }

            else if (x == 2)
            {
                string name = "blank";
                itemPosition.Y = itemTopRow;
                itemArray[0, 0] = new Item(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, name, false, false, false, 50);
                itemPosition.Y = itemMiddleRow;
                itemArray[0, 1] = new Arrow(.3f, false, "Wood Arrow", 100, itemPosition);
                itemPosition.Y = itemBottomRow;
                itemArray[0, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemTopRow;
                itemPosition.X = itemPosition.X + widthSeperation;
                itemArray[1, 0] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemMiddleRow;
                itemArray[1, 1] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemBottomRow;
                itemArray[1, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemTopRow;
                itemPosition.X = itemPosition.X + widthSeperation;
                itemArray[2, 0] = new Arrow(.3f, false, "Wood Arrow", 100, itemPosition);
                itemPosition.Y = itemMiddleRow;
                itemArray[2, 1] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemBottomRow;
                itemArray[2, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemTopRow;
                itemPosition.X = itemPosition.X + widthSeperation;
                itemArray[3, 0] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemMiddleRow;
                itemArray[3, 1] = new Arrow(.3f, true, "Steel Arrow", 100, itemPosition);
                itemPosition.Y = itemBottomRow;
                itemArray[3, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
               
            }

            else if (x == 3)
            {
                string name = "blank";
                itemPosition.Y = itemTopRow;
                itemArray[0, 0] = new Item(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, name, false, false, false, 50);
                itemPosition.Y = itemMiddleRow;
                itemArray[0, 1] = new Arrow(.3f, false, "Wood Arrow", 100, itemPosition);
                itemPosition.Y = itemBottomRow;
                itemArray[0, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemTopRow;
                itemPosition.X = itemPosition.X + widthSeperation;
                itemArray[1, 0] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemMiddleRow;
                itemArray[1, 1] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemBottomRow;
                itemArray[1, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemTopRow;
                itemPosition.X = itemPosition.X + widthSeperation;
                itemArray[2, 0] = new Arrow(.3f, false, "Wood Arrow", 100, itemPosition);
                itemPosition.Y = itemMiddleRow;
                itemArray[2, 1] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemBottomRow;
                itemArray[2, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemTopRow;
                itemPosition.X = itemPosition.X + widthSeperation;
                itemArray[3, 0] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemMiddleRow;
                itemArray[3, 1] = new Arrow(.3f, true, "Steel Arrow", 100, itemPosition);
                itemPosition.Y = itemBottomRow;
                itemArray[3, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
               
            }
            else
            {
                string name = "blank";
                itemPosition.Y = itemTopRow;
                itemArray[0, 0] = new Item(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, name, false, false, false, 50);
                itemPosition.Y = itemMiddleRow;
                itemArray[0, 1] = new Arrow(.3f, false, "Wood Arrow", 100, itemPosition);
                itemPosition.Y = itemBottomRow;
                itemArray[0, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemTopRow;
                itemPosition.X = itemPosition.X + widthSeperation;
                itemArray[1, 0] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemMiddleRow;
                itemArray[1, 1] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemBottomRow;
                itemArray[1, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemTopRow;
                itemPosition.X = itemPosition.X + widthSeperation;
                itemArray[2, 0] = new Arrow(.3f, false, "Wood Arrow", 100, itemPosition);
                itemPosition.Y = itemMiddleRow;
                itemArray[2, 1] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemBottomRow;
                itemArray[2, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemTopRow;
                itemPosition.X = itemPosition.X + widthSeperation;
                itemArray[3, 0] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
                itemPosition.Y = itemMiddleRow;
                itemArray[3, 1] = new Arrow(.3f, true, "Steel Arrow", 100, itemPosition);
                itemPosition.Y = itemBottomRow;
                itemArray[3, 2] = new IceStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), scalefactor, itemPosition, "Ice Stone", false, false, false, 50);
               
            }
        }
    }
}



    

