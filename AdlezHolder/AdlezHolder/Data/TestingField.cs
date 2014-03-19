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
    public class TestingField : MapDataHolder
    {
        bool itemAdd = false, setPlayer = false;
        KeyboardState keys, oldKeys;

        public TestingField()
            : base()
        {
            backgroundRec = new Rectangle(0, 0, 0, 0);
            

            Skeleton sprite = new Skeleton(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"),
                .04f, 8, new Vector2(300, -300));
            addEnemy(sprite);
        }

        public override void Update(Map map, GameTime gameTime)
        {
            keys = Keyboard.GetState();
            if (!itemAdd)
            {
                //addItem(new Arrow(.02f, false, "Wooden Arrow", 10, new Vector2(300, -200)));
                //addItem((new Arrow(.02f, false, "Wooden Arrow", 10, new Vector2(325, -200))));
                //addItem((new Arrow(.02f, false, "Wooden Arrow", 10, new Vector2(350, -200))));
                //addItem((new Arrow(.02f, false, "Wooden Arrow", 10, new Vector2(375, -200))));
                //addItem((new Arrow(.02f, false, "Wooden Arrow", 10, new Vector2(400, -200))));
                //addItem((new Arrow(.02f, false, "Wooden Arrow", 10, new Vector2(425, -200))));
                //addItem((new Arrow(.02f, false, "Wooden Arrow", 10, new Vector2(450, -200))));
                itemAdd = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Game1.ParticleState = ParticleState.RAIN;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                Game1.ParticleState = ParticleState.OFF;
            }

            if (keys.IsKeyDown(Keys.K) && oldKeys.IsKeyUp(Keys.K))
            {
                Skeleton sprite = new Skeleton(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"),
                    .04f, 8, new Vector2(300, 300));
                addEnemy(sprite);
            }
            base.Update(map, gameTime);

            if (!setPlayer)
            {
                map.Player.Position = new Vector2(Game1.DisplayWidth / 2, Game1.DisplayHeight / 2);
                setPlayer = true;
            }
            oldKeys = keys;
        }
    }
}
