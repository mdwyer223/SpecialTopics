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
    public class Npc : WanderingSprite
    {
        MessageBox box;
        bool isTalking;
        KeyboardState keys, oldKeys;
        string dialog;

        public Npc(Texture2D defaultTexture,float scaleFactor, int SecondsToCrossScreen, int displayWidth, int displayHeight, Vector2 startPosition, string message)
            : base(defaultTexture, scaleFactor, SecondsToCrossScreen, displayWidth,startPosition)
        {
            box = new MessageBox(1);
            dialog = message;
        }

        public override void Update(Map data)
        {
            keys = Keyboard.GetState();
         
            if (keys.IsKeyDown(Keys.T) && oldKeys.IsKeyUp(Keys.T)) 
            {
                //if (data.Player.measureCollison(this.collisionRec) ==
                if (data.Player.CollisionRec.Intersects(this.CollisionRec))
                    box.show(dialog);
            }
            else
                this.wander();

            oldKeys = keys;
            box.update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (box.Visible == true)
            {
                box.draw(spriteBatch);
            }
        }

        public void talk()
        {
            // face the direction of the player
            // show message box
            // random select message  
            if (!isTalking)
            {
                isTalking = true;
                //string[] message = new string[3];
                //message[0] = "I HATE U ALOT.";
                //message[1] = "STAY AWAY FROM ME.";
                //message[2] = "BLAH BLAH BLAH BLAH BLAH";
                string message = "blah blah blah blah";
                box.show(message);
            }
        }
        
        public void stopTalk()
        {
            isTalking = false;
        }
    }
}
