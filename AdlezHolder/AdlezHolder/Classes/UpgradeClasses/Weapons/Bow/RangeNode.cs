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
    class RangeNode : UpgradeNode
    {
        double multiplier;
        public RangeNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            : base(texture, scaleFactor, startPosition, price)
        {
            this.setNodeName("Range Node");
            multiplier = 1.5;
            this.setCost(price);
        }

        public override void upgradeBow(Bow x)
        {
            x.UpgradeRange(multiplier);
            base.upgradeBow(x);
            this.setChangesString("\nYour Bow's Range Has Increased!");
        }
        public override string getEffectsString()
        {
            return "\nThis Increases Your Bow's Range By One and a Half Times! ";
        }

    }
}
