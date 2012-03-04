using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;

namespace Pixie1
{
    public class Toy: PixieSpritelet
    {
        float cyclePeriod;

        public Toy()
            : base("pixie")
        {
            cyclePeriod = RandomMath.RandomBetween(0.2f, 1.82f);
            ColorCycleBehavior cycl = new ColorCycleBehavior(cyclePeriod);
            cycl.minR = RandomMath.RandomBetween(0.3f, 0.5734f); ;
            cycl.maxR = 0.9434f;
            cycl.minG = 0f;
            cycl.maxG = RandomMath.RandomBetween(0.7f, 1f); ;
            cycl.minB = RandomMath.RandomBetween(0.1f, 0.54f); ;
            cycl.maxB = 0.9f;
            cycl.timePeriodR = cyclePeriod * RandomMath.RandomBetween(1.02f, 1.537f); ;
            cycl.timePeriodG = cyclePeriod * RandomMath.RandomBetween(0.7f, 0.93f); ;
            Add(cycl);
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            //MotionB.Target = TargetPos; // FIXME mappin to screen
            //MotionB.TargetSpeed = 1f;
            Motion.RotateModifier += SimTime;
        }
    }
}
