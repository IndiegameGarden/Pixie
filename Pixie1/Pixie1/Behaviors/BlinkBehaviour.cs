using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;

namespace Pixie1.Behaviors
{
    public class BlinkBehavior: Gamelet
    {
        /// <summary>
        /// the length of a single blink period
        /// </summary>
        public float TimePeriod;

        /// <summary>
        /// the fraction 0...1 of time that the blinking thing is visible
        /// </summary>
        public float DutyCycle
        {
            get
            {
                return dutyCycle;
            }
            set
            {
                dutyCycle = value;
                timeOn = value * TimePeriod;
            }
        }

        protected float timeOn, dutyCycle;

        public BlinkBehavior(float timePeriod, float dutyCycle)
        {
            this.TimePeriod = timePeriod;
            DutyCycle = dutyCycle;
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            float t = SimTime % TimePeriod;
            if (t <= timeOn)
                Parent.Visible = true;
            else
                Parent.Visible = false;
        }
    }
}
