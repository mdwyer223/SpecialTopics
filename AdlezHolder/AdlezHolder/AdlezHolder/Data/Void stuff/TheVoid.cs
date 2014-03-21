using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class TheVoid : MapDataHolder
    {
        StarField sField;
        Galaxy spiral;
        Random rand = new Random();
        bool changePos = false;

        public TheVoid()
            : base()
        {
            int value = rand.Next(1, 3);
            if (value == 1)
            {
                sField = new StarField();
            }
            else
            {
                spiral = new Galaxy(10, 6);
            }
        }

        public TheVoid(string id, string newPlace)
            : base()
        {
            int value = rand.Next(1, 3);
            if (value == 1)
            {
                sField = new StarField();
            }
            else
            {
                spiral = new Galaxy(10, 6);
            }
            if (id == "dungeon1")
            {
                newPlace = "nwot";
                this.addImmovable(new Teleporter(new Rectangle(600, 240, 20, 20), newPlace));
            }
        }

        public override void Update(Map map, GameTime gameTime)
        {
            if (!changePos)
            {
                map.Player.Position = new Vector2(100, 240);
                changePos = true;
            }

            map.Player.decreaseColor(true, false, false);

            base.Update(map, gameTime);

            if (sField != null)
            {
                sField.Update(gameTime, this);
            }

            if (spiral != null)
            {
                spiral.Update(gameTime, this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (sField != null)
            {
                sField.Draw(spriteBatch);
            }

            if (spiral != null)
            {
                spiral.Draw(spriteBatch);
            }
        }
    }
}
