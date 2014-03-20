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

        Color FADED = new Color(75, 50, 50, 75);
        Color NORMAL = new Color(255, 255, 255, 250);

        SpriteFont font;        
        bool selected;

        public bool Selected
        {
            get { return selected; }
            set 
            {
                if (value)
                    ImageColor = NORMAL;
                else
                    ImageColor = FADED;
                selected = value;
            }
        }

        public Button(Texture2D texture, float scaleFactor, Vector2 startPosition)
            : base(texture, scaleFactor,Game1.DisplayHeight, 0, startPosition)
        {
            this.font = Game1.GameContent.Load<SpriteFont>("SpriteFont1");         
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            // spriteBatch.DrawString(font, message, this.position, Color.Red);
        }

    }

}
