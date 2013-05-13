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
        /// relative force of pushing (strength of unit related). 0f is no pushing force at all but can be pushed.
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
            if (Force < 5f)  // FIXME hack to let pixie not be pushed
                pushFromOthers += dir;
        }

        protected override void OnNextMove()
        {
 	        base.OnNextMove();
 	         	        
        }


        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

			// check if there is push from others. This push can build up over time, only
			// released upon a next move
            float dist = pushFromOthers.Length();
            if (dist > 0f )
            {
                // yes - check if push direction square occupied
                Vector2 dif = pushFromOthers;              

                // choose dominant direction, if diagonals would be required
                if (Math.Abs(dif.X) > Math.Abs(dif.Y))
                    dif.Y = 0f;
                else
                    dif.X = 0f;
                dif.Normalize();
               	
                // if that square is taken, transfer my push to the Thing there with my own Force
                List<Thing> lt = ParentThing.DetectCollisions(dif);
                foreach (Thing t in lt)
                {
                    t.Pushing.BePushed(dif);
                }

                // if the square being pushed to is free, allow the move to go there
                if (lt.Count == 0)
                {                    
                    TargetMove = dif;
                    IsTargetMoveDefined = true;
                    wTime = 0f; // FIXME hack to trigger instant-move
                    AllowNextMove();
                }
            }

            // reset the push buildup
            pushFromOthers = Vector2.Zero;

        }
    }
}
