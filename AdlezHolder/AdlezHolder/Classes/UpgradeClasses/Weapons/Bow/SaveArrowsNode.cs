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
    class UpgradeSaveArrowsNode : UpgradeNode
    {
        public UpgradeSaveArrowsNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            : base(texture, scaleFactor, startPosition, price)
        {
            this.setNodeName("Save Arrows Node");
            this.setCost(price);
        }

        public override void upgradeBow(Bow x)
        {
            x.SaveArrows(true);
            base.upgradeBow(x);
            this.setChangesString("\nYour Bow Now Has The Save Arrows Ability!");
        }

        public override string getEffectsString()
        {
            return "This Will Randomly Not Take Arrows From Your Inventory When You Shoot!";
        }

    }
}
