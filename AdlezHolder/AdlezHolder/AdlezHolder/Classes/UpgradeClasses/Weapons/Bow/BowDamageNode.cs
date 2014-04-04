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
    class BowDamageNode : UpgradeNode
    {
        double multiplier;
        public BowDamageNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            : base(texture, scaleFactor, startPosition, price)
        {
            this.setNodeName("Damage Node");
            multiplier = 1.5;
            this.setCost(price);
        }

        public override void upgradeBow(Bow x)
        {
            x.UpgradeDamage(multiplier);
            base.upgradeBow(x);
            this.setChangesString("\nYour Bow Now Does " + x.Damage + "!!!");
        }
        public override string getEffectsString()
        {
            return "\nThis Increases Your Bow's Damage By One and a Half Times! ";
        }

    }
}