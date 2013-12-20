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
    public class MessageBox : BaseSprite
    {
        string[] messages;
        string currentMessage;
        SpriteFont spritefont;
        Vector2 messagePosition;
        int count,messageIndex;
        KeyboardState keys;
        KeyboardState oldKeys;
        
        public MessageBox(Texture2D texture, float scaleFactor, int displayWidth, int displayHeight)
            : base(texture, scaleFactor,displayWidth, 0, Vector2.Zero)
        {
            this.hide();

            this.Position = new Vector2(0, displayHeight - DrawnRec.Height);

            messagePosition = new Vector2(Position.X + 11, Position.Y + 4);
            spritefont = Game1.GameContent.Load<SpriteFont>("SpriteFont1");           
        }

        public virtual void  Update()
        {           
            if (IsVisible)
            {
                keys = Keyboard.GetState();
                if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
                {
                    if (messageIndex + 1 < messages.Length)
                    {
                        messageIndex += 1;
                        count = 0;
                        currentMessage = "";
                    }
                    else
                    {
                        this.hide();
                        return;
                    }
                                    
                }

                if (count < messages[messageIndex].Length)
                {
                    currentMessage += messages[messageIndex].Substring(count, 1);
                    count++;
                }
                
                oldKeys = keys;
            }            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {   
            if(IsVisible)
            {
                base.Draw(spriteBatch);
                spriteBatch.DrawString(spritefont, currentMessage, messagePosition, Color.White);
            }

        }

        public void hide()
        {
            IsVisible = false;
            count = 0;
            currentMessage = "";
        }

        public void show(string message)
        {
            IsVisible = true;
            this.messages = new string[1];
            this.messages[0] = message;
            messageIndex = -1;
        }

        public void show(string[] message)
        {
            IsVisible = true;
            this.messages = message;
            messageIndex = -1;
        }
    }
}
