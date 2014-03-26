﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class MessageBox
    {
        String message;
        String currentMessage, printedMessage, tempString;
        SpriteFont spritefont;
        Vector2 messagePosition;
        Boolean boxFull = false, heightCheck = false, cutOff = false, complete = false;
        protected Boolean messageComplete = false;
        protected Boolean visible;
        int count, messageWidth, messageHeight, nextLineCount = 0, spaceIndex, prevSpaceIndex, lineCount, checkCharacterCount;
        KeyboardState keys;
        KeyboardState oldKeys;
        Vector2 messageSize, checkSizeWidth, checkSizeHeight, checkSizeTemp, continueTextSize;
        Rectangle rectangle;
        Texture2D texture;
        //MapDataHolder data;

        public Boolean Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public Boolean Complete
        {
            get { return complete; }
            set { complete = false; }
        }

        public MessageBox(float scaleFactor)
        {
            texture = Game1.GameContent.Load<Texture2D>("The best thing ever");
                    
            spritefont = Game1.GameContent.Load<SpriteFont>("SpriteFont1");

            rectangle.Width = (int)((Game1.DisplayWidth * scaleFactor) + 0.5f);
            float aspectRatio = (float)texture.Width / texture.Height;
            rectangle.Height = (int)((rectangle.Width / aspectRatio) / 1.5 + 0.5f);

            rectangle.Y = Game1.DisplayHeight - rectangle.Height;
            messagePosition = new Vector2(rectangle.X + 11, rectangle.Y + 4);
            continueTextSize = spritefont.MeasureString("Press space to continue");

            //data = new MapDataHolder();
        }

        public void update()//fix the cut off when the box is full, get the box to disappear
        {
             if (Visible)
            {
                keys = Keyboard.GetState();
                messageWidth = (int)messageSize.X;
                messageHeight = (int)messageSize.Y;
                 if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
                {
                    if (messageComplete)
                    {
                        hide();
                    }
                    else if (boxFull)
                    {
                        printedMessage = "";
                        currentMessage = "";
                        nextLineCount = 0;
                        lineCount = 0;
                        tempString = "";
                    }
                }

                if (!messageComplete)
                {
                    if (count < message.Length && visible)
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
                    else if (!visible && count >= message.Length)
                    {
                        count = 0;
                        currentMessage = "";
                        nextLineCount = 0;
                        lineCount = 0;
                        printedMessage = "";
                    }
                }

                oldKeys = keys;
                printedMessage = currentMessage;

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

                if (count >= message.Length)
                {
                    messageComplete = true;
                }

                prevSpaceIndex = spaceIndex;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("The best thing ever"), rectangle, Color.White);
                if (printedMessage != null)
                {
                    spriteBatch.DrawString(spritefont, printedMessage, messagePosition, Color.White);
                }
                if (boxFull && (checkCharacterCount >= 70))
                {
                    spriteBatch.DrawString(spritefont, "Press space to continue", new Vector2(rectangle.Width - (int)(continueTextSize.X * 1.1), rectangle.Y + rectangle.Height - continueTextSize.Y + 5), Color.White);
                }
                else if (messageComplete)
                {
                    spriteBatch.DrawString(spritefont, "Press space to continue", new Vector2(rectangle.Width - (int)(continueTextSize.X * 1.1), rectangle.Y + rectangle.Height - continueTextSize.Y + 5), Color.White);
                }
            }
        }

        public void hide()
        {
            Visible = false;
            messageComplete = false;
            complete = true;
            count = 0;
            nextLineCount = 0;
            lineCount = 0;
            //message = "";
            //tempString = "";
            printedMessage = "";
            currentMessage = ""; 
            //data.DialogFinished(true);
            if (Game1.PreviousGameState == GameState.CUTSCENE)
            {
                Game1.MainGameState = GameState.CUTSCENE;
                //Game1.PreviousGameState = GameState.BLANK;
            }
            else
            {
                Game1.MainGameState = GameState.PLAYING;
            }
        }

        public void show(string message)
        {
            Visible = true;
            if (this.message == "" || this.message == null)
            {
                this.message = message;
                messageSize = spritefont.MeasureString(this.message);
            }  
        }
    }
}
