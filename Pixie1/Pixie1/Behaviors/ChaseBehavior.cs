using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using Microsoft.Xna.Framework;

namespace Pixie1
{
    /// <summary>
    /// lets a Thing chase another Thing when it's visible.
    /// </summary>
    public class ChaseBehavior: ThingControl
    {
        /// <summary>
        /// followed target of this chase behavior
        /// </summary>
        public Thing ChaseTarget;

        /// <summary>
        /// relative speed of chasing target
        /// </summary>
        public float ChaseSpeed = 1.0f;

        /// <summary>
        /// chase range in pixels
        /// </summary>
        public float ChaseRange = 10.0f;

        // waiting time before a next move is taken
        float wTime = 0f;

        public ChaseBehavior(Thing chaseTarget)
        {
            this.ChaseTarget = chaseTarget;
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            wTime += p.Dt;
            if (wTime >= 0.2f / ChaseSpeed ) 
                wTime = 0f;
            if (wTime == 0f && ChaseTarget.Visible)
            {
                Vector2 dif = ChaseTarget.Position - ParentThing.Target;
                float dist = dif.Length();
                if (dist > 0f && dist <= ChaseRange )
                {
                    // choose one direction randomly, if diagonals are requested
                    if (dif.X != 0f && dif.Y != 0f)
                    {
                        float r = RandomMath.RandomUnit();
                        if (r > 0.5f)
                            dif.X = 0f;
                        else
                            dif.Y = 0f;
                    }
                    dif.Normalize();
                    ParentThing.TargetMove += dif;
                }
            }
        }
    }
}
