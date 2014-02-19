using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class Trigger : BaseSprite
    {
        Texture2D currentImage;
        Texture2D firstImage;
        Texture2D triggeredImage;

        public Trigger(Texture2D firstTexture, Texture2D secondTexture, int displayWidth,
            float secondsToCrossScreen, float scaleFactor, Vector2 start)
            : base(firstTexture, scaleFactor, displayWidth, secondsToCrossScreen, start)
        {
            this.currentImage = firstTexture;
            this.firstImage = firstTexture;
            this.triggeredImage = secondTexture;
        }

        public void toggle()
        {
            if (currentImage.Equals(firstImage))
            {
                currentImage = triggeredImage;
            }
            else
            {
                currentImage = firstImage;
            }
        }
    }
}
