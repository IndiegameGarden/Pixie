using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;

namespace Pixie1
{
    public class BadPixel: PixieSpritelet
    {
        
        public BadPixel()
            : base("pixie")
        {
            DrawInfo.DrawColor = new Color(255, 10, 4);
            float blinkPeriod = RandomMath.RandomBetween(0.3f, 1.12f);
            float blinkDutycycle = RandomMath.RandomBetween(0.1f, 0.95f);
            Add(new BlinkBehaviour(blinkPeriod,blinkDutycycle));
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            //MotionB.Target = TargetPos; // FIXME mappin to screen
            //MotionB.TargetSpeed = 1f;
        }
    }
}
