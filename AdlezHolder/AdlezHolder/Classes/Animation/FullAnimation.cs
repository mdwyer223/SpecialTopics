using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class FullAnimation
    {
        public Animation Up { get; private set; }
        public Animation Down { get; private set; }
        public Animation Left { get; private set; }
        public Animation Right { get; private set; }

        public FullAnimation(Animation up, Animation down, Animation left, Animation right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }

        public FullAnimation(Texture2D[] up, Texture2D[] down, Texture2D[] left, Texture2D[] right, float frameRate)
        {
            Up = new Animation(up, frameRate, Orientation.UP);
            Down = new Animation(down, frameRate, Orientation.DOWN);
            Left = new Animation(left, frameRate, Orientation.LEFT);
            Right = new Animation(right, frameRate, Orientation.RIGHT);
        }

        public FullAnimation(Texture2D[] animation, float frameRate)
        {
            Up = new Animation(animation, frameRate, Orientation.UP);
            Down = new Animation(animation, frameRate, Orientation.DOWN);
            Left = new Animation(animation, frameRate, Orientation.LEFT);
            Right = new Animation(animation, frameRate, Orientation.RIGHT);
        }
    }
}
