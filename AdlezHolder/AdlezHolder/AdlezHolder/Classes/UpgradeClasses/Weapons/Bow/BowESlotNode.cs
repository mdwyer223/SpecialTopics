﻿using System;
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
    class BowESlotNode : UpgradeNode
    {
        int increase;
        public BowESlotNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            : base(texture, scaleFactor, startPosition, price)
        {
            this.setNodeName("Enchantment Slots Node");
            increase = 1;
            this.setCost(price);
        }

        public override void upgradeBow(Bow x)
        {
            x.IncreaseEnchantmentSlots(increase);
            base.upgradeBow(x);
            this.setChangesString("Your Bow Now Has An Extra Enchament Slot!");
        }
    }
}
