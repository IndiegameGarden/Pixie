using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Artemis;
using Artemis.System;
using Artemis.Manager;
using Artemis.Attributes;
using Game1.Comps;
using TTengine.Comps;
using TTengine.Core;

namespace Game1.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 3)]
    public class RandomWanderSystem : EntityComponentProcessingSystem<ThingComp,RandomWanderComp>
    {
        double dt = 0;

        protected override void Begin()
        {        
            dt = TimeSpan.FromTicks(EntityWorld.Delta).TotalSeconds;
        }

        public override void Process(Entity entity, ThingComp tc, RandomWanderComp rwc)
        {
            if (!rwc.IsActive) return;
            rwc.UpdateComp(dt);
            rwc.CurrentDirectionChangeTime -= dt;
            Vector2 dir = rwc.CurrentDirection;
            if (dir.Length() < 0.1f)
                dir = Vector2.Zero;
            rwc.TargetMove = dir;

            // direction changing after some random time
            if (rwc.CurrentDirectionChangeTime <= 0)
            {
                // TODO double version of randombetween
                rwc.CurrentDirectionChangeTime = RandomMath.RandomBetween((float)rwc.MinDirectionChangeTime, (float)rwc.MaxDirectionChangeTime);
                rwc.CurrentDirection = RandomMath.RandomDirection();
            }

        }
    }
}
