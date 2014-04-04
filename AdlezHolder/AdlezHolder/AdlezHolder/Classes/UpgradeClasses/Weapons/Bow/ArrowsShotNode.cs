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
    class ArrowsShotNode : UpgradeNode
    {
        int increase;
        public ArrowsShotNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            : base(texture, scaleFactor, startPosition, price)
        {
            this.setNodeName("Arrows Shot Node");
            increase = 1;
            this.setCost(price);
        }

        public override void upgradeBow(Bow x)
        {
            x.UpgradeArrowShots(increase);
            base.upgradeBow(x);
            this.setChangesString("\nYour Bow Now Shoots More Arrows!");
        }
        public override string getEffectsString()
        {
            return "\nThis Increases The Amount of Bows You Shoot By 1! ";
        }
    }
}
