using System;
using System.Collections.Generic;
using TTengine.Core;
using Microsoft.Xna.Framework;
using TreeSharp;
using Artemis;
using Game1.Core;
using Game1.Comps;

namespace Game1.Behaviors
{
    /// <summary>
    /// lets a ThingComp chase another ThingComp when it's visible.
    /// </summary>
    public class ChaseBehavior: Behavior
    {
        /// <summary>followed target of this chase behavior</summary>
        public Entity ChaseTarget;

        /// <summary>chase range in pixels</summary>
        public float ChaseRange = 10.0f;

        /// <summary>range reached when chaser is satisfied and stops chasing (0 means chase all the way)</summary>
        public float SatisfiedRange = 0f;

        /// <summary>if true, inverts the Chase into an Avoid behavior</summary>
        public bool Avoidance = false;

        public ChaseBehavior(Entity chaseTarget)
        {
            this.ChaseTarget = chaseTarget;
        }

        public override IEnumerable<RunStatus> Execute(object context)
        {
            var ctx = context as BTAIContext;
            var tc = ctx.Entity.GetComponent<ThingComp>();

            if (ChaseTarget != null && ChaseTarget.IsActive)
            {
                var targetTc = ChaseTarget.GetComponent<ThingComp>();
                Vector2 dif;
                if (targetTc.Visible)
                {
                    dif = targetTc.Position - tc.Target;
                    float dist = dif.Length();
                    if (dist > 0f && dist <= ChaseRange && dist > SatisfiedRange)
                    {
                        // compute direction towards chase-target
                        dif = targetTc.Position - tc.Target;
                        if (Avoidance)
                            dif = -dif;
                        var tcc = ctx.Entity.GetComponent<ThingControlComp>();
                        tcc.TargetMove = dif;
                        tcc.DeltaTimeBetweenMoves = this.DeltaTimeBetweenMoves;
                        yield return RunStatus.Success;
                    }
                }
            }
            yield return RunStatus.Failure;
        }

    }
}
