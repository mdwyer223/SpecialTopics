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
        Texture2D swordImage;
        UpgradeNode selectedNode, lastNode;
        UpgradeNode[,] multiNodeArray;
        int NodeColumnIndex, NodeRowIndex;
        float scalefactor = .3f;
        int moveNodes = 50, playersCash;
        Character tempCharacter;
        int  displayWidth = Game1.DisplayWidth;
        int displayHeight = Game1.DisplayHeight;
        Sword tempSword;
        BaseSprite swordPicture;
        bool wasJustPurchased = false;

        public SwordUpgradeClass()
        {
            oldKeys = Keyboard.GetState();
            Texture2D nodeTexture = Game1.GameContent.Load<Texture2D>("MenuButtons/Continue");
            tempCharacter = new Character(Game1.GameContent.Load<Texture2D>("MenuButtons/Continue"), scalefactor, 4, 3, Vector2.Zero);
            tempCharacter.addFunds(7500);
            playersCash = tempCharacter.Money;
            multiNodeArray = tempCharacter.Sword.GetTree(displayWidth, displayHeight);

            NodeColumnIndex = 1;
            NodeRowIndex = 0;
            multiNodeArray[0, 1].Selected = false;
            multiNodeArray[0, 1].unlockItem();
            NodeColumnIndex = 0;
            NodeRowIndex = 1;
            nodeMessage = "";
            infoBox = new NodeMessageBox(1);
            nodeMessage = multiNodeArray[NodeColumnIndex, NodeRowIndex].getName + ":  $" + multiNodeArray[NodeColumnIndex, NodeRowIndex].getCost;
            infoBox.receiveMessage("Players Cash:  $" + playersCash + "  " + nodeMessage);
            tempSword = tempCharacter.Sword;
            lockedPurchasedMessage = "\nPress Enter to Purchase";
            swordImage = Game1.GameContent.Load <Texture2D> ("Particle");

            swordPicture = new BaseSprite(swordImage, .1f, displayWidth, 0, Vector2.One);

        }


        public void Update(GameTime gameTime)
        {
            moveNodes = 0;
            keys = Keyboard.GetState();
           
            if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
                {
                    wasJustPurchased = false;
                    lockedPurchasedMessage = "\nPress Enter to Purchase";         
                        if (multiNodeArray[NodeColumnIndex, NodeRowIndex] != null)
                        {
                            lastNode = multiNodeArray[NodeColumnIndex, NodeRowIndex];
                            if (NodeRowIndex != 2)
                            {
                                NodeRowIndex = (NodeRowIndex + 1) % 3;
                            }
                            else
                            {
                                NodeRowIndex = 0;
                                multiNodeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                            }
                            if (multiNodeArray[NodeColumnIndex, NodeRowIndex] != null)
                            {
                                multiNodeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                            }
                            else
                            {
                                NodeRowIndex = 1;
                                multiNodeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                                lastNode = multiNodeArray[1, 0];
                            }
                         
                        }
                }

            if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
            {
                wasJustPurchased = false;
                lockedPurchasedMessage = "\nPress Enter to Purchase";  
                if (multiNodeArray[NodeColumnIndex, NodeRowIndex] != null)
                {
                    lastNode = multiNodeArray[NodeColumnIndex, NodeRowIndex];
                    if (NodeRowIndex != 0)
                    {
                        NodeRowIndex = (NodeRowIndex - 1) % 3;
                    }
                    else
                    {
                        NodeRowIndex = 2;
                    }
                    if (multiNodeArray[NodeColumnIndex, NodeRowIndex] != null)
                    {
                        multiNodeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    }
                    else
                    {
                        NodeRowIndex = 1;
                        multiNodeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                        lastNode = multiNodeArray[1, 0];
                    }
                }
            }

            if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
            {
                lockedPurchasedMessage = "\nPress Enter to Purchase";  
                if ( NodeColumnIndex != 0)
                {
                    wasJustPurchased = false;
                    lastNode = multiNodeArray[NodeColumnIndex, NodeRowIndex];
                    NodeColumnIndex--;
                    NodeRowIndex = 1;
                    multiNodeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    moveNodes = 150;
                }
                


            }

            if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
            {
                lockedPurchasedMessage = "\nPress Enter to Purchase";  
                    if (NodeColumnIndex != 6)
                    {
                        wasJustPurchased = false;
                        lastNode = multiNodeArray[NodeColumnIndex, NodeRowIndex];
                        NodeColumnIndex++;
                        NodeRowIndex = 1;
                        multiNodeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                        moveNodes = -150;
                    }
              }

            if (keys.IsKeyDown(Keys.Q) && oldKeys.IsKeyUp(Keys.Q))
            {
                changeTreeGameState(TreeGameState.BOMBTREE);
            }

            if (keys.IsKeyDown(Keys.E) && oldKeys.IsKeyUp(Keys.E))
            {
                changeTreeGameState(TreeGameState.BOWTREE);
            }
            if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
            {

                if (multiNodeArray[NodeColumnIndex, NodeRowIndex].isLocked != true && multiNodeArray[NodeColumnIndex, NodeRowIndex].isPurchased != true)
                {

                    if (playersCash >= multiNodeArray[NodeColumnIndex, NodeRowIndex].getCost)
                    {
                        lockedPurchasedMessage = "\nPurchase Complete!!!!";
                        multiNodeArray[NodeColumnIndex, NodeRowIndex].upgradeSword(tempSword);
                        multiNodeArray[NodeColumnIndex, NodeRowIndex].purchaseItem();
                        tempCharacter.subtractFunds(multiNodeArray[NodeColumnIndex, NodeRowIndex].getCost);
                        playersCash = tempCharacter.Money;
                        wasJustPurchased = true;

                        if (NodeColumnIndex > 0 && multiNodeArray[NodeColumnIndex - 1, NodeRowIndex] != null)
                            multiNodeArray[NodeColumnIndex - 1, NodeRowIndex].unlockItem();
                        if (NodeColumnIndex < 6 && multiNodeArray[NodeColumnIndex + 1, NodeRowIndex] != null)
                            multiNodeArray[NodeColumnIndex + 1, NodeRowIndex].unlockItem();
                        if (NodeRowIndex < 2 && multiNodeArray[NodeColumnIndex, NodeRowIndex + 1] != null)
                            multiNodeArray[NodeColumnIndex, NodeRowIndex + 1].unlockItem();
                        if (NodeRowIndex > 0 && multiNodeArray[NodeColumnIndex, NodeRowIndex - 1] != null)
                            multiNodeArray[NodeColumnIndex, NodeRowIndex - 1].unlockItem();


                    }
                    else
                    {
                        lockedPurchasedMessage = "\nYou do not have enough cash!";
                    }

                }

                   
            }

            else if (multiNodeArray[NodeColumnIndex, NodeRowIndex].isLocked == true)
                {
                    lockedPurchasedMessage = "\nThis Node Is Locked \nYou Cannot Purchase This Node at This Time";
                }
            else  if (multiNodeArray[NodeColumnIndex, NodeRowIndex].isPurchased == true && wasJustPurchased != true)
                {
                    lockedPurchasedMessage = "\nThis Node Has Already Been Purchased";
                }
            



            if (lastNode != null)
            {
                lastNode.Selected = true;
            }
            oldKeys = keys;
      



            selectedNode = multiNodeArray[NodeColumnIndex, NodeRowIndex];

            for (int i = 0; i < 7; i++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (multiNodeArray.GetValue(i, x) != null)
                    {
                        multiNodeArray[i, x].setRec(moveNodes);

                    }
                }
            }

            if (wasJustPurchased)
            {
                nodeMessage = multiNodeArray[NodeColumnIndex, NodeRowIndex].getName + ":  $" + multiNodeArray[NodeColumnIndex, NodeRowIndex].getCost + lockedPurchasedMessage + multiNodeArray[NodeColumnIndex, NodeRowIndex].getChangesString;
            }
            else
            {
                nodeMessage = multiNodeArray[NodeColumnIndex, NodeRowIndex].getName + ":  $" + multiNodeArray[NodeColumnIndex, NodeRowIndex].getCost + lockedPurchasedMessage;
            }
        
                infoBox.deleteMessage();
                infoBox.receiveMessage("Players Cash:  $" + playersCash + "  " + nodeMessage);
                infoBox.update();
           
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
                        if (multiNodeArray.GetValue(i, x) != null)
                        {
                            multiNodeArray[i, x].Draw(spriteBatch);
                        }
                    }

                    infoBox.draw(spriteBatch);
                    swordPicture.Draw(spriteBatch);
                }
            }
        }

    }
