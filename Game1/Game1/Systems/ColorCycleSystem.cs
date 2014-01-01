using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis;
using Artemis.System;
using Artemis.Manager;
using Artemis.Attributes;
using Game1.Comps;
using TTengine.Comps;
using Microsoft.Xna.Framework;

namespace Game1.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = 4)]
    public class ColorCycleSystem : EntityComponentProcessingSystem<ColorCycleComp>
    {
        double dt;

        protected override void Begin()
        {
            dt = TimeSpan.FromTicks(EntityWorld.Delta).TotalSeconds;
        }

        public override void Process(Entity entity, ColorCycleComp comp)
        {
            comp.UpdateComp(dt);

            double t = 2 * (comp.SimTime % comp.timePeriod); // TODO SimTime is not the time related to the Draw!
            if (t > comp.timePeriod) // gen sawtooth wave
                t = 2 * comp.timePeriod - t;
            Color col = new Color((int)((t / comp.timePeriodR) * (comp.maxColor.R - comp.minColor.R) + comp.minColor.R),
                                   (int)((t / comp.timePeriodG) * (comp.maxColor.G - comp.minColor.G) + comp.minColor.G),
                                   (int)((t / comp.timePeriodB) * (comp.maxColor.B - comp.minColor.B) + comp.minColor.B),
                                   (int)((t / comp.timePeriodA) * (comp.maxColor.A - comp.minColor.A) + comp.minColor.A)
                                 );
            entity.GetComponent<DrawComp>().DrawColor = col;

        }
    }
}
