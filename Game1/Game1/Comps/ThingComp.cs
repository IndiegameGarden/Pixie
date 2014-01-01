//
// Method IntersectPixels used under Microsoft Permissive License (Ms-PL). (http://create.msdn.com/downloads/?id=15)
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using TTengine.Comps;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1.Core;
using Game1.Behaviors;
using Game1.Actors;
using TreeSharp;
using Artemis;
using Artemis.Interface;

namespace Game1.Comps
{
    public enum Faction { GOOD, EVIL, NEUTRAL };

    public enum ThingType { HERO, COMPANION, RED_GUARD, SERVANT, BOSS, OTHER };

    public class ThingComp: IComponent
    {
        public bool Active = true;

        public bool Visible = true;

        public Color Color = Color.White;

        public Faction Faction = Faction.NEUTRAL;

        public ThingType Type;

        /// <summary>
        /// if true can pass anything without colliding
        /// </summary>
        public bool IsCollisionFree = true;

        /// <summary>
        /// Determines what intensity levels of background pixel color this ThingComp can pass.
        /// Intensity is the sum of R,G,B bytes of pixel. Any background pixel at threshold value or
        /// brighter is passable for this ThingComp.
        /// </summary>
        public int PassableIntensityThreshold;

        /// <summary>
        /// position in the level, in pixels, in sub-pixel resolution
        /// </summary>
        public Vector2 Position = Vector2.Zero;

        /// <summary>
        /// Position.X as a rounded integer
        /// </summary>
        public int PositionX
        {
            get { return (int)Math.Round(Position.X); }
            set { Position.X = (float)value; }
        }

        /// <summary>
        /// Position.Y as a rounded integer
        /// </summary>
        public int PositionY
        {
            get { return (int)Math.Round(Position.Y); }
            set { Position.Y = (float)value; }
        }

        /// <summary>
        /// Target.X as a rounded integer
        /// </summary>
        public int TargetX
        {
            get { return (int)Math.Round(Target.X); }
            set { Target.X = (float)value; }
        }

        /// <summary>
        /// Target.Y as a rounded integer
        /// </summary>
        public int TargetY
        {
            get { return (int)Math.Round(Target.Y); }
            set { Target.Y = (float)value; }
        }

        /// <summary>
        /// position in the level, in pixels, where the entity's Position should move towards in a smooth fashion
        /// </summary>
        public Vector2 Target = Vector2.Zero;

        /// <summary>
        /// a direction (if any) the Thing is facing towards e.g. up (0,-1), down (0,1) or right (1,0).
        /// </summary>
        public Vector2 FacingDirection = new Vector2(1f, 0f);

        /// <summary>
        /// to set both Position and Target in one go
        /// </summary>
        public Vector2 PositionAndTarget
        {
            set
            {
                Position = value;
                Target = value;
                TTutil.Round(ref Position);
                TTutil.Round(ref Target);
            }
        }

        private Rectangle boundingRectangle = new Rectangle();
        private LevelBackgroundComp bg;

        /// <summary>
        /// the bounding rectangle of the sprite of this Thing, based on Position
        /// </summary>
        public Rectangle BoundingRectangle
        {
            get {
                boundingRectangle.X = PositionX; 
                boundingRectangle.Y = PositionY; 
                return boundingRectangle; 
            }
        }

        /// <summary>
        /// a 'relative to normal' velocity-of-moving factor i.e. 1f == normal velocity
        /// </summary>
        public double Velocity = 1;

        /// <summary>
        /// relative speed of the smooth motion for moving towards Target. Linear speed defined for Velocity==1.
        /// </summary>
        public double TargetSpeed = 10;

        /// <summary>
        /// the Toy that is carried by and active for this Thing, or null if none
        /// </summary>
        public Toy ToyActive = null;

        // used for the collision detection per-pixel
        protected Color[] textureData;

        public ThingComp(ThingType type, LevelBackgroundComp bgComp, Texture2D collisionTexture)
        {
            this.Type = type;
            this.bg = bgComp;
            this.boundingRectangle.Width = collisionTexture.Width;
            this.boundingRectangle.Height = collisionTexture.Height;

            // TODO needed to fetch text.data per entity? only for those that may change its shape/texture during play?
            textureData = new Color[collisionTexture.Width * collisionTexture.Height];
            collisionTexture.GetData(textureData);

        }

        /// <summary>
        /// detect all collisions with other collidable Things (that have IsCollisionFree=false set)
        /// </summary>
        /// <param name="myPotentialMove">a potential move of this ThingComp, collision checked after applying potential move.</param>
        /// <returns></returns>
        public List<Entity> DetectCollisions(Entity thing,Vector2 myPotentialMove)
        {
            List<Entity> l = new List<Entity>();
            var b = thing.entityWorld.EntityManager.GetEntities(Aspect.All(typeof(ThingComp)));
            foreach (Entity e in b)
            {
                var tc = e.GetComponent<ThingComp>();
                if (tc == this) continue;
                if (!tc.Active) continue;
                if (CollidesWhenThisMoves(tc,myPotentialMove))
                {
                    l.Add(e);
                }
            }
            return l;
        }

        public Entity FindNearest(Entity thing, ThingType thingType)
        {
            Entity foundThing = null;
            float bestDist = 99999999f;
            var b = thing.entityWorld.EntityManager.GetEntities(Aspect.All(typeof(ThingComp)));
            foreach (Entity e in b)
            {
                var tc = e.GetComponent<ThingComp>();
                if (tc == this) continue;
                if (!tc.Active) continue;
                if (tc.Type == thingType)
                {
                    float dist = (tc.Position - this.Position).Length();
                    if (dist < bestDist)
                    {
                        bestDist = dist;
                        foundThing = e;
                    }
                }
            }
            return foundThing;
        }

        public bool Collides(ThingComp other)
        {
            return IntersectPixels(BoundingRectangle, textureData, other.BoundingRectangle, other.textureData );
        }

        public bool CollidesWhenThisMoves(ThingComp other, Vector2 myPotentialMove)
        {
            Rectangle rectNow = BoundingRectangle;
            Rectangle rectMoved = new Rectangle(rectNow.X + (int) Math.Round(myPotentialMove.X) ,
                                            rectNow.Y + (int) Math.Round(myPotentialMove.Y) ,
                                            rectNow.Width,rectNow.Height);
            return IntersectPixels(rectMoved, textureData, other.BoundingRectangle, other.textureData);
        }

        public bool CollidesWhenOtherMoves(ThingComp other, Vector2 othersPotentialMove)
        {
            Rectangle rectOther = other.BoundingRectangle;
            Rectangle rectMoved = new Rectangle(rectOther.X + (int)Math.Round(othersPotentialMove.X),
                                            rectOther.Y + (int)Math.Round(othersPotentialMove.Y),
                                            rectOther.Width, rectOther.Height);
            return IntersectPixels(BoundingRectangle, textureData, rectMoved, other.textureData);
        }

        /// <summary>
        /// check whether this ThingComp would collide with anything (ThingComp or background), when attempting
        /// to do a given move - TODO factor out this method? Use DetectCollisions
        /// </summary>
        /// <param name="potentialMove">the move to attempt for this ThingComp</param>
        /// <returns>true if ThingComp would collide with something after the potentialMove would have been made, false otherwise</returns>
        public bool CollidesWithSomething(Entity entity, Vector2 potentialMove)
        {
            if (CollidesWithBackground(potentialMove))
                return true;
            List<Entity> lt = DetectCollisions(entity,potentialMove);
            return  lt.Count > 0 ;
        }

        /// <summary>
        /// check whether this ThingComp would collide with background, when attempting to do a given potential move
        /// </summary>
        /// <param name="potentialMove">the move vector, delta from current Target, that we would like to apply</param>
        /// <returns>true if ThingComp would collide with background after a potentialMove would have been made, false otherwise</returns>
        public bool CollidesWithBackground(Vector2 potentialMove)
        {
            int posX = TargetX + (int)Math.Round(potentialMove.X);
            int posY = TargetY + (int)Math.Round(potentialMove.Y);
            if (posX < 0) 
                return true;
            if (posY < 0) 
                return true;
            Rectangle bgSampleRect = new Rectangle(posX, posY, BoundingRectangle.Width, BoundingRectangle.Height);
            Rectangle thingRect = new Rectangle(posX, posY, BoundingRectangle.Width, BoundingRectangle.Height);
            if (bgSampleRect.Right >= bg.Texture.Width)
                return true;
            if (bgSampleRect.Bottom >= bg.Texture.Height)
                return true;
            int N = bgSampleRect.Width * bgSampleRect.Height;
            Color[] bgTextureData = new Color[N];
            bg.Texture.GetData<Color>(0, bgSampleRect, bgTextureData, 0, N);
            return IntersectPixelsBg(thingRect, textureData, bgSampleRect, bgTextureData);
        }

        /// <summary>
        /// Determines if there is overlap of the non-transparent pixels
        /// between two sprites.
        /// Method code from http://create.msdn.com/en-US/education/catalog/tutorial/collision_2d_perpixel
        /// used under Microsoft Permissive License (Ms-PL). 
        /// </summary>
        /// <param name="rectangleA">Bounding rectangle of the first sprite</param>
        /// <param name="dataA">Pixel data of the first sprite</param>
        /// <param name="rectangleB">Bouding rectangle of the second sprite</param>
        /// <param name="dataB">Pixel data of the second sprite</param>
        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
        static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
                                    Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }

        /// <summary>
        /// Determines if there is overlap of the non-transparent pixels
        /// of a sprite with non-passable colored pixels in the background.
        /// Derived from above IntersectPixels() method.
        /// </summary>
        /// <param name="rectangleA">Bounding rectangle of the sprite</param>
        /// <param name="dataA">Pixel data of the sprite</param>
        /// <param name="rectangleB">Bouding rectangle of the background snapshot</param>
        /// <param name="dataB">Pixel data of the background snapshot</param>
        /// <returns>True if collision with background; false otherwise</returns>
        bool IntersectPixelsBg(Rectangle rectangleA, Color[] dataA,
                                    Rectangle rectangleB, Color[] dataB)
        {
           

            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If pixel A not completely transparent,
                    // and BG pixel non-passable (by comparing intensity to threshold),
                    if (colorA.A != 0 && (colorB.R + colorB.G + colorB.B) < PassableIntensityThreshold )
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }


    }
}
