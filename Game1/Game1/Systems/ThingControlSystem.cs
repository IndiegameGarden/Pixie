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
    public class ThingControlSystem : EntityComponentProcessingSystem<ThingComp,ControlComp,TargetMotionComp>
    {
        public override void Process(Entity entity, ThingComp tc, ControlComp tcc, TargetMotionComp targetPosComp)
        {
            tcc.TimeBeforeNextMove -= Dt;

            // check how much push I get. If get pushed, try to move
            if (tcc.PushFromOthers.LengthSquared() > tcc.PushForce)
            {
                tcc.Move = tcc.PushFromOthers;
                tcc.Move.Normalize();
            }
            else
            {
                // if not yet time to make my move, return
                if (tcc.TimeBeforeNextMove > 0)
                    return;
            }

            // reset the countdown timer back to its value
            if (tcc.TimeBeforeNextMove <= 0)
                tcc.TimeBeforeNextMove = tcc.TimeBetweenMoves;

            // if no move to make, return
            if (tcc.Move.LengthSquared() == 0f)
                return;

            // compute new facingDirection from final TargetMove
            tc.FacingDirection = tcc.Move;
            tc.FacingDirection.Normalize();

            // check if passable...
            // FIXME
            /*
            List<Entity> cols = tc.DetectCollisions(entity,tcc.TargetMove);
            if (!tc.IsCollisionFree && cols.Count > 0 && tcc.PushForce > 0f)
            {
                // no - so try to push neighbouring things away
                foreach (Entity t in cols)
                {
                    if (t.HasComponent<ControlComp>())
                    {
                        t.GetComponent<ControlComp>().BePushed(tcc.TargetMove, tcc.PushForce);
                    }
                }
            }

            if (tc.IsCollisionFree || (!tc.CollidesWithBackground(tcc.TargetMove) && cols.Count == 0))
            {
                // yes - passable
                tc.Target += tcc.TargetMove;
                TTutil.Round(ref tc.Target);
            }
             */

            targetPosComp.Target.AddToTarget(tcc.Move);
        }
    }
}
