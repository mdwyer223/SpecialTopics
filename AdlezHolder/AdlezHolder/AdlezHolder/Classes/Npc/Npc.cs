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

        public override BaseSpriteStruct SaveData
        {
            get
            {
                BaseSpriteStruct myData = base.SaveData;
                myData.saveId = "Npc";
                return myData;
            }
            set
            {
                base.SaveData = value;
            }
        }

        public Rectangle TalkRec
        {
            get { return new Rectangle(LeftRec.X, TopRec.Y, (RightRec.X + RightRec.Width) - LeftRec.X, (BottomRec.Y + BottomRec.Height) - TopRec.Y); }
        }

        public Npc(Texture2D defaultTexture, Texture2D messageBoxBackground,float scaleFactor, int SecondsToCrossScreen,
             int displayWidth, int displayHeight, Vector2 startPosition)
            : base(defaultTexture, scaleFactor, SecondsToCrossScreen, displayWidth,startPosition)
        {
            box = new MessageBox(1f);
        }

        public override void Update(Map data)
        {
            box.update();

            if (!isTalking)
            {
                this.wander();
            }

            if (!box.Visible)
            {
                stopTalk();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            box.draw(spriteBatch);            
        }

        public void talk()
        {
            // face the direction of the player
            // show message box
            // random select message  
            if (!isTalking)
            {
            }
        }

        public void stopTalk()
        {

            box.hide();
        }
    }
}
