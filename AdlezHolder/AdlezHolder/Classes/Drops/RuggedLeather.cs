using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class RuggedLeather : Item
    {
        //DO NOT IMPLEMENT THIS CLASS
        public RuggedLeather()
            : base(null, 0f, Vector2.Zero, "", false, false, false, 0, 1)
        {
        }

        public override string getEffectsString()
        {
            return " ";
        }
        public override string getName()
        {
            return "Rugged Leather";
        }

        public override string getChangesString()
        {
            return "+1 Leather";
        }

    }
}
