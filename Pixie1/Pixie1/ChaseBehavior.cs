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
            if (wTime >= 0.2f) // TODO speed varies
                wTime = 0f;
            if (wTime == 0f)
            {
                Vector2 dif = chaseTarget.Position - ParentPixie.Position;
                if (dif.Length() > 0f)
                    dif /= dif.Length();
                if (dif.X != 0f && dif.Y != 0f)
                {
                    // choose one direction randomly, if diagonals are requested
                    float r = RandomMath.RandomUnit();
                    if (r > 0.5f)
                        dif.X = 0f;
                    else
                        dif.Y = 0f;
                }
                TargetMove = dif;
            }
            else
            {
                TargetMove = Vector2.Zero;
            }
        }
    }
}
