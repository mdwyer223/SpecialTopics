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
        }

        public override void Update(Map map, GameTime gameTime)
        {
            keys = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                //Game1.ParticleState = ParticleState.RAIN;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                //Game1.ParticleState = ParticleState.SNOW;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                Game1.ParticleState = ParticleState.OFF;
            }

            if (keys.IsKeyDown(Keys.K) && oldKeys.IsKeyUp(Keys.K))
            {
                Thing sprite = new Thing(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"),
                    .04f, new Vector2(300, 300));
                //addEnemy(sprite);
                //Thing t = new Thing(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"), .04f, new Vector2(300, 400));
                //addEnemy(t);
            }
            base.Update(map, gameTime);

            if (!setPlayer)
            {
                map.Player.Position = new Vector2(Game1.DisplayWidth / 2, Game1.DisplayHeight / 2);
                setPlayer = true;
            }
            if (!itemAdd)
            {
                addImmovable((new Teleporter(new Rectangle(100, 200, 50, 3), "")));
                addImmovable((new Teleporter(new Rectangle(175, 200, 50, 3), "")));
                addImmovable((new Teleporter(new Rectangle(250, 200, 50, 3), "")));
                addImmovable((new Teleporter(new Rectangle(325, 200, 50, 3), "nwot")));
                addImmovable((new Teleporter(new Rectangle(400, 200, 50, 3), "mroom2")));
                addImmovable((new Teleporter(new Rectangle(475, 200, 50, 3), "mroom1")));
                addItem(new Key(new Vector2(300, 240), KeyType.GOLD, 1));
                addImmovable(new Chest(.07f, new Vector2(300, 300), ChestType.GOLD));
                addImmovable(new Chest(.07f, new Vector2(225, 300), ChestType.SILVER));
                addImmovable(new Chest(.07f, new Vector2(175, 300), ChestType.BRONZE));
                addImmovable(new Chest(.07f, new Vector2(100, 300), ChestType.DUNGEON));
                itemAdd = true;
            }
            oldKeys = keys;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {   
            base.Draw(spriteBatch);
        }
    }
}
