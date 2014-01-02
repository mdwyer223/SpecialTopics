using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Gem : Item
    {
        int damage;
        float applyDamageTime, chance;

        public int Damage
        {
            get { return damage; }
        }

        public float Chance
        {
            get { return chance; }
        }

        public float Duration
        {
            get { return applyDamageTime; }
        }

        private Gem()
        {
        }

        public Gem(Texture2D texture, float scaleFactor, Vector2 startPosition, string tag, int value)
            :base(texture, scaleFactor, startPosition, tag, true, false, false, value)
        {
        }
    }
}
