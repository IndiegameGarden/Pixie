using System;
using System.Collections.Generic;
using TTengine.Core;
using Microsoft.Xna.Framework;

namespace Pixie1.Behaviors
{
    /**
     * when engaged in close proximity to an enemy, plays sounds (and maybe do hitpoints etc)
     */
    public class CombatBehavior: ThingControl
    {
        public Type EnemyType;
        public bool IsCombat = false;
        public bool WasCombat = false;

        public CombatBehavior(Type enemyType)
        {
            EnemyType = enemyType;
        }

        protected override void OnNextMove() 
        {
            base.OnNextMove();

            // check for enemy facing
            List<Thing> facing = ParentThing.DetectCollisions(ParentThing.FacingDirection);
            WasCombat = IsCombat;
            IsCombat = false;
            foreach(Thing t in facing) 
            {
                if (t.GetType() == EnemyType)
                {
                    IsCombat = true;
                }
            }

            if (IsCombat)
            {
                if (WasCombat == false || RandomMath.RandomUnit() < 0.08f)
                    Level.Current.Sound.PlayRandomCombatSound(0.23f, 0.4f);
            }
        }

    }
}
