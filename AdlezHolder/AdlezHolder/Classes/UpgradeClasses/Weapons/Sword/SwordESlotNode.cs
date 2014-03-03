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
    class SwordESlotNode : UpgradeNode
    {
        public SwordESlotNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            :base(texture,scaleFactor,startPosition, price)
        {
            this.setNodeName("Enchanment Slot Node");
            this.setCost(price);
        }
        public override void upgradeSword(Sword x)
        {
            x.increaseEnchantmentSlot(1);
            this.setChangesString("\nYour Sword Now Has " + x.ESlots + " Enchament Slots!");
            base.upgradeSword(x);
        }


        public override string getEffectsString()
        {
            return "\nThis Increases Your Sword's Enchantment Slots By One! ";
        }

    }
}
