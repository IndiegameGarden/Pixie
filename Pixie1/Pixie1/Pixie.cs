using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;

namespace Pixie1
{
    public class Pixie: PixieSpritelet
    {
        
        public Pixie()
            : base("pixie")
        {
            DrawInfo.DrawColor = Level.PIXIE_COLOR; // 
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            //MotionB.Target = TargetPos; // FIXME mappin to screen
            //MotionB.TargetSpeed = 1f;
        }
    }
}
