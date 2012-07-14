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
    public class PixieSpritelet: Spritelet
    {
        /// <summary>
        /// centre of screen viewing pos in pixels for all PixieSpritelets
        /// </summary>
        public static Vector2 ViewPos = Vector2.Zero;

        /// <summary>
        /// position in the level, in pixels
        /// </summary>
        public Vector2 Position = Vector2.Zero;

        public Vector2 Target = Vector2.Zero;

        public Vector2 PositionAndTarget
        {
            set
            {
                Position = value;
                Target = value;
            }
        }

        /// <summary>
        /// a relative to normal velocity of moving scale factor
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

        public PixieSpritelet(string bitmapFile)
            : base(bitmapFile)
        {
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            Motion.Position = Screen.Center + Motion.ScaleAbs * (  FromPixels( Position - ViewPos)); // TODO ViewPos smoothing using Draw cache
            //Motion.Position = Position - ViewPos;

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
