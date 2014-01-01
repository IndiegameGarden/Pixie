using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Artemis;
using Artemis.System;
using Artemis.Manager;
using Artemis.Attributes;
using Game1.Comps;
using TTengine.Comps;

namespace Game1.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 3)]
    public class ThingSystem : EntityComponentProcessingSystem<ThingComp,PositionComp>
    {
        protected double dt = 0;

        protected override void Begin()
        {
            dt = TimeSpan.FromTicks(EntityWorld.Delta).TotalSeconds;
        }

        public override void Process(Entity entity, ThingComp tc, PositionComp pc)
        {
            // Smooth Movement of Thing towards Target
            Vector2 vdif = tc.Target - tc.Position;
            if (vdif.LengthSquared() > 0f) // if target not reached yet
            {
                Vector2 vmove = vdif;
                vmove.Normalize();
                vmove *= (float)tc.TargetSpeed * (float)tc.Velocity ;
                // convert speed vector to move vector (x = v * t)
                vmove *= (float)dt;
                // check if target reached already (i.e. move would overshoot target)
                if (vmove.LengthSquared() >= vdif.LengthSquared())
                {
                    tc.Position = tc.Target;
                }
                else
                {
                    // apply move towards target
                    tc.Position += vmove;
                }
            }

            // set position in PositionComp
            pc.Position = tc.Position;
        }
    }
}
