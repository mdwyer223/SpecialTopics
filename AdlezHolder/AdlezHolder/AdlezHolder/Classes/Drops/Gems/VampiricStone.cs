using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class VampiricStone : Gem
    {
        public VampiricStone(float scaleFactor, Vector2 startPosition)
            :base(null, scaleFactor, startPosition, "Vampiric Stone", 50)
        {
            setImage(Game1.GameContent.Load<Texture2D>("Items/MagentaStone"));

            Chance = .1f;
        }
    }
}
