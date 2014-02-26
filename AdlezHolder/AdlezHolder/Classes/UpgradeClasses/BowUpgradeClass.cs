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
    class BowUpgradeClass
    {
        UpgradeNode[,] bowTreeArray;
        KeyboardState keys, oldKeys;
        UpgradeNode selectedNode, lastNode;
        int NodeColumnIndex, NodeRowIndex;
        Texture2D bowImage = Game1.GameContent.Load<Texture2D>("bow selected");
        int moveNodes, playersCash;
        Character tempCharacter;
        Bow tempBow;
        NodeMessageBox infoBox;
        BaseSprite bowPicture;
        string nodeMessage, lockedPurchasedMessage;
        bool wasJustPurchased = false;


        public BowUpgradeClass(Character Character)
        {
            tempBow = new Bow(.3f);
            bowTreeArray = tempBow.GetTree(Game1.DisplayWidth, Game1.DisplayHeight);
            keys = Keyboard.GetState();
            oldKeys = keys;
            NodeColumnIndex = 0;
            NodeRowIndex = 1;
            infoBox = new NodeMessageBox(1);
            nodeMessage = bowTreeArray[NodeColumnIndex, NodeRowIndex].getName + ":  $" + bowTreeArray[NodeColumnIndex, NodeRowIndex].getCost;
            infoBox.receiveMessage("Players Cash:  $" + playersCash + "  " + nodeMessage);
            lockedPurchasedMessage = "\nPress Enter to Purchase";
            bowPicture = new BaseSprite(bowImage, .1f, Game1.DisplayWidth, 0, Vector2.One);

            NodeColumnIndex = 0;
            NodeRowIndex = 1;

        
            bowTreeArray[0, 1].Selected = false;
            bowTreeArray[0, 1].unlockItem();
            tempCharacter = Character;
            playersCash = tempCharacter.Money;

        }


        public void Update(GameTime gameTime)
        {
            moveNodes = 0;
            playersCash = tempCharacter.Money;
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Q) && oldKeys.IsKeyUp(Keys.Q))
            {
                changeTreeGameState(TreeGameState.BOMBTREE);
                return;
            }
            if (keys.IsKeyDown(Keys.E) && oldKeys.IsKeyUp(Keys.E))
            {
                changeTreeGameState(TreeGameState.SWORDTREE);
                return;
            }

            if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
            {
                lockedPurchasedMessage = "\nPress Enter to Purchase";
                wasJustPurchased = false;
                if (bowTreeArray[NodeColumnIndex, NodeRowIndex] != null)
                {
                    lastNode = bowTreeArray[NodeColumnIndex, NodeRowIndex];
                    if (NodeRowIndex != 2)
                    {
                        NodeRowIndex = (NodeRowIndex + 1) % 3;
                    }
                    else
                    {
                        NodeRowIndex = 0;
                        bowTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    }
                    if (bowTreeArray[NodeColumnIndex, NodeRowIndex] != null)
                    {
                        bowTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    }
                    else
                    {
                        NodeRowIndex = 1;
                        bowTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                        lastNode = bowTreeArray[1, 0];
                    }
                }
            }

            if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
            {
                if (bowTreeArray[NodeColumnIndex, NodeRowIndex] != null)
                {
                    lockedPurchasedMessage = "\nPress Enter to Purchase";
                    wasJustPurchased = false;
                    lastNode = bowTreeArray[NodeColumnIndex, NodeRowIndex];
                    if (NodeRowIndex != 0)
                    {
                        NodeRowIndex = (NodeRowIndex - 1) % 3;
                    }
                    else
                    {
                        NodeRowIndex = 2;
                    }
                    if (bowTreeArray[NodeColumnIndex, NodeRowIndex] != null)
                    {
                        bowTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    }
                    else
                    {
                        NodeRowIndex = 1;
                        bowTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                        lastNode = bowTreeArray[1, 0];
                    }
                }
            }

            if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
            {
                lockedPurchasedMessage = "\nPress Enter to Purchase";
                if (NodeColumnIndex != 0)
                {
                    lastNode = bowTreeArray[NodeColumnIndex, NodeRowIndex];
                    NodeColumnIndex--;
                    NodeRowIndex = 1;
                    bowTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    moveNodes = 75;
                    wasJustPurchased = false;
                }



            }

            if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
            {
                lockedPurchasedMessage = "\nPress Enter to Purchase";
                if (NodeColumnIndex != 6)
                {
                    lastNode = bowTreeArray[NodeColumnIndex, NodeRowIndex];
                    NodeColumnIndex++;
                    NodeRowIndex = 1;
                    bowTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    moveNodes = -75;
                    wasJustPurchased = false;
                }
            }


            if (lastNode != null)
            {
                lastNode.Selected = true;
            }

            if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
            {
                if (bowTreeArray[NodeColumnIndex, NodeRowIndex].isLocked != true && bowTreeArray[NodeColumnIndex, NodeRowIndex].isPurchased != true)
                {
                    if (tempCharacter.Money >= bowTreeArray[NodeColumnIndex, NodeRowIndex].getCost)
                    {
                        nodeMessage = "Purchase Complete";
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


            selectedNode = bowTreeArray[NodeColumnIndex, NodeRowIndex];

            for (int i = 0; i < 7; i++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (bowTreeArray.GetValue(i, x) != null)
                    {
                        bowTreeArray[i, x].setRec(moveNodes);

                    }
                }
            }

            if (wasJustPurchased)
            {
                nodeMessage = bowTreeArray[NodeColumnIndex, NodeRowIndex].getName + ":  $" + bowTreeArray[NodeColumnIndex, NodeRowIndex].getCost + lockedPurchasedMessage + bowTreeArray[NodeColumnIndex, NodeRowIndex].getChangesString;
            }
            else
            {
                nodeMessage = bowTreeArray[NodeColumnIndex, NodeRowIndex].getName + ":  $" + bowTreeArray[NodeColumnIndex, NodeRowIndex].getCost + lockedPurchasedMessage;
            }

            infoBox.deleteMessage();
            infoBox.receiveMessage("Players Cash:  $" + tempCharacter.Money + "  " + nodeMessage);
            infoBox.update();

            selectedNode = bowTreeArray[NodeColumnIndex, NodeRowIndex];


            for (int i = 0; i < 7; i++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (bowTreeArray.GetValue(i, x) != null)
                    {
                        bowTreeArray[i, x].setRec(moveNodes);

                    }
                }
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
                    if (bowTreeArray.GetValue(i, x) != null)
                    {
                        bowTreeArray[i, x].Draw(spriteBatch);

                    }
                }
                infoBox.draw(spriteBatch);
                bowPicture.Draw(spriteBatch);
            }
        }

        public void stopKeyPress()
        {
            oldKeys = keys = Keyboard.GetState();
            
        }

    }
}
    