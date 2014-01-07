using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class FireStone : Gem
    {
        //orange
        public FireStone(Texture2D texture, float scaleFactor, Vector2 startPosition, string tag, int value)
            :base(texture, scaleFactor, startPosition, tag, value)
        {
        }
    }
}
