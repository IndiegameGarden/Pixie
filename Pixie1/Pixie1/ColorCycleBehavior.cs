using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using Microsoft.Xna.Framework;

namespace Pixie1
{
    public class ColorCycleBehavior: Gamelet
    {
        public float timePeriod;
        public float timePeriodR, timePeriodG, timePeriodB;
        public float minB, maxB, minG, maxG, minR, maxR;

        public ColorCycleBehavior(float timePeriod)
        {
            this.timePeriod = timePeriod;
            timePeriodR = timePeriod;
            timePeriodG = timePeriod;
            timePeriodB = timePeriod;
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            float t = 2 * (SimTime % timePeriod); // TODO SimTime is not the time related to the Draw!
            if (t > timePeriod ) // gen sawtooth wave
                t = 2*timePeriod - t;
            Color col = new Color((t / timePeriodR) * (maxR - minR) + minR, (t / timePeriodG) * (maxG - minG) + minG, (t / timePeriodB) * (maxB - minB) + minB);
            Parent.DrawInfo.DrawColor = col;
            
        }

    }
}
