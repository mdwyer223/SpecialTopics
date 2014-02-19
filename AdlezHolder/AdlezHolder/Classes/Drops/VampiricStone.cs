using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class VampiricStone : Gem
    {
        public VampiricStone(Texture2D texture, float scaleFactor, Vector2 startPosition, string tag, int value)
            :base(texture, scaleFactor, startPosition, tag, value)
        {
        }
    }
}
