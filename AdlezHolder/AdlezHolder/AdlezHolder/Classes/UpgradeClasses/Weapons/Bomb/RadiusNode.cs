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
    class RadiusNode : UpgradeNode
    {
        double multiplier;
        public RadiusNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            : base(texture, scaleFactor, startPosition, price)
        {
            this.setNodeName("Radius Node");
            multiplier = 1.5;
            this.setCost(price);
        }

        public override void upgradeBomb(Bomb x)
        {
            x.UpgradeRadius(multiplier);
            base.upgradeBomb(x);
            this.setChangesString("\nYour Bomb's Explosion Radius Increased!");
        }

        public override string getEffectsString()
        {
            return "\nThis Makes Your Bomb's Radius Increase By 1 And a Half Times! ";
        }
    }
}