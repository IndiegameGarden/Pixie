// (c) 2010-2012 TranceTrance.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TTengine.Core;

namespace TTengine.Util
{
    public class PixieMotionBehavior : Motion
    {
        public const float MIN_VELOCITY = 0.5f;

        /// <summary>
        /// sets a target position for cursor to move towards in pixels
        /// </summary>
        public Vector2 Target = Vector2.Zero;

        /// <summary>
        /// speed for moving towards Target
        /// </summary>
        public float TargetSpeed = 0f;

        protected override void OnNewParent()
        {
            base.OnNewParent();
            Motion = Parent.Motion;
            DrawInfo = Parent.DrawInfo;
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            if (TargetSpeed > 0f && !Position.Equals(Target))
            {
                // motion towards target
                Vector2 vDif = (Target - Position);
                Velocity = vDif * TargetSpeed;
                if (Velocity.Length() < MIN_VELOCITY * TargetSpeed)
                    Velocity *= (MIN_VELOCITY * TargetSpeed / Velocity.Length());
                if (vDif.Length() < 0.5f) // TODO const
                {
                    Velocity = Vector2.Zero;
                    Position = Target;
                }
            }
        }

    }
}
