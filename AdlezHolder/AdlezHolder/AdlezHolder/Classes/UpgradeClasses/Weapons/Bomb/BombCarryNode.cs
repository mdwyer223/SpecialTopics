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
    class BombCarryNode : UpgradeNode
    {
        public BombCarryNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            : base(texture, scaleFactor, startPosition, price)
        {  
            this.setNodeName("Carry Limit Node");
            this.setCost(price);
        }

        public override void upgradeBomb(Bomb x)
        {
            x.increaseCarryLimit(1);
            base.upgradeBomb(x);
            this.setChangesString("\nYou Can Now Carry " + x.CarryLimit  + " Bombs!");
        }

        public override string getEffectsString()
        {
            return "\nThis Increases Your Carry Limit By 1!";
        }
    }
}

