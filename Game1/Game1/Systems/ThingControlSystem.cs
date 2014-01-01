using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis;
using Artemis.System;
using Artemis.Manager;
using Artemis.Attributes;
using Game1.Comps;
using TTengine.Comps;
using TTengine.Util;

namespace Game1.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 3)]
    public class ThingControlSystem : EntityComponentProcessingSystem<ThingComp,ThingControlComp>
    {
        double dt = 0;

        protected override void Begin()
        {
            dt = TimeSpan.FromTicks(EntityWorld.Delta).TotalSeconds;
        }

        public override void Process(Entity entity, ThingComp tc, ThingControlComp tcc)
        {
            tcc.TimeBeforeNextMove -= dt;

            // check how much push I get. If get pushed, try to move
            if (tcc.PushFromOthers.LengthSquared() > tcc.PushingForce)
            {
                tcc.TargetMove = tcc.PushFromOthers;
                tcc.TargetMove.Normalize();
            }
            else
            {
                // if not yet time to make my move, return
                if (tcc.TimeBeforeNextMove > 0)
                    return;
            }

            // reset the countdown timer back to its value
            if (tcc.TimeBeforeNextMove <= 0)
                tcc.TimeBeforeNextMove = tcc.DeltaTimeBetweenMoves;

            // if no move to make, return
            if (tcc.TargetMove.LengthSquared() == 0f)
                return;

            // compute new facingDirection from final TargetMove
            tc.FacingDirection = tcc.TargetMove;
            tc.FacingDirection.Normalize();

            // check if passable...
            List<Entity> cols = tc.DetectCollisions(entity,tcc.TargetMove);
            if (!tc.IsCollisionFree && cols.Count > 0 && tcc.PushingForce > 0f)
            {
                // no - so try to push neighbouring things away
                foreach (Entity t in cols)
                {
                    if (t.HasComponent<ThingControlComp>())
                    {
                        t.GetComponent<ThingControlComp>().BePushed(tcc.TargetMove, tcc.PushingForce);
                    }
                }
            }

            if (tc.IsCollisionFree || (!tc.CollidesWithBackground(tcc.TargetMove) && cols.Count == 0))
            {
                // yes - passable
                tc.Target += tcc.TargetMove;
                TTutil.Round(ref tc.Target);
            }

        }
    }
}
