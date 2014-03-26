using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    class NodeMessageBox
    {
        String message;
        String currentMessage, printedMessage, tempString;
        SpriteFont spritefont;
        Vector2 messagePosition;
        Boolean boxFull = false, heightCheck = false, cutOff = false, messageComplete = false;
        protected Boolean visible;
        int count, messageWidth, messageHeight, nextLineCount = 0, spaceIndex, prevSpaceIndex, lineCount, checkCharacterCount;
        KeyboardState keys;
        KeyboardState oldKeys;
        Vector2 messageSize, checkSizeWidth, checkSizeHeight, checkSizeTemp, continueTextSize;
        Rectangle rectangle;
        Texture2D texture;
        int messageLength;

        public NodeMessageBox(float scaleFactor)
        {
            texture = Game1.GameContent.Load<Texture2D>("The best thing ever");
                    
            spritefont = Game1.GameContent.Load<SpriteFont>("SpriteFont1");

            rectangle.Width = (int)((Game1.DisplayWidth * scaleFactor) + 0.5f);
            float aspectRatio = (float)texture.Width / texture.Height;
            rectangle.Height = (int)((rectangle.Width / aspectRatio) / 1.5 + 0.5f);

            rectangle.Y = Game1.DisplayHeight - rectangle.Height;
            messagePosition = new Vector2(rectangle.X + 11, rectangle.Y + 4);
        }

        public void update()
        {
                keys = Keyboard.GetState();
                messageWidth = (int)messageSize.X;
                messageHeight = (int)messageSize.Y;
                for (int i = 0; i < message.Length; i++)
                {
                    if (!messageComplete)
                    {
                        if (count < message.Length)
                        {
                            if (!boxFull)
                            {
                                spaceIndex = message.IndexOf(" ", count);
                                if (currentMessage != null && prevSpaceIndex != spaceIndex)
                                {
                                    if (spaceIndex > 0)
                                    {
                                        tempString = currentMessage.Substring(nextLineCount) + message.Substring(prevSpaceIndex + 1, spaceIndex - prevSpaceIndex - 1);
                                    }
                                    else
                                    {
                                        tempString = currentMessage.Substring(nextLineCount) + message.Substring(prevSpaceIndex + 1);
                                    }
                                }

                                if (tempString != null)
                                {
                                    checkSizeTemp = spritefont.MeasureString(tempString);
                                    if (((int)checkSizeTemp.X >= ((int)rectangle.Width - 35)) && currentMessage != "")
                                    {
                                        cutOff = true;
                                    }
                                }

                                if (!cutOff)
                                {
                                    currentMessage += message.Substring(count, 1);
                                    count++;
                                    lineCount++;
                                }

                                checkSizeHeight = spritefont.MeasureString(currentMessage);
                                checkSizeWidth = spritefont.MeasureString(currentMessage.Substring(nextLineCount));

                                if ((int)checkSizeHeight.Y >= ((int)rectangle.Height - 35))
                                {
                                    boxFull = true;
                                    heightCheck = true;
                                }

                                if (cutOff && currentMessage != "")
                                {
                                    currentMessage += "\n";
                                    nextLineCount = lineCount;
                                    tempString = "";
                                    cutOff = false;
                                }
                            }
                        }
                        //else if (count >= message.Length)
                        //{
                        //    count = 0;
                        //    currentMessage = "";
                        //    nextLineCount = 0;
                        //    lineCount = 0;
                        //    printedMessage = "";
                        //}
                    }

                    oldKeys = keys;
                    printedMessage = currentMessage;
                    if (count < message.Length)
                    {
                        if (printedMessage == "")
                        {
                            boxFull = false;
                        }

                        if (heightCheck == true)
                        {
                            currentMessage += message.Substring(count, 1);
                            count++;
                            spaceIndex = message.IndexOf(" ", count);

                            if (currentMessage != null && prevSpaceIndex != spaceIndex)
                            {
                                if (spaceIndex > 0)
                                {
                                    tempString = currentMessage.Substring(nextLineCount) + message.Substring(prevSpaceIndex + 1, spaceIndex - prevSpaceIndex - 1);
                                }
                                else
                                {
                                    tempString = currentMessage.Substring(nextLineCount) + message.Substring(prevSpaceIndex + 1);
                                }
                            }

                            checkCharacterCount = tempString.Length;

                            if (checkCharacterCount > 70)
                            {
                                heightCheck = false;
                                nextLineCount = lineCount;
                            }

                        }
                    }
                    prevSpaceIndex = spaceIndex;
                }
            
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("The best thing ever"), rectangle, Color.White);               
            if (printedMessage != null)               
            {                
                spriteBatch.DrawString(spritefont, printedMessage, messagePosition, Color.White);             
            }               
        }

        public void deleteMessage()
        {

            count = 0;
            nextLineCount = 0;
            lineCount = 0;
            tempString = "";
            printedMessage = "";
            currentMessage = "";
        }

        public void receiveMessage(string message)
        {
            this.message = message;
            messageSize = spritefont.MeasureString(this.message);
            messageLength = message.Length;
        }
        public int getLength
        {
            get { return messageLength; }
        }

    }
}
