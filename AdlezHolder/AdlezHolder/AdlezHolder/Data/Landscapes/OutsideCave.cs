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
    public class OutsideCave : MapDataHolder
    {
        SpriteBatch sBatch;
        LibraryCutscene lC;
        Rectangle rec;
        bool startScene = true;

        public OutsideCave()
            : base()
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Landscapes/CAVE Empty");
            backgroundRec = new Rectangle(0, 0, Game1.DisplayWidth, Game1.DisplayHeight);

            music = Game1.GameContent.Load<Song>("Music/TravelingSong");

            lC = new LibraryCutscene();
            rec = new Rectangle(630, 350, 50, 50);
        }

        public override void Update(Map map, GameTime gameTime)
        {
            if (startScene)
            {
                Game1.newCutscene(lC, map.Player);
                startScene = false;
            }

            base.Update(map, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (lC.Pit)
                spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), rec, Color.White);
        }
    }
}
