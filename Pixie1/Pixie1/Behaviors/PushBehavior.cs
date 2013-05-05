using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using Microsoft.Xna.Framework;

namespace Pixie1.Behaviors
{
    /// <summary>
    /// lets a Thing push another Thing and being pushed
    /// </summary>
    public class PushBehavior: ThingControl
    {
        /// <summary>
        /// relative force of pushing (strength of unit related)
        /// </summary>
        public float Force = 1.0f;

        Vector2 pushFromOthers = Vector2.Zero;

        public PushBehavior()
        {
        }

        /// <summary>
        /// receive push force from neighbor Things
        /// </summary>
        /// <param name="dir"></param>
        public void BePushed(Vector2 dir)
        {
            pushFromOthers += dir;
        }

        protected override void OnNextMove()
        {
 	        base.OnNextMove();
            Vector2 dif = TargetMove;
            if (dif.Length() > 0)
            {
                pushFromOthers = Vector2.Zero;
            }
        }


        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            float dist = pushFromOthers.Length();
            if (dist > 0f)
            {
                // check if square occupied
                Vector2 pushFromOthersNorm = pushFromOthers;
                // choose one direction randomly, if diagonals would be required
                if (pushFromOthersNorm.X != 0f && pushFromOthersNorm.Y != 0f)
                {
                    float r = RandomMath.RandomUnit();
                    if (r > 0.5f)
                        pushFromOthersNorm.X = 0f;
                    else
                        pushFromOthersNorm.Y = 0f;
                }
                pushFromOthersNorm.Normalize();
                TargetMove = pushFromOthersNorm;

                List<Thing> lt = ParentThing.DetectCollisions(pushFromOthersNorm);
                if (lt.Count == 0)
                {
                    IsTargetMoveDefined = true;
                    AllowNextMove();
                }
                else
                {
                    foreach (Thing t in lt)
                    {
                        t.Pushing.BePushed(TargetMove * Force);
                    }
                }
            }
        }
    }
}
