using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AdlezHolder
{
    class BombESlotNode : UpgradeNode
    {
        public BombESlotNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            :base(texture,scaleFactor,startPosition, price)
        {
            this.setNodeName("Enchantment Slots Node");
            this.setCost(price);
        }

        public override void upgradeBomb(Bomb x)
        {
            x.increaseEnchantmentSlot(1);
            base.upgradeBomb(x);
            this.setChangesString("\nYour Enchantment Slots Have Increased By 1!");
        }
        public override string getEffectsString()
        {
            return "\nThis Increases Your Bomb's Enchantment Slots By 1! ";
        }

    }
}
