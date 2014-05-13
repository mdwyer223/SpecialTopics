﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;

//namespace AdlezHolder
//{

//    class TreeDisplay
//    {
//        UpgradeNode[,] tree;
//        KeyboardState keys, oldKeys;
//        UpgradeNode selectedNode, lastNode;
//        Vector2 nodePosition;
//        int NodeColumnIndex, NodeRowIndex;
//        Texture2D nodeTexture = Game1.GameContent.Load<Texture2D>("Particle");
//        float scalefactor = .3f;
//        int moveNodes;

//        public TreeDisplay(UpgradeNode[,] tree)
//        {
//            this.tree = tree;
//            NodeColumnIndex = 1;
//            NodeRowIndex = 0;
//            Rectangle overScan;
//            int displayWidth, displayHeight;
//            int widthSeperation, heightSeparation, nodeTopRow, nodeMiddleRow, nodeBottomRow;

//            displayWidth = Game1.DisplayWidth;
//            displayHeight = Game1.DisplayHeight;

//            int marginWidth = (int)(displayWidth * .05);
//            int marginHeight = (int)(displayHeight * .05);


//            overScan.Width = displayWidth - marginWidth;
//            overScan.Height = displayHeight - marginHeight;

//            overScan.X = displayWidth + marginWidth;
//            overScan.Y = displayHeight - marginHeight;

//            heightSeparation = overScan.Height / 6;
//            widthSeperation = overScan.Width / 4;

//            nodePosition.X = widthSeperation;
//            nodePosition.Y = heightSeparation;

//            nodeBottomRow = heightSeparation * 4;
//            nodeMiddleRow = heightSeparation * 3;
//            nodeTopRow = heightSeparation * 2;
//        }

//        public void Update(GameTime gameTime)
//        {
//            moveNodes = 0;
//            keys = Keyboard.GetState();
//            if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
//            {

//                if (tree[NodeColumnIndex, NodeRowIndex] != null)
//                {
//                    lastNode = tree[NodeColumnIndex, NodeRowIndex];
//                    if (NodeRowIndex != 2)
//                    {
//                        NodeRowIndex = (NodeRowIndex + 1) % 3;
//                    }
//                    else
//                    {
//                        NodeRowIndex = 0;
//                        tree[NodeColumnIndex, NodeRowIndex].Selected = false;
//                    }
//                    if (tree[NodeColumnIndex, NodeRowIndex] != null)
//                    {
//                        tree[NodeColumnIndex, NodeRowIndex].Selected = false;
//                    }
//                    else
//                    {
//                        NodeRowIndex = 1;
//                        tree[NodeColumnIndex, NodeRowIndex].Selected = false;
//                        lastNode = tree[1, 0];
//                    }
//                    lockedPurchasedMessage = "";
//                }
//            }

//            if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
//            {
//                if (tree[NodeColumnIndex, NodeRowIndex] != null)
//                {
//                    lastNode = tree[NodeColumnIndex, NodeRowIndex];
//                    if (NodeRowIndex != 0)
//                    {
//                        NodeRowIndex = (NodeRowIndex - 1) % 3;
//                    }
//                    else
//                    {
//                        NodeRowIndex = 2;
//                    }
//                    if (tree[NodeColumnIndex, NodeRowIndex] != null)
//                    {
//                        tree[NodeColumnIndex, NodeRowIndex].Selected = false;
//                    }
//                    else
//                    {
//                        NodeRowIndex = 1;
//                        tree[NodeColumnIndex, NodeRowIndex].Selected = false;
//                        lastNode = tree[1, 0];
//                    }
//                    lockedPurchasedMessage = "";
//                }
//            }

//            if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
//            {

//                if (NodeColumnIndex != 0)
//                {
//                    lastNode = tree[NodeColumnIndex, NodeRowIndex];
//                    NodeColumnIndex--;
//                    NodeRowIndex = 1;
//                    tree[NodeColumnIndex, NodeRowIndex].Selected = false;
//                    moveNodes = 150;
//                    lockedPurchasedMessage = "";
//                }



//            }

//            if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
//            {
//                if (NodeColumnIndex != 6)
//                {
//                    lastNode = tree[NodeColumnIndex, NodeRowIndex];
//                    NodeColumnIndex++;
//                    NodeRowIndex = 1;
//                    tree[NodeColumnIndex, NodeRowIndex].Selected = false;
//                    moveNodes = -150;
//                    lockedPurchasedMessage = "";
//                }
//            }



//            if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
//            {

//                if (tree[NodeColumnIndex, NodeRowIndex].isLocked != true && tree[NodeColumnIndex, NodeRowIndex].isPurchased != true)
//                {

//                    if (playersCash >= tree[NodeColumnIndex, NodeRowIndex].getCost)
//                    {
//                        lockedPurchasedMessage = "\nPurchase Complete!!!!";
//                        tree[NodeColumnIndex, NodeRowIndex].upgradeSword(tempSword);
//                        tree[NodeColumnIndex, NodeRowIndex].purchaseItem();
//                        tempCharacter.subtractFunds(tree[NodeColumnIndex, NodeRowIndex].getCost);
//                        playersCash = tempCharacter.Money;

//                        if (NodeColumnIndex > 0 && tree[NodeColumnIndex - 1, NodeRowIndex] != null)
//                            tree[NodeColumnIndex - 1, NodeRowIndex].unlockItem();
//                        if (NodeColumnIndex < 6 && tree[NodeColumnIndex + 1, NodeRowIndex] != null)
//                            tree[NodeColumnIndex + 1, NodeRowIndex].unlockItem();
//                        if (NodeRowIndex < 2 && tree[NodeColumnIndex, NodeRowIndex + 1] != null)
//                            tree[NodeColumnIndex, NodeRowIndex + 1].unlockItem();
//                        if (NodeRowIndex > 0 && tree[NodeColumnIndex, NodeRowIndex - 1] != null)
//                            tree[NodeColumnIndex, NodeRowIndex - 1].unlockItem();
//                    }
                        
//                    else
//                    {
//                        lockedPurchasedMessage = "\nYou do not have enough cash!";
//                    }

//                }
//                else if (tree[NodeColumnIndex, NodeRowIndex].isLocked == true)
//                {
//                    lockedPurchasedMessage = "\nThis Node Is Locked";
//                }
//                else
//                {
//                    lockedPurchasedMessage = "\nThis Node Has Already Been Purchased";
//                }
//            }



//            if (lastNode != null)
//            {
//                lastNode.Selected = true;
//            }
//            oldKeys = keys;




//            selectedNode = multiNodeArray[NodeColumnIndex, NodeRowIndex];

//            for (int i = 0; i < 7; i++)
//            {
//                for (int x = 0; x < 3; x++)
//                {
//                    if (tree.GetValue(i, x) != null)
//                    {
//                        tree[i, x].setRec(moveNodes);

//                    }
//                }
//            }

//            nodeMessage = tree[NodeColumnIndex, NodeRowIndex].getName + ":  $" + tree[NodeColumnIndex, NodeRowIndex].getCost + lockedPurchasedMessage;
//            infoBox.deleteMessage();
//            infoBox.receiveMessage("Players Cash:  $" + playersCash + "  " + nodeMessage);
//            infoBox.update();


//        }



//        public void Draw(SpriteBatch spriteBatch)
//        {

//            for (int i = 0; i < 7; i++)
//            {
//                for (int x = 0; x < 3; x++)
//                {
//                    if (tree.GetValue(i, x) != null)
//                    {
//                        tree[i, x].Draw(spriteBatch);

//                    }
//                }
//            }
//            infoBox.draw(spriteBatch);
//        }
//    }
//}