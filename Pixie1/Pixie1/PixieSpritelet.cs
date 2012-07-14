using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pixie1
{
    /**
     * base class for any visible thing in the Pixie universe
     */
    public class PixieSpritelet: Spritelet
    {
        /// <summary>
        /// if true can pass anything
        /// </summary>
        public bool IsGodMode = false;

        /// <summary>
        /// centre of screen viewing pos in pixels for ALL PixieSpritelets
        /// </summary>
        public static Vector2 ViewPos = Vector2.Zero;

        /// <summary>
        /// position in the level, in pixels
        /// </summary>
        public Vector2 Position = Vector2.Zero;

        /// <summary>
        /// position in the level, in pixels, where the entity's Position should move towards
        /// </summary>
        public Vector2 Target = Vector2.Zero;

        /// <summary>
        /// to set both Position and Target in one go
        /// </summary>
        public Vector2 PositionAndTarget
        {
            set
            {
                Position = value;
                Target = value;
            }
        }

        /// <summary>
        /// a 'relative to normal' velocity-of-moving factor i.e. 1f == normal velocity
        /// </summary>
        public float Velocity = 1f;

        /// <summary>
        /// relative speed for moving towards Target. Is calculated linearly based on distance to target.
        /// </summary>
        public float TargetSpeed = 1f;

        /// <summary>
        /// minimum ABSOLUTE speed to move towards target in pixels/sec
        /// </summary>
        public float TargetSpeedMin = 5f;

        /// <summary>
        /// create a single-pixel PixieSpritelet
        /// </summary>
        public PixieSpritelet()
            : base("pixie")
        {
        }

        /// <summary>
        /// create a PixieSpritelet from arbitrary bitmap shape
        /// </summary>
        /// <param name="bitmapFile">content graphics file</param>
        public PixieSpritelet(string bitmapFile)
            : base(bitmapFile)
        {
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            Motion.Position = Screen.Center + Motion.ScaleAbs * (  FromPixels( Position - ViewPos)); // TODO ViewPos smoothing using Draw cache
            //Motion.Position = Position - ViewPos;

            // sum all intended moves from PixieControls attached
            Vector2 TargetMove = Vector2.Zero;
            foreach (Gamelet g in Children)
            {
                if (g is PixieControl) {
                    PixieControl c = g as PixieControl;
                    TargetMove += c.TargetMove;
                }
            }
            // take steering inputs if any, and move pixie
            if (TargetMove.Length() > 0f)
            {
                Vector2 newPos = Target + TargetMove;
                bool isWalkable = Level.Current.CanPass(newPos) || IsGodMode;
                if (isWalkable)
                    Target += TargetMove;
            }
            TTutil.Round(Target);

            Vector2 vdif = Target - Position;
            if (vdif.LengthSquared() > 0f) // if target not reached yet
            {
                Vector2 vmove = vdif;
                vmove *= TargetSpeed ;
                // check minimum speed of moving
                if (vmove.Length() < TargetSpeedMin)
                {
                    vmove.Normalize();
                    vmove *= TargetSpeedMin;
                }
                // convert speed vector to move vector (x = v * t)
                vmove *= p.Dt;
                // check if target reached already (i.e. move would overshoot target)
                if (vmove.LengthSquared() >= vdif.LengthSquared())
                {
                    Position = Target;
                }
                else
                {
                    // apply move towards target
                    Position += vmove;
                }
            }
        }

    }
}
