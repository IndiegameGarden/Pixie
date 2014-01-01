using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TTengine.Core;
using TTengine.Util;
using TreeSharp;
using Game1.Core;
using Game1.Comps;

namespace Game1.Behaviors
{
    public class RandomWanderBehavior: Behavior
    {

        public override IEnumerable<RunStatus> Execute(object context)
        {
            BTAIContext ctx = context as BTAIContext;
            
            // enable, or keep alive, the random wander comp. 
            var rwc = ctx.Entity.GetComponent<RandomWanderComp>();
            ctx.BTComp.EnableComp(rwc);

            // take the computed move and apply to the move of this Thing
            var tcc = ctx.Entity.GetComponent<ThingControlComp>();
            tcc.TargetMove = rwc.TargetMove;
            tcc.DeltaTimeBetweenMoves = this.DeltaTimeBetweenMoves;
            yield return RunStatus.Success;
        }

    }    
}
