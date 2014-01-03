
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Game1.Comps;

namespace Game1.Systems
{
    /// <summary>'Motion towards a target' system.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.ScrollingSystem)]
    public class ScrollingSystem : EntityComponentProcessingSystem<ScrollingComp>
    {
        public override void Process(Entity entity, ScrollingComp sc)
        {
            /// FIXME
        }
    }

}
