using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class IceStone : Gem
    {
        float critDamagePercent;
        public float CritDamage
        {
            get { return critDamagePercent; }
            protected set { critDamagePercent = value; }
        }

        //cyan
        public IceStone(GemStruct gemData)
            : base(gemData)
        {
        }

        public IceStone(float scaleFactor, Vector2 startPosition, int tier)
            : base(null, scaleFactor, startPosition, "Ice Stone", 0)
        {
            if (tier == 1)
            {
                //load texture, value, and stats
                this.Chance = .1f;
                this.CritDamage = 1f;
                this.Duration = 3f;
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
