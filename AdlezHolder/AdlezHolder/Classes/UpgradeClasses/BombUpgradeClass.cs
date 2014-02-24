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
    class BombUpgradeClass
    {
        UpgradeNode[,] bombTreeArray;
        KeyboardState keys, oldKeys;
        UpgradeNode selectedNode, lastNode;
        int NodeColumnIndex, NodeRowIndex;
        Texture2D bombImage = Game1.GameContent.Load<Texture2D>("bomb selected");
        float scalefactor = .3f;
        int moveNodes, playersCash;
        Character tempCharacter;
        Bomb tempBomb;
        NodeMessageBox infoBox;
        BaseSprite bombPicture;
        string nodeMessage, lockedPurchasedMessage;
        bool wasJustPurchased = false;


        public BombUpgradeClass(Character Character)
        {
            tempBomb = new Bomb(.3f);
            bombTreeArray = tempBomb.GetTree(Game1.DisplayWidth, Game1.DisplayHeight);
            keys = Keyboard.GetState();
            oldKeys = keys;
            NodeColumnIndex = 1;
            NodeRowIndex = 0;
            infoBox = new NodeMessageBox(1);
            nodeMessage = bombTreeArray[NodeColumnIndex, NodeRowIndex].getName + ":  $" + bombTreeArray[NodeColumnIndex, NodeRowIndex].getCost;
            infoBox.receiveMessage("Players Cash:  $" + playersCash + "  " + nodeMessage);
            lockedPurchasedMessage = "\nPress Enter to Purchase";
            bombPicture = new BaseSprite(bombImage, .1f, Game1.DisplayWidth, 0, Vector2.One);


            bombTreeArray[0, 1].Selected = false;
            bombTreeArray[0, 1].unlockItem();
            NodeColumnIndex = 0;
            NodeRowIndex = 1;

            tempCharacter = Character;
            playersCash = tempCharacter.Money;

        }


        public void Update(GameTime gameTime)
        {
            playersCash = tempCharacter.Money;
            moveNodes = 0;
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Q) && oldKeys.IsKeyUp(Keys.Q))
            {
                changeTreeGameState(TreeGameState.BOWTREE);
                return;
            }
            else if (keys.IsKeyDown(Keys.E) && oldKeys.IsKeyUp(Keys.E))
            {
                changeTreeGameState(TreeGameState.SWORDTREE);
                return;
            }

            if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
            {
                lockedPurchasedMessage = "\nPress Enter to Purchase";
                wasJustPurchased = false;
                if (bombTreeArray[NodeColumnIndex, NodeRowIndex] != null)
                {
                    lastNode = bombTreeArray[NodeColumnIndex, NodeRowIndex];
                    if (NodeRowIndex != 2)
                    {
                        NodeRowIndex = (NodeRowIndex + 1) % 3;
                    }
                    else
                    {
                        NodeRowIndex = 0;
                        bombTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    }
                    if (bombTreeArray[NodeColumnIndex, NodeRowIndex] != null)
                    {
                        bombTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    }
                    else
                    {
                        NodeRowIndex = 1;
                        bombTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                        lastNode = bombTreeArray[1, 0];
                    }
                }
            }

            if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
            {
                if (bombTreeArray[NodeColumnIndex, NodeRowIndex] != null)
                {
                    lockedPurchasedMessage = "\nPress Enter to Purchase";
                    wasJustPurchased = false;
                    lastNode = bombTreeArray[NodeColumnIndex, NodeRowIndex];
                    if (NodeRowIndex != 0)
                    {
                        NodeRowIndex = (NodeRowIndex - 1) % 3;
                    }
                    else
                    {
                        NodeRowIndex = 2;
                    }
                    if (bombTreeArray[NodeColumnIndex, NodeRowIndex] != null)
                    {
                        bombTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    }
                    else
                    {
                        NodeRowIndex = 1;
                        bombTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                        lastNode = bombTreeArray[1, 0];
                    }
                }
            }

            if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
            {
                lockedPurchasedMessage = "\nPress Enter to Purchase";
                if (NodeColumnIndex != 0)
                {
                    lastNode = bombTreeArray[NodeColumnIndex, NodeRowIndex];
                    NodeColumnIndex--;
                    NodeRowIndex = 1;
                    bombTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    moveNodes = 75;
                    wasJustPurchased = false;
                }



            }

            if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
            {
                lockedPurchasedMessage = "\nPress Enter to Purchase";
                if (NodeColumnIndex != 6)
                {
                    lastNode = bombTreeArray[NodeColumnIndex, NodeRowIndex];
                    NodeColumnIndex++;
                    NodeRowIndex = 1;
                    bombTreeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
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
                if (bombTreeArray[NodeColumnIndex, NodeRowIndex].isLocked != true && bombTreeArray[NodeColumnIndex, NodeRowIndex].isPurchased != true)
                {
                    if (tempCharacter.Money >= bombTreeArray[NodeColumnIndex, NodeRowIndex].getCost)
                    {
                        nodeMessage = "Purchase Complete";
                        wasJustPurchased = true;
                        bombTreeArray[NodeColumnIndex, NodeRowIndex].upgradeBomb(tempBomb);
                        bombTreeArray[NodeColumnIndex, NodeRowIndex].purchaseItem();
                        tempCharacter.subtractFunds(bombTreeArray[NodeColumnIndex, NodeRowIndex].getCost);

                        if (NodeColumnIndex > 0 && bombTreeArray[NodeColumnIndex - 1, NodeRowIndex] != null)
                            bombTreeArray[NodeColumnIndex - 1, NodeRowIndex].unlockItem();
                        if (NodeColumnIndex < 6 && bombTreeArray[NodeColumnIndex + 1, NodeRowIndex] != null)
                            bombTreeArray[NodeColumnIndex + 1, NodeRowIndex].unlockItem();
                        if (NodeRowIndex < 2 && bombTreeArray[NodeColumnIndex, NodeRowIndex + 1] != null)
                            bombTreeArray[NodeColumnIndex, NodeRowIndex + 1].unlockItem();
                        if (NodeRowIndex > 0 && bombTreeArray[NodeColumnIndex, NodeRowIndex - 1] != null)
                            bombTreeArray[NodeColumnIndex, NodeRowIndex - 1].unlockItem();
                    }

                    else
                    {
                        lockedPurchasedMessage = "\nYou do not have enough cash!";
                    }
                }

            }
            else if (bombTreeArray[NodeColumnIndex, NodeRowIndex].isLocked == true)
            {
                lockedPurchasedMessage = "\nThis Node Is Locked \nYou Cannot Purchase This Node at This Time";
            }
            else if (bombTreeArray[NodeColumnIndex, NodeRowIndex].isPurchased == true && wasJustPurchased != true)
            {
                lockedPurchasedMessage = "\nThis Node Has Already Been Purchased";
            }


            selectedNode = bombTreeArray[NodeColumnIndex, NodeRowIndex];

            for (int i = 0; i < 7; i++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (bombTreeArray.GetValue(i, x) != null)
                    {
                        bombTreeArray[i, x].setRec(moveNodes);

                    }
                }
            }

            if (wasJustPurchased)
            {
                nodeMessage = bombTreeArray[NodeColumnIndex, NodeRowIndex].getName + ":  $" + bombTreeArray[NodeColumnIndex, NodeRowIndex].getCost + lockedPurchasedMessage + bombTreeArray[NodeColumnIndex, NodeRowIndex].getChangesString;
            }
            else
            {
                nodeMessage = bombTreeArray[NodeColumnIndex, NodeRowIndex].getName + ":  $" + bombTreeArray[NodeColumnIndex, NodeRowIndex].getCost + lockedPurchasedMessage;
            }
            oldKeys = keys;



            infoBox.deleteMessage();
            infoBox.receiveMessage("Players Cash:  $" + tempCharacter.Money + "  " + nodeMessage);
            infoBox.update();

            selectedNode = bombTreeArray[NodeColumnIndex, NodeRowIndex];

            for (int i = 0; i < 7; i++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (bombTreeArray.GetValue(i, x) != null)
                    {
                        bombTreeArray[i, x].setRec(moveNodes);

                    }
                }
            }

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
                    if (bombTreeArray.GetValue(i, x) != null)
                    {
                        bombTreeArray[i, x].Draw(spriteBatch);

                    }
                }
                infoBox.draw(spriteBatch);
                bombPicture.Draw(spriteBatch);
            }
        }

        public void stopKeyPress()
        {
            oldKeys = keys = Keyboard.GetState();
        }

    }
}