using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace AdlezHolder
{
    class BowSpeedNode : UpgradeNode
    {
        public BowSpeedNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            : base(texture, scaleFactor, startPosition, price)
        {
        }
        public void upgrade(double multiplier, Bow bow)
        {
            //bow.UpgradeSpeed(multiplier);
        }
    }
}

