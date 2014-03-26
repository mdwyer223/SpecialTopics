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
    class ManualDetNode : UpgradeNode
    {
        public ManualDetNode(Texture2D texture, float scaleFactor,Vector2 startPosition, int price)
            :base(texture,scaleFactor,startPosition, price)
        {
            this.setNodeName("Manual Detonation Node");
            this.setCost(price);
        }

        public override void upgradeBomb(Bomb x)
        {
            x.ManualDet(true);
            base.upgradeBomb(x);
            this.setChangesString("\nYour Bomb Now Has The Manual Denonation Ability");
        }
    }
}
