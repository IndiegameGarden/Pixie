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
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.ThingSystem)]
    public class ThingSystem : EntityComponentProcessingSystem<ThingComp,PositionComp>
    {
        public override void Process(Entity entity, ThingComp tc, PositionComp pc)
        {
        }
    }
}
