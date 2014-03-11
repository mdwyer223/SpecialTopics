 using System;
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
    class SellingItemShop
    {
        Item[,] itemArray;
        Item[] tempItemArray;
        Texture2D itemImage;
        int displayHeight = Game1.DisplayHeight;
        int displayWidth = Game1.DisplayWidth;
        BaseSprite itemPicture;
        NodeMessageBox infoBox;
        Button switchToBuy;
        Item selectedItem, lastItem;
        int itemRowIndex, itemColumnIndex;
        string itemMessage, enoughCash;
        KeyboardState keys, oldKeys;
        int playersCash;
        Character tempCharacter;
        bool wasJustPurchased = false;

        public SellingItemShop(Character Character)
        {


            tempCharacter = Character;
            //tempData
            VampiricStone vampStone = new VampiricStone(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), .3f, Vector2.One, "", 50);
            Gem gem = new Gem(Game1.GameContent.Load<Texture2D>("Items/CyanStone"), .3f, Vector2.One, "", 50);
            tempCharacter.PlayerInvent.addItem(vampStone, tempCharacter);
            tempCharacter.PlayerInvent.addItem(vampStone, tempCharacter);
            tempCharacter.PlayerInvent.addItem(vampStone, tempCharacter);
            tempCharacter.PlayerInvent.addItem(gem, tempCharacter);
            tempCharacter.PlayerInvent.addItem(gem, tempCharacter);
            tempCharacter.PlayerInvent.addItem(gem, tempCharacter);
            //EndTemp

            Vector2 itemPosition;
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

            itemArray = new Item[4, 3];
            tempItemArray = new Item[18];


            tempItemArray = tempCharacter.PlayerInvent.ItemList.ToArray<Item>();


            itemPosition.Y = itemTopRow;
            itemArray[0, 0] = tempItemArray[0];
            itemPosition.Y = itemMiddleRow;
            itemArray[0, 1] = tempItemArray[1];
            itemPosition.Y = itemBottomRow;
            itemArray[0, 2] = tempItemArray[2];
            itemPosition.Y = itemTopRow;
            itemPosition.X = itemPosition.X + widthSeperation;
            itemArray[1, 0] = tempItemArray[3];
            itemPosition.Y = itemMiddleRow;
            itemArray[1, 1] = tempItemArray[4];
            itemPosition.Y = itemBottomRow;
            itemArray[1, 2] = tempItemArray[5];
            itemPosition.Y = itemTopRow;
            itemPosition.X = itemPosition.X + widthSeperation;
            itemArray[2, 0] = tempItemArray[6];
            itemPosition.Y = itemMiddleRow;
            itemArray[2, 1] = tempItemArray[7];
            itemPosition.Y = itemBottomRow;
            itemArray[2, 2] = tempItemArray[8];
            itemPosition.Y = itemTopRow;
            itemPosition.X = itemPosition.X + widthSeperation;
            itemArray[3, 0] = tempItemArray[9];
            itemPosition.Y = itemMiddleRow;
            itemArray[3, 1] = tempItemArray[10];
            itemPosition.Y = itemBottomRow;
            itemArray[3, 2] = tempItemArray[11];
               

            Vector2 itemImageVector, buttonPosition;
            keys = Keyboard.GetState();
            oldKeys = keys;
            itemColumnIndex = 0;
            itemRowIndex = 1;
            infoBox = new NodeMessageBox(1);
            itemMessage = "";
            infoBox.receiveMessage("Players Cash:  $" + playersCash + "  " + itemMessage);

            itemImageVector.X = (float)(Game1.DisplayHeight * .05);
            itemImageVector.Y = (float)(Game1.DisplayHeight * .05);
            buttonPosition.X = (float)(Game1.DisplayWidth * .75);
            buttonPosition.Y = (float)(Game1.DisplayHeight* .05);
            itemColumnIndex = 0;
            itemRowIndex = 1;

            itemArray[0, 0].Selected = false;
            playersCash = tempCharacter.Money;
            itemImage = itemArray[0, 0].getItemImage();

            itemPicture = new BaseSprite(itemImage, .2f, displayWidth, 0,itemImageVector) ;
            switchToBuy = new Button(Game1.GameContent.Load<Texture2D>("The best thing ever"), .3f, buttonPosition);
        }


        public void Update(GameTime gameTime)
        {
            bool wasJustSold = false;
            playersCash = tempCharacter.Money;
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
            {
                wasJustSold = false;
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
                        itemMessage = "\nPress Enter to Sell";
                        itemImage = itemArray[itemColumnIndex, itemRowIndex].getItemImage();


                    }

                    else
                    {
                        itemRowIndex = 1;
                        itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                        lastItem = itemArray[1, 0];
                        itemMessage = "\nPress Enter to Sell";
                        itemImage = itemArray[itemColumnIndex, itemRowIndex].getItemImage();
                    }
                }
                itemPicture.setImage(itemImage);
            }

            if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
            {
                if (itemArray[itemColumnIndex, itemRowIndex] != null)
                {
                    wasJustSold = false;
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
                        itemMessage = "\nPress Enter to Sell";
                        itemImage = itemArray[itemColumnIndex, itemRowIndex].getItemImage();
                    }
                    else
                    {
                        itemRowIndex = 1;
                        itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                        lastItem = itemArray[1, 0];
                        itemMessage = "\nPress Enter to Sell";
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
                    wasJustSold = false;
                    itemMessage = "\nPress Enter to Sell";
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
                    wasJustSold = false;
                    itemMessage = "\nPress Enter to Sell";
                    itemImage = itemArray[itemColumnIndex, itemRowIndex].getItemImage();
                    itemPicture.setImage(itemImage);
                }
            }

            if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
            {
                //switch to sell
            }


            if (lastItem != null)
            {
                lastItem.Selected = true;
            }

            if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
            {
                        if (itemArray[itemColumnIndex,itemRowIndex]!= null)
                        {
                            wasJustSold = true;
                           // need a subrtact item method ..tempCharacter.PlayerInvent.subractItem(itemArray[itemColumnIndex, itemRowIndex], tempCharacter);
                            tempCharacter.addFunds((int)(itemArray[itemColumnIndex, itemRowIndex].getCost * .75));
                        }
                        else
                        {
                            itemMessage ="\nYou Do Not Have Any Items Here to Sell!";
                        }
                

            }

            selectedItem = itemArray[itemColumnIndex, itemRowIndex];

            if (wasJustSold)
            {
                itemMessage = "\nItem Sold!!";
            }


            infoBox.deleteMessage();
            infoBox.receiveMessage("Players Cash:  $" + tempCharacter.Money +  "        " + itemArray[itemColumnIndex, itemRowIndex].getName + ": $" + itemArray[itemColumnIndex, itemRowIndex].getCost + itemMessage);
            infoBox.update();

            selectedItem = itemArray[itemColumnIndex, itemRowIndex];

            oldKeys = keys;
        }

        //private void changeItemGameState(ItemGameState newState)
        //{
        //tempItemArray = tempCharacter.PlayerInvent.ItemList.ToArray<Item>();
          //Vector2 itemPosition;
          //  float scalefactor = .3f;
          //  Rectangle overScan;
          //  int widthSeperation, heightSeparation, itemTopRow, itemMiddleRow, itemBottomRow;

          //  displayWidth = Game1.DisplayWidth;
          //  displayHeight = Game1.DisplayHeight;

          //  int marginWidth = (int)(displayWidth * .05);
          //  int marginHeight = (int)(displayHeight * .05);


          //  overScan.Width = displayWidth - marginWidth;
          //  overScan.Height = displayHeight - marginHeight;

          //  overScan.X = displayWidth + marginWidth;
          //  overScan.Y = displayHeight - marginHeight;

          //  heightSeparation = overScan.Height / 8;
          //  widthSeperation = overScan.Width / 8;

          //  itemPosition.X = widthSeperation * 4;
          //  itemPosition.Y = heightSeparation;

          //  itemBottomRow = heightSeparation * 4;
          //  itemMiddleRow = heightSeparation * 3;
          //  itemTopRow = heightSeparation * 2;
          //  tempItemArray = tempCharacter.PlayerInvent.ItemList.ToArray<Item>();


          //  itemPosition.Y = itemTopRow;
          //  itemArray[0, 0] = tempItemArray[0];
          //  itemPosition.Y = itemMiddleRow;
          //  itemArray[0, 1] = tempItemArray[1];
          //  itemPosition.Y = itemBottomRow;
          //  itemArray[0, 2] = tempItemArray[2];
          //  itemPosition.Y = itemTopRow;
          //  itemPosition.X = itemPosition.X + widthSeperation;
          //  itemArray[1, 0] = tempItemArray[3];
          //  itemPosition.Y = itemMiddleRow;
          //  itemArray[1, 1] = tempItemArray[4];
          //  itemPosition.Y = itemBottomRow;
          //  itemArray[1, 2] = tempItemArray[5];
          //  itemPosition.Y = itemTopRow;
          //  itemPosition.X = itemPosition.X + widthSeperation;
          //  itemArray[2, 0] = tempItemArray[6];
          //  itemPosition.Y = itemMiddleRow;
          //  itemArray[2, 1] = tempItemArray[7];
          //  itemPosition.Y = itemBottomRow;
          //  itemArray[2, 2] = tempItemArray[8];
          //  itemPosition.Y = itemTopRow;
          //  itemPosition.X = itemPosition.X + widthSeperation;
          //  itemArray[3, 0] = tempItemArray[9];
          //  itemPosition.Y = itemMiddleRow;
          //  itemArray[3, 1] = tempItemArray[10];
          //  itemPosition.Y = itemBottomRow;
          //  itemArray[3, 2] = tempItemArray[11];
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
            switchToBuy.Draw(spriteBatch);
        }

    }
}



    


