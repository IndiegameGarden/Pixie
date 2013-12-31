
using System;
using Microsoft.Xna.Framework;

using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using TTengine.Comps;

namespace TTengine.Systems
{
    /// <summary>'Motion towards a target' system.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.TargetMotionSystem)]
    public class TargetMotionSystem : EntityComponentProcessingSystem<PositionComp, TargetMotionComp>
    {
        double dt = 0;

        protected override void Begin()
        {
            dt = TimeSpan.FromTicks(EntityWorld.Delta).TotalSeconds;
        }

        public override void Process(Entity entity, PositionComp posComp, TargetMotionComp targetComp)
        {
            targetComp.Target.OnUpdate(dt);
        }
    }

}
