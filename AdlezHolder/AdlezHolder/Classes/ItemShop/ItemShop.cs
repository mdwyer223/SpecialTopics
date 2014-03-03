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

namespace AdlezHolder.Classes.ItemShop
{
    class ItemShop
    {
        Item[,] itemArray;
        int price;
        int inflation;
        int displayHeight = Game1.DisplayHeight;
        int displayWidth = Game1.DisplayWidth;
        BaseSprite itemPicture;
        NodeMessageBox infoBox;
        Item selectedItem, lastItem;
        int itemRowIndex, itemColumnIndex;
        string itemMessage, lockedPurchasedMessage;
        KeyboardState keys, oldKeys;
        int playersCash;
        Character tempCharacter;
        int moveItems;
        bool wasJustPurchased = false;

        public ItemShop(Character Character)
        {
            keys = Keyboard.GetState();
            oldKeys = keys;
            itemColumnIndex = 0;
            itemRowIndex = 1;
            infoBox = new NodeMessageBox(1);
            itemMessage = "";
            //infoBox.receiveMessage("Players Cash:  $" + playersCash + "  " + itemMessage);
            lockedPurchasedMessage = "\nPress Enter to Purchase";

            int widthSeperation;
            widthSeperation = Game1.DisplayWidth / 20;
            Vector2 tempVector;
            tempVector.X = widthSeperation / 2;
            tempVector.Y = ((int)(displayHeight * .05));

            itemColumnIndex = 0;
            itemRowIndex =1;
        
            //itemArray[0, 1].Selected = false;
            //itemArray[0, 1].unlockItem();
            tempCharacter = Character;
            playersCash = tempCharacter.Money;

        }


        public void Update(GameTime gameTime)
        {
            bool wasJustPurchased;
            moveItems = 0;
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
                       // itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                    }
                    if (itemArray[itemColumnIndex, itemRowIndex] != null)
                    {
                       // itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                       // lockedPurchasedMessage = "\nPress Enter to Purchase" + itemArray[itemColumnIndex, itemRowIndex].getEffectsString();
                    }
                    else
                    {
                        itemRowIndex = 1;
                        //itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                        lastItem = itemArray[1, 0];
                       // lockedPurchasedMessage = "\nPress Enter to Purchase" + itemArray[itemColumnIndex, itemRowIndex].getEffectsString();
                    }
                }
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
                        //itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                        //lockedPurchasedMessage = "\nPress Enter to Purchase" + itemArray[itemColumnIndex, itemRowIndex].getEffectsString();
                    }
                    else
                    {
                        itemRowIndex = 1;
                       // itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                        lastItem = itemArray[1, 0];
                       // lockedPurchasedMessage = "\nPress Enter to Purchase" + itemArray[itemColumnIndex, itemRowIndex].getEffectsString();
                    }
                }
            }

            if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
            {
                if (itemColumnIndex != 0)
                {
                    lastItem = itemArray[itemColumnIndex, itemRowIndex];
                    itemColumnIndex--;
                    itemRowIndex = 1;
                    //ItemArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    moveItems = 75;
                    wasJustPurchased = false;
                    //lockedPurchasedMessage = "\nPress Enter to Purchase" + itemArray[itemColumnIndex, itemRowIndex].getEffectsString();
                }



            }

            if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
            {
                if (itemColumnIndex != 6)
                {
                    lastItem = itemArray[itemColumnIndex, itemRowIndex];
                    itemColumnIndex++;
                    itemRowIndex = 1;
                    //itemArray[itemColumnIndex, itemRowIndex].Selected = false;
                    moveItems = -75;
                    wasJustPurchased = false;
                    //lockedPurchasedMessage = "\nPress Enter to Purchase" + itemArray[itemColumnIndex, itemRowIndex].getEffectsString();
                }
            }


            if (lastItem != null)
            {
                lastItem.Selected = true;
            }

            if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
            {
                if (bowTreeArray[NodeColumnIndex, NodeRowIndex].isLocked != true && bowTreeArray[NodeColumnIndex, NodeRowIndex].isPurchased != true)
                {
                    if (tempCharacter.Money >= bowTreeArray[NodeColumnIndex, NodeRowIndex].getCost)
                    {
                        lockedPurchasedMessage = "\nPurchase Complete";
                        wasJustPurchased = true;
                        bowTreeArray[NodeColumnIndex, NodeRowIndex].upgradeBow(tempBow);
                        bowTreeArray[NodeColumnIndex, NodeRowIndex].purchaseItem();
                        tempCharacter.subtractFunds(bowTreeArray[NodeColumnIndex, NodeRowIndex].getCost);

                        if (NodeColumnIndex > 0 && bowTreeArray[NodeColumnIndex - 1, NodeRowIndex] != null)
                            bowTreeArray[NodeColumnIndex - 1, NodeRowIndex].unlockItem();
                        if (NodeColumnIndex < 6 && bowTreeArray[NodeColumnIndex + 1, NodeRowIndex] != null)
                            bowTreeArray[NodeColumnIndex + 1, NodeRowIndex].unlockItem();
                        if (NodeRowIndex < 2 && bowTreeArray[NodeColumnIndex, NodeRowIndex + 1] != null)
                            bowTreeArray[NodeColumnIndex, NodeRowIndex + 1].unlockItem();
                        if (NodeRowIndex > 0 && bowTreeArray[NodeColumnIndex, NodeRowIndex - 1] != null)
                            bowTreeArray[NodeColumnIndex, NodeRowIndex - 1].unlockItem();
                    }

                    else
                    {
                        lockedPurchasedMessage = "\nYou do not have enough cash!";
                    }
                }

            }
            else if (bowTreeArray[NodeColumnIndex, NodeRowIndex].isLocked == true)
            {
                lockedPurchasedMessage = "\nThis Node Is Locked \nYou Cannot Purchase This Node at This Time";
            }
            else if (bowTreeArray[NodeColumnIndex, NodeRowIndex].isPurchased == true && wasJustPurchased != true)
            {
                lockedPurchasedMessage = "\nThis Node Has Already Been Purchased";
            }


            selectedItem = bowTreeArray[NodeColumnIndex, NodeRowIndex];

            for (int i = 0; i < 7; i++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (bowTreeArray.GetValue(i, x) != null)
                    {
                        bowTreeArray[i, x].setRec(moveItems);

                    }
                }
            }

            if (wasJustPurchased)
            {
                itemMessage = bowTreeArray[NodeColumnIndex, NodeRowIndex].getName + ":  $" + bowTreeArray[NodeColumnIndex, NodeRowIndex].getCost + lockedPurchasedMessage + bowTreeArray[NodeColumnIndex, NodeRowIndex].getChangesString;
            }
            else
            {
                itemMessage = bowTreeArray[NodeColumnIndex, NodeRowIndex].getName + ":  $" + bowTreeArray[NodeColumnIndex, NodeRowIndex].getCost + lockedPurchasedMessage;
            }

            infoBox.deleteMessage();
            infoBox.receiveMessage("Players Cash:  $" + tempCharacter.Money);
            infoBox.update();

            selectedItem = itemTreeArray[NodeColumnIndex, NodeRowIndex];


            for (int i = 0; i < 7; i++)
            {
                for (int x = 0; x < 3; x++)
                {
                     if (itemTreeArray.GetValue(i, x) != null)
                    {
                        itemTreeArray[i, x].setRec(moveItems);

                    }
                }
            }
            oldKeys = keys;

        }

        private void changeItemGameState(ItemGameState newState)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (itemArray.GetValue(i, x) != null)
                    {
                        itemArray[i, x].Draw(spriteBatch);

                    }
                }
            }
        }


    }
}
