// (c) 2010-2011 TranceTrance.com. Distributed under the FreeBSD license in LICENSE.txt
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using TTengine.Core;

namespace Pixie1
{
    public class PixieKeyControl: ThingControl
    {
        float pressTime = 0f;

        public PixieKeyControl()
            : base()
        {
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            float dx = 0f, dy = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (pressTime == 0f)
                    dy = -1.0f;
                pressTime += p.Dt;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (pressTime == 0f)
                    dy = +1.0f;
                pressTime += p.Dt;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (pressTime == 0f)
                    dx = -1.0f;
                pressTime += p.Dt;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (pressTime == 0f)
                    dx = +1.0f;
                pressTime += p.Dt;
            }
            else
            {
                pressTime = 0f;
            }

            // key rep
            if (pressTime > 0.2f / ParentThing.Velocity ) // TODO 0.1 const
                pressTime = 0f;

            // make user's requested motion vector
            ParentThing.TargetMove += new Vector2(dx, dy);

        }

    }
}
