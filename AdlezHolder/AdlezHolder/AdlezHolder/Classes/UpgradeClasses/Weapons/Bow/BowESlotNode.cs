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
    class UpgradeESlotBowNode : UpgradeNode
    {
        public UpgradeESlotBowNode(Texture2D texture, float scaleFactor, Vector2 startPosition)
            : base(texture, scaleFactor, startPosition)
        { 
        }

        public void upgrade(int numOfEnchantments, Bow bow)
        {
           // bow.increaseEnchantmentSlot();
        }
    }
}
