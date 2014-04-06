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
    class TripMineNode : UpgradeNode
    {
        public TripMineNode(Texture2D texture, float scaleFactor, Vector2 startPosition, int price)
            : base(texture, scaleFactor, startPosition, price)
        {
            this.setNodeName("Trip Mine Node");
            this.setCost(price);
        }

        public override void upgradeBomb(Bomb x)
        {
            x.TripMine(true);
            base.upgradeBomb(x);
            this.setChangesString("\nYour Bomb Now Has The Trip Mine Ability");
        }

        public override string getEffectsString()
        {
            return "\nThis Gives Your Bomb The Trip Mine Ability ";
        }
    }
}