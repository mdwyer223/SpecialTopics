using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class Node
    {
        
        public Node parent;

        public Rectangle CollisionRec
        {
            get;
            set;
        }

        public Vector2 Location
        {
            get;
            set;
        }

        public float AvgDis
        {
            get { return PathDis + EndDis; }
        }

        public float PathDis
        {
            get;
            set;
        }

        public float EndDis
        {
            get;
            set;
        }

        public Node()
        {
            Location = Vector2.Zero;

            CollisionRec = new Rectangle();            

        }

        public Node(Vector2 location)
        {
            Location = location;

            CollisionRec = new Rectangle();

        }

        public bool isColliding(Rectangle target)
        {
            return CollisionRec.Intersects(target);
        }

    }
}
