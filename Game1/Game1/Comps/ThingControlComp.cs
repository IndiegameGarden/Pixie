using System;
using System.Collections.Generic;
using TTengine.Core;
using TTengine.Comps;
using TTengine.Util;
using Microsoft.Xna.Framework;
using TreeSharp;
using Game1.Comps;

namespace Game1
{
    /// <summary>
    /// The ability to control moves of a ThingComp, either by user input or from AI
    /// </summary>
    public class ThingControlComp: Comp
    {
        public Vector2 TargetMove = new Vector2();

        public double PushingForce = 0;

        public Vector2 TargetMoveMultiplier = new Vector2(1f, 1f);

        public double DeltaTimeBetweenMoves = 0.2;

        public double TimeBeforeNextMove = 0;

        public Vector2 PushFromOthers = Vector2.Zero;
        //public Vector2 pushFromOthersRemainder = Vector2.Zero;

        /// <summary>
        /// receive push force from neighbor Things
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="pushingForce"></param>
        public void BePushed(Vector2 dir, double pushingForce)
        {
            PushFromOthers += dir * (float)pushingForce;
        }


        public ThingControlComp()
        {
        }

    }
}
