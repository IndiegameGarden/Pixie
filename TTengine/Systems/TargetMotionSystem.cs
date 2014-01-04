
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
        public override void Process(Entity entity, PositionComp posComp, TargetMotionComp targetComp)
        {
            targetComp.Target.OnUpdate(Dt);
            if (targetComp.Target.IsActive)
            {
                posComp.Position = targetComp.Target.Current;
            }
        }
    }

}
