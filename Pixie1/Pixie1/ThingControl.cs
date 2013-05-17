using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using Microsoft.Xna.Framework;
using TTengine.Util;

namespace Pixie1
{
    /**
     * A script or behavior that controls a Thing
     */
    public class ThingControl: Gamelet
    {
        /// <summary>
        /// relative speed of execution (1.0 is normal)
        /// </summary>
        public float MoveSpeed = 1.0f;

        /// <summary>
        /// References the Thing that is controlled
        /// </summary>
        public Thing ParentThing = null;

        /// <summary>
        /// flag indicating whether TargetMove and TargetMoveMultiplier are valid (true) for this round or undefined (false)
        /// </summary>
        public bool IsTargetMoveDefined = false;

        /// <summary>
        /// flag indicating whether a previously generated TargetMove is still active, or not (active until a next move calculation)
        /// </summary>
        public bool IsMoveActive = false;

        /// <summary>
        /// Every update cycle the move produced by this control, if any, is written to TargetMove
        /// </summary>
        public Vector2 TargetMove = new Vector2();
        public Vector2 TargetMoveMultiplier = new Vector2(1f, 1f);

        /// <summary>
        /// the TargetMove of the previous round
        /// </summary>
        public Vector2 LastTargetMove = new Vector2();

        // timer that fires when next move is to be computed
        protected float wTime = RandomMath.RandomBetween(0f,0.2f); 

        protected override void OnNewParent()
        {
            base.OnNewParent();
            ParentThing = GetParentThing();
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            IsTargetMoveDefined = false;
            TargetMove = Vector2.Zero;

            wTime -= p.Dt;

            if (wTime <= 0f )   // timer triggers
            {
                // do my move                
                OnNextMove();
                wTime += 0.2f / MoveSpeed;

                // if no targetmove defined, clear the 'lock'
                IsMoveActive = IsTargetMoveDefined;
            }
        }
        
        /// <summary>
        /// called from OnUpdate() when it's time to execute a next move 
        /// </summary>
        protected virtual void OnNextMove()
        {
        }

        protected Thing GetParentThing()
        {
            if (Parent is Thing)
                return Parent as Thing;
            else if (Parent is ThingControl)
                return (Parent as ThingControl).GetParentThing();
            return null;
        }
    }
}
