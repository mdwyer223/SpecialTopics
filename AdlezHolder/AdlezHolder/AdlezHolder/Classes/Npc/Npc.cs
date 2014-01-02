using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace AdlezHolder
{
    public class Npc : WanderingSprite
    {
        MessageBox box;
        bool isTalking;

        public Rectangle TalkRec
        {
            get { return new Rectangle(LeftRec.X, TopRec.Y, (RightRec.X + RightRec.Width) - LeftRec.X, (BottomRec.Y + BottomRec.Height) - TopRec.Y); }
        }

        protected Npc()
        {
        }

        public Npc(Texture2D defaultTexture, Texture2D messageBoxBackground,float scaleFactor, int SecondsToCrossScreen,
             int displayWidth, int displayHeight, Vector2 startPosition)
            : base(defaultTexture, scaleFactor, SecondsToCrossScreen, displayWidth,startPosition)
        {
            box = new MessageBox(messageBoxBackground, 1, displayWidth, displayHeight);
        }

        public override void Update(Map data)
        {
            box.Update(data);

            if (!isTalking)
            {
                this.wander();
            }

            if (!box.IsVisible)
            {
                stopTalk();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            box.Draw(spriteBatch);            
        }

        public void talk()
        {
            // face the direction of the player
            // show message box
            // random select message  
            if (!isTalking)
            {
                isTalking = true;
                string[] message = new string[3];
                message[0] = "I HATE U ALOT.";
                message[1] = "STAY AWAY FROM ME.";
                message[2] = "BLAH BLAH BLAH BLAH BLAH";
                box.show(message);
            }
        }

        public void stopTalk()
        {
            isTalking = false;
            box.hide();
        }
    }
}
