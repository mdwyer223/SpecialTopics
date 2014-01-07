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
    
    class TreeDisplay
    {
        UpgradeNode[,] tree;
        KeyboardState keys, oldKeys;
        UpgradeNode selectedNode, lastNode;
        Vector2 nodePosition;
        int NodeColumnIndex, NodeRowIndex;
        Texture2D nodeTexture = Game1.GameContent.Load<Texture2D>("Particle");
        float scalefactor = .3f;
        int moveNodes;
        
        public TreeDisplay(UpgradeNode[,] tree)
        {
            this.tree = tree;
            NodeColumnIndex = 1;
            NodeRowIndex = 0;
            Rectangle overScan;
            int displayWidth, displayHeight;
            int widthSeperation, heightSeparation, nodeTopRow, nodeMiddleRow, nodeBottomRow;

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

            nodePosition.X = widthSeperation;
            nodePosition.Y = heightSeparation;

            nodeBottomRow = heightSeparation * 4;
            nodeMiddleRow = heightSeparation * 3;
            nodeTopRow = heightSeparation * 2;
        }

        public void Update(GameTime gameTime)
        {
            moveNodes = 0;
            keys = Keyboard.GetState();
            lastNode = tree[NodeColumnIndex, NodeRowIndex];
            if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
            {
                if (NodeRowIndex > 2)
                    NodeRowIndex = (NodeRowIndex - 1) % 3;

                if (tree[NodeColumnIndex, NodeRowIndex] != null)
                {
                    tree[NodeColumnIndex, NodeRowIndex].Selected = false;
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
                    NodeRowIndex = (NodeRowIndex + 1) % 3;
                    lastNode.Selected = true;
                    if (tree[NodeColumnIndex, NodeRowIndex] != null)
                    {
                        tree[NodeColumnIndex, NodeRowIndex].Selected = false;
                    }
                }
            }

            if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
            {

                if (NodeColumnIndex != 0)
                {
                    NodeColumnIndex--;
                    NodeRowIndex = 1;
                    lastNode.Selected = true;
                    tree[NodeColumnIndex, NodeRowIndex].Selected = false;
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
                    tree[NodeColumnIndex, NodeRowIndex].Selected = false;
                    moveNodes = -150;
                }
            }

            oldKeys = keys;
            lastNode = tree[NodeColumnIndex, NodeRowIndex];
            selectedNode = tree[NodeColumnIndex, NodeRowIndex];

        }


        public void Draw(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < 7; i++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (tree.GetValue(i, x) != null)
                    {
                        tree[i, x].setRec(moveNodes);
                    }
                }
            }
        }

    }
}
