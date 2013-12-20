using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Button : BaseSprite
    {

        //GraphicsDevice.Viewport.Width
        Color Normal;
        string message;
        SpriteFont font;
        Color faded;

        public Button(Texture2D texture, float scaleFactor, int displayWidth, Vector2 startPosition, SpriteFont font,string message)
            : base(texture, scaleFactor, displayWidth, 0, startPosition)
        {
            this.ImageColor = new Color(75, 50, 50, 75);
            this.font = font;
            this.message = message;
            this.faded = new Color(255, 255, 255, 250);
            Normal = new Color(75, 50, 50, 75);
        }

        public void changeColor()
        {
            this.ImageColor = new Color(150, 150, 150, 75);

        }

        public void MakeTransparent()
        {
            this.ImageColor = new Color(0, 0, 0, 0);

        }
        public virtual void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(font, message, this.position, Color.Red);

        }

        public void  Selected(Button button)
        {
            button.ImageColor = faded;

        }

        public void OrignalColor(Button button)
        {
            button.ImageColor = Normal;
        }

        public void enteredPress()
        {
        }
    }

}
