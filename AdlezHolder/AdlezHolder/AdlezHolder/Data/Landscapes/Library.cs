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
    public class Library : MapDataHolder
    {
        bool bookVis = true;


        public bool BookVisibility
        {
            get { return bookVis; }
            set { bookVis = value; }
        }

           public Library()
            : base()
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Buildings/Library/LibraryDONE");
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
            //Actual book
            if (BookVisibility)
                spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Levon/Levon"), new Rectangle(500,298,58,35), Color.White); //Levon

            //Decoration Books
            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Levon/Levon2"), new Rectangle(176, 378, 58, 35), Color.White);
            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Levon/Levon3"), new Rectangle(369, 400, 58, 35), Color.White);
            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Levon/Levon4"), new Rectangle(580, 230, 58, 35), Color.White);
            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Levon/Levon5"), new Rectangle(426, 360, 58, 35), Color.White);
            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Levon/Levon2"), new Rectangle(225, 560, 58, 35), Color.White);
            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Levon/Levon3"), new Rectangle(380, 320, 58, 35), Color.White);
            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Levon/Levon4"), new Rectangle(127, 236, 58, 35), Color.White);
            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Levon/Levon5"), new Rectangle(600, 400, 58, 35), Color.White);
            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Levon/Levon5"), new Rectangle(231, 272, 58, 35), Color.White);
        }
    }
}
