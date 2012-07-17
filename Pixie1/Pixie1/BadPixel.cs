using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;

namespace Pixie1
{
    public class BadPixel: Thing
    {
        // behaviors - the things that bad pixels do 
        public BlinkBehaviour Blinking;
        public ChaseBehavior  Chasing;

        public static BadPixel Create()
        {
            return new BadPixel(Level.Current.pixie);
        }

        public static BadPixel CreateCloaky()
        {
            BadPixel p = new BadPixel(Level.Current.pixie);
            p.IsCloaky = true;
            return p;
        }

        bool isCloaky = false;

        public BadPixel(Thing chaseTarget)
            : base("pixie")
        {
            DrawInfo.DrawColor = new Color(255, 10, 4);
            float blinkPeriod = RandomMath.RandomBetween(0.64f, 2.12f);
            float blinkDutycycle = RandomMath.RandomBetween(0.4f, 0.95f);
            Blinking = new BlinkBehaviour(blinkPeriod, blinkDutycycle);
            Add(Blinking);

            Chasing = new ChaseBehavior(chaseTarget);
            Chasing.ChaseSpeed = RandomMath.RandomBetween(0.57f, 1.05f);
            Add(Chasing);
        }

        public bool IsCloaky
        {
            get
            {
                return isCloaky;
            }
            set
            {
                if (IsCloaky == value)
                    return;
                // if change - swap dutycycle
                Blinking.DutyCycle = 1f - Blinking.DutyCycle;
                isCloaky = value;                
            }
        }
        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            //MotionB.Target = TargetPos; // FIXME mappin to screen
            //MotionB.TargetSpeed = 1f;
        }
    }
}
