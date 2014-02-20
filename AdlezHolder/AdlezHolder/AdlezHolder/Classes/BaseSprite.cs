using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class BaseSprite
    {
        Texture2D image;
        protected Rectangle drawnRec,collisionRec;
        protected Vector2 position;
        protected Color color;
        protected int speed;
        private int invisRecWidth;
        private int widthDifference;

        protected bool canMoveRight, canMoveDown, canMoveLeft, canMoveUp;

        public bool CanMoveRight
        {
            get { return canMoveRight; }
            set { canMoveRight = value; }
        }

        public bool CanMoveLeft
        {
            get { return canMoveLeft; }
            set { canMoveLeft = value; }
        }

        public bool CanMoveDown
        {
            get { return canMoveDown; }
            set { canMoveDown = value; }
        }

        public bool CanMoveUp
        {
            get { return canMoveUp; }
            set { canMoveUp = value;}
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Center
        {
            get { return new Vector2(CollisionRec.X + (CollisionRec.Width / 2), 
                CollisionRec.Y + (CollisionRec.Height / 2)); }
            protected set
            {
                position.X = value.X - (CollisionRec.Width / 2);
                position.Y = value.Y - (CollisionRec.Height / 2);
            }// TODO: CHANGE: set; 
        }

        public Rectangle DrawnRec
        {
            get 
            {
                return new Rectangle(   
                   (int)position.X + drawnRec.X,
                   (int)position.Y + drawnRec.Y,
                   drawnRec.Width,
                   drawnRec.Height);
            }
            protected set { drawnRec = value; }
        }

        public virtual Rectangle CollisionRec
        {
            get
            {
                return new Rectangle(   
                    (int)DrawnRec.X,
                    (int)DrawnRec.Y + collisionRec.Y,
                    collisionRec.Width,
                    collisionRec.Height);
            }
            protected set { collisionRec = value; }
        }

        public Rectangle LeftRec
        {
            get { return new Rectangle(CollisionRec.X - invisRecWidth, widthDifference + CollisionRec.Y, invisRecWidth, CollisionRec.Height - widthDifference * 2); }
        }

        public Rectangle RightRec
        {
            get { return new Rectangle(CollisionRec.X + CollisionRec.Width, widthDifference + CollisionRec.Y, invisRecWidth, CollisionRec.Height - widthDifference * 2); }
        }

        public Rectangle TopRec
        {
            get { return new Rectangle(CollisionRec.X + widthDifference, CollisionRec.Y - invisRecWidth, CollisionRec.Width - widthDifference * 2, invisRecWidth); }
        }

        public Rectangle BottomRec
        {
            get { return new Rectangle(CollisionRec.X + widthDifference, CollisionRec.Y + CollisionRec.Height, CollisionRec.Width - widthDifference * 2, invisRecWidth); }
        }

        public Texture2D Image
        {
            get { return image; }
            protected set { image = value; }
        }

        public Color ImageColor
        {
            get { return color; }
            set { color = value; }
        }

        public bool IsDead
        {
            get;
            protected set;
        }

        public bool IsVisible
        {
            get;
            protected set;
        }

        protected BaseSprite()
        {
        }

        public void load(BaseSpriteVars inVar)
        {
            //image = inVar.image;
            ImageColor = inVar.imageColor;
            IsDead = inVar.isDead;
            IsVisible = inVar.isVisible;
            position = inVar.position;
            //Center = inVar.center;

            drawnRec.Width = inVar.drawnRec.Width;
            drawnRec.Height = inVar.drawnRec.Height;

            collisionRec.Width = inVar.collisionRec.Width;
            collisionRec.Height = inVar.collisionRec.Height;
        }

        public BaseSprite(Texture2D texture, float scaleFactor, int inDisplayWidth, float SecondsToCrossScreen
            ,Vector2 startPosition)
            : base()
        {
            image = texture;
            color = Color.White;

            IsVisible = true;
            IsDead = false;
            if (texture != null)
            {
                drawnRec.Width = (int)(inDisplayWidth * scaleFactor + 0.5f);
                float aspectRatio = (float)texture.Width / texture.Height;
                drawnRec.Height = (int)(drawnRec.Width / aspectRatio + 0.5f);
            }

            collisionRec.Width = drawnRec.Width;
            collisionRec.Height = (int)(drawnRec.Height / 1.25);

            if (SecondsToCrossScreen != 0)
            {
                speed = (int)(inDisplayWidth / (SecondsToCrossScreen * 60));
                invisRecWidth = speed * 4;
                
            }
            else
            {
                speed = 0;
                invisRecWidth = CollisionRec.Height / 4;
            }

            widthDifference = (int)(collisionRec.Width * .1f);
            Position = startPosition;
        }

        public virtual void Update(Map data)
        {
        }        
        
        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Update(Map data, GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible && Image != null)
                spriteBatch.Draw(Image, DrawnRec, ImageColor);
        }

        public bool isColliding(Rectangle target)
        {
            return CollisionRec.Intersects(target);
        }

        public Vector2 measureCollison(Rectangle target)
        {
            if (!CollisionRec.Intersects(target))
                return Vector2.Zero;

            Vector2 targetCenter = new Vector2(target.X + target.Width / 2, target.Y + target.Height / 2);

            Vector2[] myPoints = getCorners(CollisionRec);


            Vector2 point1 = Vector2.Zero;
            Vector2 point2 = Vector2.Zero;

            double minDis = double.MaxValue;
            foreach (Vector2 myPoint in myPoints)
            {
                Vector2[] targetPoints = getWallPoints(target, myPoint);
                foreach (Vector2 targetPoint in targetPoints)
                {
                    double distance = measureDistance(myPoint, targetPoint);
                    if (distance != 0 && distance < minDis)
                    {
                        minDis = distance;
                        point1 = myPoint;
                        point2 = targetPoint;
                    }

                }
            }

            Vector2 pointDis = point2 - point1;
            
            return pointDis;

        }

        private Vector2[] getWallPoints(Rectangle inRec, Vector2 point)
        {
            Vector2[] points = new Vector2[4];
            points[0] = new Vector2(inRec.X, point.Y);
            points[1] = new Vector2(inRec.X + inRec.Width, point.Y);
            points[2] = new Vector2(point.X, inRec.Y);
            points[3] = new Vector2(point.X, inRec.Y + inRec.Height);

            return points;
        }

        private Vector2[] getCorners(Rectangle inRec)
        {
            Vector2[] corners = new Vector2[4];
            corners[0] = new Vector2(inRec.X, inRec.Y);
            corners[1] = new Vector2(inRec.X, inRec.Y + inRec.Height);
            corners[2] = new Vector2(inRec.X + inRec.Width, inRec.Y);
            corners[3] = new Vector2(inRec.X + inRec.Width, inRec.Y + inRec.Height);

            return corners;
        }

        private bool isInRec(Rectangle inRec, Vector2 point)
        {
            if (inRec.X > point.X || point.X > inRec.X + inRec.Width)
                return false;

            if (inRec.Y > point.Y || point.Y > inRec.Y + inRec.Height)
                return false;

            return true;
        }

        protected double measureDistance(Vector2 Point1, Vector2 Point2)
        {
            double angle;
            double distance;
            if (Point1.X == Point2.X)
                return Math.Abs(Point1.Y - Point2.Y);
            if (Point1.Y == Point2.Y)
                return Math.Abs(Point1.X - Point2.X);


            Vector2 trig;
            trig.X = Math.Abs(Point1.X - Point2.X);
            trig.Y = Math.Abs(Point1.Y - Point2.Y);

            angle = Math.Atan2(trig.X, trig.Y);

            distance = trig.X / Math.Sin(angle);

            return distance;
        }

        protected double measureDistance(Vector2 target)// TODO: CHANGE: over load for measure dis
        {
            if (Center.X == target.X)
                return Math.Abs(Center.Y - target.Y);
            if (Center.Y == target.Y)
                return Math.Abs(Center.X - target.X);

            double angle;
            double distance;

            Vector2 trig;
            trig.X = Math.Abs(Center.X - target.X);
            trig.Y = Math.Abs(Center.Y - target.Y);

            angle = Math.Atan2(trig.X, trig.Y);

            distance = trig.X / Math.Sin(angle);

            return distance;
        }

        public void setImage(Texture2D imageChange)
        {
            image = imageChange;
        }



    }
}
