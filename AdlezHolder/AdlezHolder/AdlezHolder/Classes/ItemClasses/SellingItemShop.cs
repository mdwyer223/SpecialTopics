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
            itemArray = new Item[5, 5];
            tempCharacter = Character;
            //tempData
            VampiricStone vampStone = new VampiricStone(.05f,Vector2.One,3,4);
            Arrow arrowS = new Arrow(.05f, true, "", 100, Vector2.One,5); ;
            Arrow arrow = new Arrow(.05f,false,"",30,Vector2.One,10); 
            RuggedLeather leather = new RuggedLeather();
            FireStone gem = new FireStone(.05f,Vector2.One,1,2);
            LightningStone lightStone = new LightningStone(.05f, Vector2.One, 1, 2);
            PoisonStone pStone = new PoisonStone(.05f, Vector2.One, 1, 2);
            tempCharacter.PlayerInvent.addItem(vampStone, tempCharacter);
            tempCharacter.PlayerInvent.addItem(lightStone, tempCharacter);
            tempCharacter.PlayerInvent.addItem(arrow, tempCharacter);
            tempCharacter.PlayerInvent.addItem(arrowS, tempCharacter);
            tempCharacter.PlayerInvent.addItem(vampStone, tempCharacter);
            tempCharacter.PlayerInvent.addItem(lightStone, tempCharacter);
            tempCharacter.PlayerInvent.addItem(pStone, tempCharacter);
            //EndTemp
            this.setPlayerInvent();
               
            Vector2 itemImageVector, buttonPosition;
            keys = Keyboard.GetState();
            oldKeys = keys;
            itemColumnIndex = 0;
            itemRowIndex =0;
            infoBox = new NodeMessageBox(1);
            itemMessage = "";
            infoBox.receiveMessage("Players Cash:  $" + playersCash + "  " + itemMessage);

            itemImageVector.X = (float)(Game1.DisplayHeight * .05);
            itemImageVector.Y = (float)(Game1.DisplayHeight * .05);
            buttonPosition.X = (float)(Game1.DisplayWidth * .75);
            buttonPosition.Y = (float)(Game1.DisplayHeight* .05);
            itemColumnIndex = 0;
            itemRowIndex = 0;
            tempItemArray[0].Selected = false;
            playersCash = tempCharacter.Money;
            itemImage = tempItemArray[0].getItemImage();

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
                        if (itemArray[itemColumnIndex, itemRowIndex] != null)
                        {
                            itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                            lastItem = itemArray[1, 0];
                            itemMessage = "\nPress Enter to Sell";
                            itemImage = itemArray[itemColumnIndex, itemRowIndex].getItemImage();
                        }
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
                        if (itemArray[itemColumnIndex, itemRowIndex] != null)
                        {
                            itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                            lastItem = itemArray[1, 0];
                            itemMessage = "\nPress Enter to Sell";
                            itemImage = itemArray[itemColumnIndex, itemRowIndex].getItemImage();
                        }
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
                        if (itemRowIndex == 2 && itemArray[itemColumnIndex,itemRowIndex -1] != null )
                        {
                            itemRowIndex -= 1;
                        }
                        else if (itemRowIndex == 0 && itemArray[itemColumnIndex, itemRowIndex + 1] != null)
                        {
                            itemRowIndex += 1;
                        }

                   if (itemArray[itemColumnIndex, itemRowIndex] != null)
                    {
                    itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                    wasJustSold = false;
                    itemMessage = "\nPress Enter to Sell";
                    itemImage = itemArray[itemColumnIndex, itemRowIndex].getItemImage();
                    itemPicture.setImage(itemImage);
                     }
                        else
                            itemColumnIndex--;
                }



            }

            if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
            {
                if (itemColumnIndex != (tempItemArray.Length + 1) % 3)
                {
                    lastItem = itemArray[itemColumnIndex, itemRowIndex];
                    itemColumnIndex++;
                        if (itemRowIndex == 2 && itemArray[itemColumnIndex, itemRowIndex - 1] != null)
                        {
                            itemRowIndex -= 1;
                        }
                        else if (itemRowIndex == 0 && itemArray[itemColumnIndex, itemRowIndex + 1] != null)
                        {
                            itemRowIndex += 1;
                        }
                        if (itemArray[itemColumnIndex, itemRowIndex] != null)
                        {
                            itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                            wasJustSold = false;
                            itemMessage = "\nPress Enter to Sell";
                            itemImage = itemArray[itemColumnIndex, itemRowIndex].getItemImage();
                            itemPicture.setImage(itemImage);
                        }
                        else
                            itemColumnIndex--;
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
                            int positionInList;
                            if (itemColumnIndex == 0)
                            {
                                positionInList = itemRowIndex;
                            }
                            else
                            {
                                positionInList = itemRowIndex + (itemColumnIndex * 3);
                            }
                            wasJustSold = true;
                            tempCharacter.PlayerInvent.subtractItem(tempCharacter, positionInList);
                            tempCharacter.addFunds((int)(itemArray[itemColumnIndex, itemRowIndex].getCost() * .75));
                            this.setPlayerInvent();
                        }
                        if (itemArray[0, 0] != null)
                        {
                            itemColumnIndex = 0;
                            itemRowIndex = 0;
                            itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                        }
                        else
                        {
                            itemMessage = "\nYou Do Not Have Any Items Here to Sell!";
                        }
                

            }

            selectedItem = itemArray[itemColumnIndex, itemRowIndex];

            if (wasJustSold)
            {
                itemMessage = "\nItem Sold!!";
            }

            if (itemArray[itemColumnIndex, itemRowIndex] != null)
            {
                infoBox.deleteMessage();
                infoBox.receiveMessage("Players Cash:  $" + tempCharacter.Money + "        " + itemArray[itemColumnIndex, itemRowIndex].getName() + ": $" + (int)(itemArray[itemColumnIndex, itemRowIndex].getCost() * .75) + itemMessage);
                infoBox.update();

                selectedItem = itemArray[itemColumnIndex, itemRowIndex];
            }
            else
                infoBox.receiveMessage("You have no more items to sell!");

            oldKeys = keys;
        }

        //private void changeItemGameState(ItemGameState newState)
        //{
        //}

        public void setPlayerInvent()
        {

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

            itemPosition.X = widthSeperation * 3;
            itemPosition.Y = heightSeparation;

            itemBottomRow = heightSeparation * 4;
            itemMiddleRow = heightSeparation * 3;
            itemTopRow = heightSeparation * 2;


            tempItemArray = tempCharacter.PlayerInvent.ItemList.ToArray<Item>();

            int count = 0;
            for (int i = 0; i < tempItemArray.Length; i++)
            {
                if (count == 0)
                {
                    itemPosition.Y = itemTopRow;
                }
                else if (count == 1)
                {
                    itemPosition.Y = itemMiddleRow;
                }
                else if (count == 2)
                {
                    itemPosition.Y = itemBottomRow;
                }
                

                if (tempItemArray[i] != null)
                {
                    tempItemArray[i].Position = itemPosition;
                    if (count != 2)
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                        itemPosition = itemPosition;
                        itemPosition.X += widthSeperation;
                    }
                }
            }

            int row = 0, column = 0;

            for (int i = 0; i < tempItemArray.Length; i++)
            {
                itemArray[column, row] = tempItemArray[i];
                row++;
                if ((i + 1) % 3 == 0 && i != 0)
                {
                    column++;
                    row = 0;
                }
            }
        }
   
        public void Draw(SpriteBatch spriteBatch)
        {
            int countCol = 0,countRow = 0;
            for (int i = 0; i < tempItemArray.Length; i++)
            {
                if (tempItemArray[i] != null)
                {
                    if (countRow > 2)
                    {
                        countCol++;
                        countRow = 0;
                    }
                    itemArray[countCol, countRow].Position = itemArray[countCol, countRow].Position;
                    itemArray[countCol,countRow].Draw(spriteBatch);
                    countRow++;

                }
            }
            infoBox.draw(spriteBatch);
            itemPicture.Draw(spriteBatch);
            switchToBuy.Draw(spriteBatch);
        }

    }
}



    


