using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace AdlezHolder
{
    public class BossRoom : MapDataHolder
    {
        Minotaur minotaur;
        string lastPlace = "";
        bool changePos = false, teleAdded;

        public BossRoom(Character player)
        {
            background = Game1.GameContent.Load<Texture2D>("BackgroundsAndFloors/Dungeons/FixedDungeon1/DungeonRoomBoss");
            teleID = "dungeon1";
            int x = (Game1.DisplayWidth - background.Width) / 2;
            int y = (Game1.DisplayHeight - background.Height);
            backgroundRec = new Rectangle(0, 0, background.Width, background.Height);

            Wall wall = new Wall(new Rectangle(130, 215 - backgroundRec.Height, backgroundRec.Width, backgroundRec.Height),
                Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), new Vector2(130, 215 - backgroundRec.Height));
            addImmovable(wall);

            wall = new Wall(new Rectangle(129 - backgroundRec.Width, 215, backgroundRec.Width, backgroundRec.Width),
                Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), new Vector2(129 - backgroundRec.Width, 215));
            addImmovable(wall);

            wall = new Wall(new Rectangle(852, 216, backgroundRec.Width, backgroundRec.Height),
                Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), new Vector2(852, 216));
            addImmovable(wall);

            wall = new Wall(new Rectangle(130, backgroundRec.Height, backgroundRec.Width, backgroundRec.Height),
                Game1.GameContent.Load<Texture2D>("Random/The best thing ever"), new Vector2(130, backgroundRec.Height));
            addImmovable(wall);

            minotaur = new Minotaur(Game1.GameContent.Load<Texture2D>("ComputerPpl/Bosses/D1 Boss/Move/F"),
                .15f, new Vector2(140, 220));
            addEnemy(minotaur);

            if (player.Direction == Orientation.UP)
            {
                player.Position = new Vector2((backgroundRec.Width / 2) - player.CollisionRec.Width,
                    backgroundRec.Height - player.CollisionRec.Height - 50);
            }
        }

        public override void Update(Map map, GameTime gameTime)
        {
            if (!everything.Contains(minotaur) && !teleAdded)
            {
                this.everything.Add(new Teleporter(new Rectangle(400, 300, 20, 20), "void", this.teleID));
                teleAdded = true;
            }
            
            base.Update(map, gameTime);
        }

    }
}
