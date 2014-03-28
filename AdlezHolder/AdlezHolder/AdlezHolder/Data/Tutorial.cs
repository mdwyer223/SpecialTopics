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
    public class Tutorial : MapDataHolder
    {
        bool moveDown, moveUp, moveRight, moveLeft,
            switchWeapons, attacked, pauseUsed, introDone, changePos;

        KeyboardState keys, oldkeys;

        float timerPerMove = 0, introTimer = 0, messageIndex = 0;

        public Tutorial()
            : base()
        {
            keys = oldkeys = Keyboard.GetState();
        }

        public override void Update(Map map, GameTime gameTime)
        {
            keys = Keyboard.GetState();
            //if (keys.IsKeyDown(Keys.B) && oldkeys.IsKeyUp(Keys.B))
            //{
            //    Game1.newCutscene(new DeathAnimation(), map.Player);
            //}

            if (!changePos)
            {
                map.Player.Position = new Vector2(400, 240);
                changePos = true;
            }
            introTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            base.Update(map, gameTime);
            oldkeys = keys;
        }
    }
}
