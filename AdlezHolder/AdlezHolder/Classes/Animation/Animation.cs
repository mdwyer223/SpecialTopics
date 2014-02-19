using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public enum Orientation
    {
        LEFT,RIGHT,
        UP,DOWN
    }

    public class Animation
    {
        Texture2D[] Frames =  new Texture2D[0];

        public Animation(Texture2D[] frames, float frameRate,Orientation direction)
        {
            Frames = frames;        
            FrameRate = frameRate;
            Direction = direction;
        }

        public int FrameCount { get { return Frames.Length; } }

        public float FrameRate { get; private set; }

        public Orientation Direction { get; private set; }

        public Texture2D getFrame(int frameindex)
        {
            return Frames[frameindex];
        }
        

    }
}
