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
    class SpeedBowNode: UpgradeNode
    {
   
        double multiplier;
        public SpeedBowNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            : base(texture, scaleFactor, startPosition, price)
        {
            this.setNodeName("Speed Node");
            multiplier = 1.25;
            this.setCost(price);
        }

        public override void upgradeBow(Bow x)
        {
            x.UpgradeSpeed(multiplier);
            base.upgradeBow(x);
            this.setChangesString("Your Bow's Speed Has Increased!");
        }
    }
}
