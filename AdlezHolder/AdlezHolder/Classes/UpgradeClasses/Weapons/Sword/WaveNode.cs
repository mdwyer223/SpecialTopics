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
    class WaveNode : UpgradeNode
     {
        public WaveNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            : base(texture, scaleFactor, startPosition, price)
        {
            this.setNodeName("Wave Node");
            this.setCost(price);
        }
        public override void upgradeSword(Sword x)
        {
            x.UpgradeWave(true);
            base.upgradeSword(x);
            this.setChangesString("\nYour Sword Now Has The Wave Ability!");
        }


        public override string getEffectsString()
        {
            return "This Gives Your Sword A Wave Of Damage When You Swing! ";
        }

    }
}
