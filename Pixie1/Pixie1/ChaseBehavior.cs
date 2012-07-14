using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using Microsoft.Xna.Framework;

namespace Pixie1
{
    public class ChaseBehavior: PixieControl
    {
        PixieSpritelet chaseTarget;
        float wTime = 0f;

        public ChaseBehavior(PixieSpritelet chaseTarget)
        {
            this.chaseTarget = chaseTarget;
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            wTime += p.Dt;
            if (wTime >= 0.1f)
                wTime = 0f;
            if (wTime == 0f)
            {
                Vector2 dif = chaseTarget.Position - (Parent as PixieSpritelet).Position;
                dif /= dif.Length();
                TargetMove = dif;
            }
            else
            {
                TargetMove = Vector2.Zero;
            }
        }
    }
}
