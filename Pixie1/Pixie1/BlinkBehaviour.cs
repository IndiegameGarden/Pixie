using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;

namespace Pixie1
{
    public class BlinkBehaviour: Gamelet
    {
        float timePeriod, timeOn;

        public BlinkBehaviour(float timePeriod, float dutyCycle)
        {
            this.timePeriod = timePeriod;
            timeOn = dutyCycle * timePeriod;
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            float t = SimTime % timePeriod;
            if (t <= timeOn)
                Parent.Visible = true;
            else
                Parent.Visible = false;
        }
    }
}
