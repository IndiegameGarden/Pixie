// (c) 2010-2011 TranceTrance.com. Distributed under the FreeBSD license in LICENSE.txt
﻿using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using TTengine.Core;
using Pixie1.Actors;

namespace Pixie1
{
    public class PixieKeyControl: ThingControl
    {
        float pressTime = 0f;
        bool isTriggerPressed = false;

        public PixieKeyControl()
            : base()
        {
            MoveSpeed = 1.5f;
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

            // trigger Toy
            KeyboardState kbstate = Keyboard.GetState();
            bool isSpacePressed =   kbstate.IsKeyDown(Keys.Space) ||
                                    kbstate.IsKeyDown(Keys.X) ||
                                    kbstate.IsKeyDown(Keys.LeftControl);
            if (!isTriggerPressed && isSpacePressed)
            {
                isTriggerPressed = true;

                // use toy
                Toy t = ParentThing.ToyActive;
                if (t != null)
                {
                    if (!t.IsUsed && t.UsesLeft > 0)
                        t.StartUsing();
                }
            }
            else if (!isSpacePressed)
            {
                isTriggerPressed = false;
            }

            // key rep
            if (pressTime > 0.2f / ParentThing.Velocity ) 
                pressTime = 0f;

            // make user's requested motion vector
            TargetMove = new Vector2(dx, dy);
            IsTargetMoveDefined = true;

        }

    }
}
