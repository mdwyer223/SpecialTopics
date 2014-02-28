using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class PoisonStone : Gem
    {
        //purple
        public PoisonStone(GemStruct gemData)
            : base(gemData)
        {
        }

        public PoisonStone(float scaleFactor, Vector2 startPosition, int tier)
            : base(null, scaleFactor, startPosition, "Poison Stone", 0)
        {
            if (tier == 1)
            {
                //load texture, value, and stats
            }
            else if (tier == 2)
            {
            }
            else if (tier == 3)
            {
            }
            else if (tier == 4)
            {
            }
            else if (tier == 5)
            {
            }
            else
            {
            }
        }
    }
}
