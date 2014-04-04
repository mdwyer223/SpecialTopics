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
    class UpgradeSeekArrowNode : UpgradeNode
     {
        public UpgradeSeekArrowNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            : base(texture, scaleFactor, startPosition, price)
        {
            this.setNodeName("Seek Arrows Node");
            this.setCost(price);
        }

        public override void upgradeBow(Bow x)
        {
            x.SeekArrows(true);
            base.upgradeBow(x);
            this.setChangesString("\nYour Bow Now Has Seek Arrows!");
        }

        public override string getEffectsString()
        {
            return "\nThis Will Make Your Bow's Arrows Heat Seeking, They Will Chase Down Your Enemies!";
        }
}
}

