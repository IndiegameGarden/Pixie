using System;
using System.Collections.Generic;
using TTengine.Core;
using Microsoft.Xna.Framework;
using Game1.Core;
using Game1.Actors;
using Game1.Comps;
using TTengine.Util;
using TreeSharp;
using Artemis;

namespace Game1.Behaviors
{
    /// <summary>
    /// Attack any Thing that is in another faction than mine and not NEUTRAL.
    /// </summary>
    public class AttackEnemyBehavior: Behavior
    {
        public bool IsAttacking = false;

        public string[] AttackStrings;

        public AttackEnemyBehavior(string[] attackStrings)
        {
            this.AttackStrings = attackStrings;
        }

        public override IEnumerable<RunStatus> Execute(object context)
        {
            BTAIContext ctx = context as BTAIContext;
            var tc = ctx.Entity.GetComponent<ThingComp>();
            var tcc = ctx.Entity.GetComponent<ThingControlComp>();

            // only attack if not blocked there.
            List<Entity> col = tc.DetectCollisions(ctx.Entity,tc.FacingDirection);
            foreach(Entity e in col) 
            {
                var tc2 = e.GetComponent<ThingComp>();
                if (tc2.Faction != tc.Faction && tc2.Faction != Faction.NEUTRAL)
                {
                    IsAttacking = true;
                    tcc.TargetMove = tc.FacingDirection;
                    tcc.DeltaTimeBetweenMoves = this.DeltaTimeBetweenMoves;

                    // TODO color set! and health decrease values parameterize.
                    Level.Current.Subtitles.Show(3, AttackStrings[RandomMath.RandomIntBetween(0, AttackStrings.Length - 1)], 3.5f, tc.Color);
                    ctx.Entity.GetComponent<HealthComp>().DecreaseHealth( RandomMath.RandomBetween(1f, 3f) );
                    yield return RunStatus.Success;
                    yield break;
                }
            }
            yield return RunStatus.Failure;
        }

    }
}
