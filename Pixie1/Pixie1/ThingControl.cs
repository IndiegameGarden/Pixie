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
        /// References the Thing that is controlled
        /// </summary>
        public Thing ParentThing = null;

        /// <summary>
        /// flag indicating whether TargetMove is valid (true) for this round or undefined (false)
        /// </summary>
        public bool IsTargetMoveDefined = false;

        /// <summary>
        /// Every update cycle the move produced by this control, if any, is written to TargetMove
        /// </summary>
        public Vector2 TargetMove = new Vector2();
        public Vector2 TargetMoveMultiplier = new Vector2(1f, 1f);

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
