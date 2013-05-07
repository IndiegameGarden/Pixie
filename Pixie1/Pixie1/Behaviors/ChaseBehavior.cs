using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using Microsoft.Xna.Framework;

namespace Pixie1.Behaviors
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
        public Type ChaseTargetType;

        /// <summary>
        /// chase range in pixels
        /// </summary>
        public float ChaseRange = 10.0f;

        /// <summary>
        /// range reached when chaser is satisfied and stops chasing (0 means chase all the way)
        /// </summary>
        public float SatisfiedRange = 0f;

        public ChaseBehavior(Thing chaseTarget)
        {
            this.ChaseTarget = chaseTarget;
        }

        public ChaseBehavior(Type chaseClass)
        {
            this.ChaseTargetType = chaseClass;
        }

        protected override void OnNextMove()
        {
 	        base.OnNextMove();
            Vector2 dif = Vector2.Zero;

            // recheck for nearest chase target
            if (ChaseTargetType != null)
            {
                ChaseTarget = ParentThing.FindNearest(ChaseTargetType);
            }
            
            if (ChaseTarget != null)
            {
                // check for dead chase targets
                if (ChaseTarget.Delete)
                    ChaseTarget = null;
                else                    
                    // compute direction towards chase-target
                    dif = ChaseTarget.Position - ParentThing.Target;
            }

            // choose one direction semi-randomly, if diagonals would be required
            if (dif.X != 0f && dif.Y != 0f)
            {
                float r = RandomMath.RandomUnit();
                // the larger dif.X wrt dif.Y, the smaller the probability of moving in the Y direction
                float thres = Math.Abs(dif.X) / (Math.Abs(dif.X) + Math.Abs(dif.Y));
                if (r > thres)
                    dif.X = 0f;
                else
                    dif.Y = 0f;
            }
            dif.Normalize();
            TargetMove = dif;
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            if (ChaseTarget != null && ChaseTarget.Visible)
            {
                Vector2 dif = ChaseTarget.Position - ParentThing.Target;
                float dist = dif.Length();
                if (dist > 0f && dist <= ChaseRange && dist > SatisfiedRange)
                {
                    // indicate we're chasing
                    IsTargetMoveDefined = true;
                    AllowNextMove();
                }
            }
        }
    }
}
