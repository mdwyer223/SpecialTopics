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
    class SizeNode:UpgradeNode
    {
        double multiplier = 1.25;
        public SizeNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            : base(texture, scaleFactor, startPosition, price)
        {
            this.setNodeName("Size Node");
            this.setCost(price);
        }
        public override void upgradeSword(Sword x)
        {
            x.UpgradeSize(multiplier);
            base.upgradeSword(x);
            this.setChangesString("\nYour Swords Size Has Now Increased!");
        }

        public override string getEffectsString()
        {
            return "This Increases Your Swords Size One and a Quarter Times! ";
        }

    }
}
