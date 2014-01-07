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
        UpgradeNode[,] multiNodeArray;
        KeyboardState keys, oldKeys;
        UpgradeNode selectedNode, lastNode;
        Vector2 nodePosition;
        int NodeColumnIndex, NodeRowIndex;
        Texture2D nodeTexture = Game1.GameContent.Load<Texture2D>("MenuButtons/Continue");
        float scalefactor = .3f;
        int moveNodes;

        public SwordUpgradeClass(Sword sword)
        {
            oldKeys = Keyboard.GetState();
            NodeColumnIndex = 1;
            NodeRowIndex = 0;
            Rectangle overScan;
            int displayWidth, displayHeight;
            int widthSeperation, heightSeparation, nodeTopRow, nodeMiddleRow,nodeBottomRow;

            displayWidth = Game1.DisplayWidth;
            displayHeight = Game1.DisplayHeight;

            int marginWidth = (int)(displayWidth * .05);
            int marginHeight = (int)(displayHeight * .05);


            overScan.Width = displayWidth - marginWidth;
            overScan.Height = displayHeight - marginHeight;

            overScan.X = displayWidth + marginWidth;
            overScan.Y = displayHeight - marginHeight;

            heightSeparation = overScan.Height / 6;
            widthSeperation = overScan.Width / 4;

            nodePosition.X =widthSeperation  ;
            nodePosition.Y = heightSeparation;

            nodeBottomRow = heightSeparation * 4;
            nodeMiddleRow = heightSeparation * 3;
            nodeTopRow = heightSeparation * 2;


            multiNodeArray = new UpgradeNode[7, 3];
            //Row1
            multiNodeArray[0, 0] = null;
            nodePosition.Y = nodeMiddleRow;
            multiNodeArray[0, 1] = new SwordDamageNode(nodeTexture, scalefactor, nodePosition);
            multiNodeArray[0, 2] = null;
            //Row 2
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            multiNodeArray[1, 0] = new SwordESlotNode(nodeTexture, scalefactor, nodePosition);//work on eSLots
            nodePosition.Y = nodeMiddleRow;
            multiNodeArray[1, 1] = new SizeNode(nodeTexture, scalefactor, nodePosition);
            nodePosition.Y = nodeBottomRow;
            multiNodeArray[1, 2] = new SpeedSwordNode(nodeTexture, scalefactor, nodePosition);

            //Row 3
            nodePosition.X = nodePosition.X + widthSeperation;
            multiNodeArray[2, 0] = null;
            nodePosition.Y = nodeMiddleRow;
            multiNodeArray[2, 1] = new SwordDamageNode(nodeTexture, scalefactor, nodePosition);
            multiNodeArray[2, 2] = null;
            //Row 4
            nodePosition.X = nodePosition.X + widthSeperation;
            multiNodeArray[3, 0] = null;
            nodePosition.Y = nodeMiddleRow;
            multiNodeArray[3, 1] = new UpgradeNode(nodeTexture, scalefactor, nodePosition);
            multiNodeArray[3, 2] = null;
            //Row 5
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            multiNodeArray[4, 0] = new SwordDamageNode(nodeTexture, scalefactor, nodePosition);
            nodePosition.Y = nodeMiddleRow;
            multiNodeArray[4, 1] = new SpeedSwordNode(nodeTexture, scalefactor, nodePosition);
            nodePosition.Y = nodeBottomRow;
            multiNodeArray[4, 2] = new SwordESlotNode(nodeTexture, scalefactor, nodePosition);
            //Row 6
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y= nodeTopRow;
            multiNodeArray[5, 0] = new SpeedSwordNode(nodeTexture, scalefactor, nodePosition);
            nodePosition.Y = nodeMiddleRow;
            multiNodeArray[5, 1] = new SizeNode(nodeTexture, scalefactor, nodePosition);
            nodePosition.Y = nodeBottomRow;
            multiNodeArray[5, 2] = new SwordDamageNode(nodeTexture, scalefactor, nodePosition);
            //Row 7
            nodePosition.X = nodePosition.X + widthSeperation;
            nodePosition.Y = nodeTopRow;
            multiNodeArray[6, 0] = new SwordESlotNode(nodeTexture, scalefactor, nodePosition);//work on eSLots
            nodePosition.Y = nodeMiddleRow;
            multiNodeArray[6, 1] = new WaveNode(nodeTexture, scalefactor, nodePosition);
            nodePosition.Y = nodeBottomRow;
            multiNodeArray[6, 2] = new SizeNode(nodeTexture, scalefactor, nodePosition);

            multiNodeArray[0, 1].Selected = false;
            NodeColumnIndex = 0;
            NodeRowIndex = 1;
           
        }


        public void Update(GameTime gameTime)
        {
            moveNodes = 0;
            keys = Keyboard.GetState();
            lastNode = multiNodeArray[NodeColumnIndex, NodeRowIndex];
            if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
                {     
                       if(NodeRowIndex > 2)
                        NodeRowIndex = (NodeRowIndex - 1) % 3;

                        if (multiNodeArray[NodeColumnIndex, NodeRowIndex] != null)
                        {
                            multiNodeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                            lastNode.Selected = true;
                        }
                        else
                        {
                            NodeRowIndex = (NodeRowIndex + 1) % 3;
                        }

                }

            if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
            {
                if (NodeRowIndex > 0)
                {
                    NodeRowIndex = (NodeRowIndex + 1)% 3;
                    lastNode.Selected = true;
                    if (multiNodeArray[NodeColumnIndex, NodeRowIndex] != null)
                    {
                        multiNodeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    }
                }
            }

            if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
            {
               
                if ( NodeColumnIndex != 0)
                {
                    NodeColumnIndex--;
                    NodeRowIndex = 1;
                    lastNode.Selected = true;
                    multiNodeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                    moveNodes = 150;
                }
                


            }

            if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
            {
                    if (NodeColumnIndex != 6)
                    {
                        NodeColumnIndex++;
                        NodeRowIndex = 1;

                        lastNode.Selected = true;
                        multiNodeArray[NodeColumnIndex, NodeRowIndex].Selected = false;
                        moveNodes = -150;
                    }
              }
            
            oldKeys = keys;
            lastNode = multiNodeArray[NodeColumnIndex, NodeRowIndex];
            selectedNode = multiNodeArray[NodeColumnIndex, NodeRowIndex];
           
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
            }

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

        }
    }
}
