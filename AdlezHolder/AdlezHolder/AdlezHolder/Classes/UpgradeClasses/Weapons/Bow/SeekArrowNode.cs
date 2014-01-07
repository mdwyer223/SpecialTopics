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
        public UpgradeSeekArrowNode(Texture2D texture, float scaleFactor, Vector2 startPosition)
            : base(texture, scaleFactor, startPosition)
        {
        }
        public void upgrade(bool state, Bow bow)
        {
           // bow.upgradeSeekArrow(state);
        }
    }
}
