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
        protected int damage, tier;
        protected bool collected;
        float applyDamageTime, chance;

        public int Damage
        {
            get { return damage; }
            protected set { damage = value; }
        }

        public float Chance
        {
            get { return chance; }
            protected set { chance = value; }
        }

        public float Duration
        {
            get { return applyDamageTime; }
            protected set { applyDamageTime = value; }
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
