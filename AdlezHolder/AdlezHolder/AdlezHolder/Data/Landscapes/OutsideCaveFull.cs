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
    public class OutsideCaveFull : MapDataHolder
    {
        SpriteBatch sBatch;
        //LibraryCutscene lC;
        Rectangle rec;
        //bool startScene = true;

        public OutsideCaveFull()
            : base()
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/CAVEfull");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);

            music = Game1.GameContent.Load<Song>("Music/TravelingSong");

            //lC = new LibraryCutscene();
            rec = new Rectangle(630, 350, 50, 50);
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

