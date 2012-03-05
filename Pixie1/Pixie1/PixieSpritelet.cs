using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;

namespace Pixie1
{
    public class PixieSpritelet: Spritelet
    {
        public PixieMotionBehavior MotionP;

        public PixieSpritelet(string bitmapFile)
            : base(bitmapFile)
        {            
            MotionP = new PixieMotionBehavior();
            Add(MotionP);

        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            //PixieGame.ViewPos in pixels
            Motion.Position = Screen.Center + Motion.ScaleAbs * (  FromPixels( MotionP.Position - Level.ViewPos)); // TODO ViewPos smoothing using Draw cache

            /* JUST DOC
                            // calculate Position from Motion
            Vector2 mp = ToPixels((-Motion.PositionAbs + Screen.Center) / Motion.ScaleAbs) - HALF_PIXEL_OFFSET;
            MotionP.Position = mp;
            
            // move towards target
            MotionB.Target = Screen.Center - Motion.ScaleAbs * FromPixels(MotionP.Target + HALF_PIXEL_OFFSET);
            */
        }

    }
}
