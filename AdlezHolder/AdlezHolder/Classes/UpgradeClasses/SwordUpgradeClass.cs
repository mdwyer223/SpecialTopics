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
    class SwordUpgradeClass
    {
        KeyboardState keys, oldKeys;
        string nodeMessage, lockedPurchasedMessage;
        NodeMessageBox infoBox;
        Texture2D swordImage, bombImage, bowImage;
        UpgradeNode selectedNode, lastNode;
        UpgradeNode[,] swordTreeArray;
        int NodeColumnIndex, NodeRowIndex;
        float scalefactor = .3f;
        int moveNodes = 50, playersCash;
        Character tempCharacter;
        int displayWidth = Game1.DisplayWidth;
        int displayHeight = Game1.DisplayHeight;
        Sword tempSword;
        BaseSprite swordPicture, bombPicture, bowPicture;
        bool wasJustPurchased = false;

        public SwordUpgradeClass(Character Character)
        {
            keys = Keyboard.GetState();
            oldKeys = keys;
            int widthSeperation;
            widthSeperation = Game1.DisplayWidth / 20;
            tempCharacter = Character;
            NodeColumnIndex = 0;
            NodeRowIndex = 0;
            Vector2 tempVector;
            tempVector.X = widthSeperation;
            tempVector.Y = ((int)(displayHeight * .05));
            tempSword = new Sword(.3f);
            swordTreeArray = tempSword.GetTree(Game1.DisplayWidth, Game1.DisplayHeight);
            nodeMessage = "";
            infoBox = new NodeMessageBox(1);
            nodeMessage = swordTreeArray[0, 1].getName + ":  $" + swordTreeArray[0, 1].getCost;
            infoBox.receiveMessage("Players Cash:  $" + playersCash + "  " + nodeMessage);
            tempSword = tempCharacter.Sword;
            lockedPurchasedMessage = "\nPress Enter to Purchase";
            swordImage = Game1.GameContent.Load<Texture2D>("sword selected");
            bombImage = Game1.GameContent.Load<Texture2D>("bomb selected");
            bowImage = Game1.GameContent.Load<Texture2D>("bow selected");

            bombPicture = new BaseSprite(bombImage, .05f, displayWidth, 0,tempVector);
            tempVector.X =  tempVector.X + (widthSeperation / 8);
            swordPicture = new BaseSprite(swordImage, .15f, displayWidth, 0,tempVector );
            tempVector.X = tempVector.X + (widthSeperation * 3);
            bowPicture = new BaseSprite(bowImage, .05f, displayWidth, 0, tempVector);
            
            tempCharacter = Character;
            playersCash = tempCharacter.Money;
            swordTreeArray = tempCharacter.Sword.GetTree(displayWidth, displayHeight);
            swordTreeArray[0, 1].unlockItem();
        }


        public void Update(GameTime gameTime)
        {
            moveNodes = 0;
            playersCash = tempCharacter.Money;
            keys = Keyboard.GetState();
            

            if (keys.IsKeyDown(Keys.Q) && oldKeys.IsKeyUp(Keys.Q))
            {
                changeTreeGameState(TreeGameState.BOWTREE);
                return;
            }
            if (keys.IsKeyDown(Keys.E) && oldKeys.IsKeyUp(Keys.E))
            {
                changeTreeGameState(TreeGameState.BOMBTREE);
                return;
            }

            if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
            {
                wasJustPurchased = false;
                lockedPurchasedMessage = "\nPress Enter to Purchase";
                if (swordTreeArray[NodeColumnIndex, NodeRowIndex] != null)
                {
                    lastNode = swordTreeArray[NodeColumnIndex, NodeRowIndex];
                    if (NodeRowIndex != 2)
                    {
                        NodeRowIndex = (NodeRowIndex + 1) % 3;
                    }
                    else
                    {
                        NodeRowIndex = 0;
                        swordTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    }
                    if (swordTreeArray[NodeColumnIndex, NodeRowIndex] != null)
                    {
                        swordTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    }
                    else
                    {
                        NodeRowIndex = 1;
                        swordTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                        lastNode = swordTreeArray[1, 0];
                    }

                }
            }

            if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
            {
                wasJustPurchased = false;
                lockedPurchasedMessage = "\nPress Enter to Purchase";
                if (swordTreeArray[NodeColumnIndex, NodeRowIndex] != null)
                {
                    lastNode = swordTreeArray[NodeColumnIndex, NodeRowIndex];
                    if (NodeRowIndex != 0)
                    {
                        NodeRowIndex = (NodeRowIndex - 1) % 3;
                    }
                    else
                    {
                        NodeRowIndex = 2;
                    }
                    if (swordTreeArray[NodeColumnIndex, NodeRowIndex] != null)
                    {
                        swordTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    }
                    else
                    {
                        NodeRowIndex = 1;
                        swordTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                        lastNode = swordTreeArray[1, 0];
                    }
                }
            }

            if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
            {
                lockedPurchasedMessage = "\nPress Enter to Purchase";
                if (NodeColumnIndex != 0)
                {
                    wasJustPurchased = false;
                    lastNode = swordTreeArray[NodeColumnIndex, NodeRowIndex];
                    NodeColumnIndex--;
                    NodeRowIndex = 1;
                    swordTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    moveNodes = 150;
                }



            }

            if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
            {
                lockedPurchasedMessage = "\nPress Enter to Purchase";
                if (NodeColumnIndex != 6)
                {
                    wasJustPurchased = false;
                    lastNode = swordTreeArray[NodeColumnIndex, NodeRowIndex];
                    NodeColumnIndex++;
                    NodeRowIndex = 1;
                    swordTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    moveNodes = -150;
                }
            }            

            if (swordTreeArray[NodeColumnIndex, NodeRowIndex] != null)
            {
                if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
                {
                    if (swordTreeArray[NodeColumnIndex, NodeRowIndex].isLocked != true && swordTreeArray[NodeColumnIndex, NodeRowIndex].isPurchased != true)
                    {

                        if (playersCash >= swordTreeArray[NodeColumnIndex, NodeRowIndex].getCost)
                        {
                            lockedPurchasedMessage = "\nPurchase Complete!!!!";
                            swordTreeArray[NodeColumnIndex, NodeRowIndex].upgradeSword(tempSword);
                            swordTreeArray[NodeColumnIndex, NodeRowIndex].purchaseItem();
                            tempCharacter.subtractFunds(swordTreeArray[NodeColumnIndex, NodeRowIndex].getCost);
                            playersCash = tempCharacter.Money;
                            wasJustPurchased = true;

                            if (NodeColumnIndex > 0 && swordTreeArray[NodeColumnIndex - 1, NodeRowIndex] != null)
                                swordTreeArray[NodeColumnIndex - 1, NodeRowIndex].unlockItem();
                            if (NodeColumnIndex < 6 && swordTreeArray[NodeColumnIndex + 1, NodeRowIndex] != null)
                                swordTreeArray[NodeColumnIndex + 1, NodeRowIndex].unlockItem();
                            if (NodeRowIndex < 2 && swordTreeArray[NodeColumnIndex, NodeRowIndex + 1] != null)
                                swordTreeArray[NodeColumnIndex, NodeRowIndex + 1].unlockItem();
                            if (NodeRowIndex > 0 && swordTreeArray[NodeColumnIndex, NodeRowIndex - 1] != null)
                                swordTreeArray[NodeColumnIndex, NodeRowIndex - 1].unlockItem();


                        }
                        else
                        {
                            lockedPurchasedMessage = "\nYou do not have enough cash!";
                        }

                    }


                }
                else if (swordTreeArray[NodeColumnIndex, NodeRowIndex].isLocked == true)
                {
                    lockedPurchasedMessage = "\nThis Node Is Locked \nYou Cannot Purchase This Node at This Time";
                }
                else if (swordTreeArray[NodeColumnIndex, NodeRowIndex].isPurchased == true && wasJustPurchased != true)
                {
                    lockedPurchasedMessage = "\nThis Node Has Already Been Purchased";
                }

                if (lastNode != null)
                {
                    lastNode.Selected = true;
                }

                selectedNode = swordTreeArray[NodeColumnIndex, NodeRowIndex];

                for (int i = 0; i < 7; i++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        if (swordTreeArray.GetValue(i, x) != null)
                        {
                            swordTreeArray[i, x].setRec(moveNodes);

                        }
                    }
                }

                if (wasJustPurchased)
                {
                    nodeMessage = swordTreeArray[NodeColumnIndex, NodeRowIndex].getName + ":  $" + swordTreeArray[NodeColumnIndex, NodeRowIndex].getCost + lockedPurchasedMessage + swordTreeArray[NodeColumnIndex, NodeRowIndex].getChangesString;
                }
                else
                {
                    nodeMessage = swordTreeArray[NodeColumnIndex, NodeRowIndex].getName + ":  $" + swordTreeArray[NodeColumnIndex, NodeRowIndex].getCost + lockedPurchasedMessage;
                }

                infoBox.deleteMessage();
                infoBox.receiveMessage("Players Cash:  $" + playersCash + "  " + nodeMessage);
                infoBox.update();

            }
            oldKeys = keys;
        }

        private void changeTreeGameState(TreeGameState newState)
        {
            ItemSelectClass.CurrentTreeGameState = newState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (swordTreeArray.GetValue(i, x) != null)
                    {
                        swordTreeArray[i, x].Draw(spriteBatch);
                    }
                }

                infoBox.draw(spriteBatch);
                swordPicture.Draw(spriteBatch);
                bombPicture.Draw(spriteBatch);
                bowPicture.Draw(spriteBatch);
            }
        }

        public void stopKeyPress()
        {
            oldKeys = keys = Keyboard.GetState();
        }

    }

}
