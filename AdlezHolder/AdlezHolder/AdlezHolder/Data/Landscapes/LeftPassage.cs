using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class LeftPassage : MapDataHolder
    {
        public LeftPassage()
            : base()
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/LeftPassage");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);

            music = Game1.GameContent.Load<Song>("Music/TravelingSong");
        }

        public override void Update(Map map, GameTime gameTime)
        {
            base.Update(map, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
