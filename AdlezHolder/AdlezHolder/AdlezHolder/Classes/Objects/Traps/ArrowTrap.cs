using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class ArrowTrap : BaseSprite
    {
        int spawnTimer, spawnTime;
        const int MAX_SPAWN_TIME = 90, LOW_SPAWN_TIME = 40;
        float scaleFactor;

        Texture2D projectileImage;
        Vector2 arrowVelocity;

        public override BaseSpriteStruct SaveData
        {
            get
            {
                BaseSpriteStruct myData = base.SaveData;
                myData.saveId = "BAt";
                return myData;
            }
            set
            {
                base.SaveData = value;
            }
        }

        public ArrowTrap(float scaleFactor, Vector2 startPosition, Vector2 velocity)
            : base(Game1.GameContent.Load<Texture2D>("Traps/arrowtrap"), 
            scaleFactor, Game1.DisplayWidth, 0, startPosition)
        {
            this.spawnTime = 70;
            this.spawnTimer = 100;
            this.arrowVelocity = velocity;
            this.scaleFactor = scaleFactor;
            this.projectileImage = Game1.GameContent.Load<Texture2D>("Items/SteelArrow");
        }

        public override void Update(Map data, GameTime gameTime)
        {
            Random rand = new Random();

            spawnTimer++;
            if (spawnTimer >= spawnTime)
            {
                addArrow(data);
                spawnTime = rand.Next(LOW_SPAWN_TIME, MAX_SPAWN_TIME);
                spawnTimer = 0;
            }

            base.Update(data, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        private void addArrow(Map data)
        {
            data.CurrentData.addProjectile((new Projectile(2, 500, 2.0f, this.scaleFactor, arrowVelocity, 
                this.Position, projectileImage)));
        }
    }
}
