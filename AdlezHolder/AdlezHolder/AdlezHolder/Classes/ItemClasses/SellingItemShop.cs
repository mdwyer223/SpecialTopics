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
        bool isNotEmpty = true;
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
        bool emptyInventory = false;

        public SellingItemShop(Character Character)
        {
            itemArray = new Item[4, 3];
            tempCharacter = Character;
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

            itemPicture = new BaseSprite(itemImage, .1f, displayWidth, 0,itemImageVector) ;
            switchToBuy = new Button(Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), .2f, buttonPosition);


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

            if (keys.IsKeyDown(Keys.Escape) && oldKeys.IsKeyUp(Keys.Escape))
            {
                this.changeItemGameState(ItemShopGameState.ITEMSHOP);
            }

            if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
            {
                if (itemColumnIndex != 3)
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
                            isNotEmpty = true;
                            if(itemArray[itemColumnIndex,itemRowIndex].getName() == "empty")
                            {
                                isNotEmpty = false;
                            }

                            wasJustSold = true;
                            tempCharacter.PlayerInvent.subtractItem(tempCharacter, positionInList,isNotEmpty);
                            tempCharacter.addFunds((int)(itemArray[itemColumnIndex, itemRowIndex].getCost() * .75));
                            this.setPlayerInvent();
                        }
                        if (itemArray.Length != 0)
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

            if (wasJustSold && isNotEmpty)
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
        

        public void setPlayerInvent()
        {

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Item emptyItem = new Item(Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), .05f, Vector2.Zero, "empty", false, false, false, 0, 1000);
                    itemArray[col, row] = emptyItem;
                }
            }

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
            widthSeperation = overScan.Width / 7;

            itemPosition.X = widthSeperation *2;
            itemPosition.Y = heightSeparation;

            itemBottomRow = heightSeparation * 4;
            itemMiddleRow = heightSeparation * 3;
            tempItemArray = tempCharacter.PlayerInvent.ItemList.ToArray<Item>();
            itemTopRow = heightSeparation * 2;
            itemPosition.Y = itemTopRow;
            int tempCount = 0;
            Vector2 tempPosition;
            tempPosition.X = widthSeperation;
            tempPosition.Y = itemTopRow;
            tempCount = 0;

            int columnTemp = 0, rowTemp = 0;
            for (int i = 0; i < tempItemArray.Length; i++)
            {
                itemArray[columnTemp, rowTemp] = tempItemArray[i];
                if (rowTemp == 2)
                {
                    rowTemp = 0;
                    columnTemp++;
                }
                else
                    rowTemp++;

            }

            for (int i = 0; i < 4; i++)
            {
                tempPosition.X = tempPosition.X + widthSeperation;
                tempCount = 0;
                for (int x = 0; x < 3; x++)
                {
                    if (itemArray.GetValue(i, x) != null)
                    {
                        if (tempCount == 0)
                            tempPosition.Y = itemTopRow;
                        if (tempCount == 1)
                            tempPosition.Y = itemMiddleRow;
                        if (tempCount == 2)
                            tempPosition.Y = itemBottomRow;

                        itemArray[i, x].Position = tempPosition;
                        tempCount = tempCount + 1;
                    }
                }
            }
        }

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
        private void changeItemGameState(ItemShopGameState newState)
        {
            ItemShopHome.CurrentItemState = newState;
        }



    }
}



    


