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
        
        public BadPixel(bool reverseBlinker, PixieSpritelet chaseTarget)
            : base("pixie")
        {
            DrawInfo.DrawColor = new Color(255, 10, 4);
            float blinkPeriod = RandomMath.RandomBetween(0.64f, 2.12f);
            float blinkDutycycle = RandomMath.RandomBetween(0.4f, 0.95f);
            if (reverseBlinker)
            {
                blinkDutycycle = 1f - blinkDutycycle;
            }
            Add(new BlinkBehaviour(blinkPeriod,blinkDutycycle));

            Add(new ChaseBehavior(chaseTarget));
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            //MotionB.Target = TargetPos; // FIXME mappin to screen
            //MotionB.TargetSpeed = 1f;
        }
    }
}
