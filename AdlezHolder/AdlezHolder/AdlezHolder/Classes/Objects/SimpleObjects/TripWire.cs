using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class TripWire
    {
        Rectangle tripper;
        Vector2 position;
        MapDataHolder data;

        bool ifTripped;

        public MapDataHolder Data
        {
            get { return data; }
        }

        public Rectangle Tripper
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y,
                    tripper.Width, tripper.Height);
            }
            protected set { tripper = value; }
        }

        public Vector2 Position
        {
            get{return position;}
            set {position = value;}
        }

        public bool IfTripped
        {
            get { return ifTripped; }
            protected set { ifTripped = value; }
        }

        public TripWire(float scaleFactor, Rectangle tripper)
        {
            this.tripper = tripper;

            position = new Vector2(tripper.X, tripper.Y);

            //float aspectRatio = (float)this.tripper.Width / this.tripper.Height;
            //this.tripper.Width = (int)(displayWidth * scaleFactor + 0.5f);
            //this.tripper.Height = (int)(this.tripper.Width / aspectRatio + 0.5f);
        }

        public void Update(Rectangle playerRec)
        {
            ifTripped = (playerRec.Intersects(Tripper));
        }
    }
}
