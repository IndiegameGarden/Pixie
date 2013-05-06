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
        Vector2 pushFromOthersRemainder = Vector2.Zero;

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

						// transfer collected push forces until now into 'remainder' variable. Reset to restart collecting for next round.
						pushFromOthersRemainder = pushFromOthers;
						pushFromOthers = Vector2.Zero;
						
            float dist = pushFromOthersRemainder.Length();
            if (dist > 0f)
            {
                // check if square occupied
                Vector2 dif = pushFromOthersRemainder;
                // choose one direction randomly, if diagonals would be required
                if (dif.X != 0f && dif.Y != 0f)
                {
                    float r = RandomMath.RandomUnit();
                    if (r > 0.5f)
                        dif.X = 0f;
                    else
                        dif.Y = 0f;
                }
                dif.Normalize();                

								// if the square being pushed to is free, allow move to go there
                List<Thing> lt = ParentThing.DetectCollisions(dif);
                if (lt.Count == 0)
                {
                		TargetMove = dif;
                    IsTargetMoveDefined = true;
                    AllowNextMove();
                }
                else
                {
                		// if square is taken, transfer my push to the Thing there with my own Force
                    foreach (Thing t in lt)
                    {
                        t.Pushing.BePushed(dif * Force);
                    }
                }
            }
        }
    }
}
