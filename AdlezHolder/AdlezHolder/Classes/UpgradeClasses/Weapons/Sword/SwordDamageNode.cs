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
    class SwordDamageNode : UpgradeNode
    
    {
        double multiplier;
        public SwordDamageNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            : base(texture, scaleFactor, startPosition, price)
        {
            this.setNodeName("Damage Node");
            multiplier = 1.5;
            this.setCost(price);
        }

        public override void upgradeSword(Sword x)
        {
            x.UpgradeDamage(multiplier);
            base.upgradeSword(x);
            this.setChangesString("\nYour Sword Now Does " + x.Damage + " Damage!");
        }

    }
}
