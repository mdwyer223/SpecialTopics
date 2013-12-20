using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class AnimatedSprite : BaseSprite
    {
        Animation currentAnimation;
        protected Orientation direction;
        int frameindex;
        float elapse;

        public Orientation Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public AnimatedSprite(Texture2D texture, float scaleFactor, int inDisplayWidth, float SecondsToCrossScreen,
             Vector2 startPosition)
            : base(texture, scaleFactor, inDisplayWidth, SecondsToCrossScreen, startPosition)
        {
            Texture2D[] ani = new Texture2D[1];
            ani[0] = texture;
            playAnimation(new FullAnimation(ani, 5));
        }

        public override void Update(Map data, GameTime gameTime)
        {
            elapse += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapse >= currentAnimation.FrameRate)
            {
                elapse = 0;
                frameindex = (frameindex + 1) % currentAnimation.FrameCount;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentAnimation.getFrame(frameindex), DrawnRec, ImageColor);
        }

        protected void playAnimation(FullAnimation animation)
        {  
            switch (direction)
            {
                case Orientation.DOWN:
                    if (currentAnimation == animation.Down)
                        return;

                        frameindex = 0;
                        currentAnimation = animation.Down;
                        break;
                case Orientation.LEFT:
                    if (currentAnimation == animation.Left)
                        return;

                        frameindex = 0;
                        currentAnimation = animation.Left;
                        break;
                case Orientation.RIGHT:
                    if (currentAnimation == animation.Right)
                        return;

                        frameindex = 0;
                        currentAnimation = animation.Right;
                        break;
                case Orientation.UP:
                    if (currentAnimation == animation.Up)
                        return;

                        frameindex = 0;
                        currentAnimation = animation.Up;
                        break;
            }

        }

    }
}
