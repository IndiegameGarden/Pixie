using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pixie1
{
    public class TrainwrecksLevel: Level
    {

        public TrainwrecksLevel(): base()
        {
            DEFAULT_SCALE = 20.0f;
            SCREEN_MOTION_SPEED = 1.0f;
        }

        protected override void InitLevel()
        {
            PIXIE_STARTING_POS = new Vector2(430f, 800f); // in pixels        
            BG_STARTING_POS = new Vector2(20f, 800f); // in pixels; bg=background

            // select bitmap bg
            bg = new LevelBackground("bg2045", SCREEN_MOTION_SPEED);
            Add(bg);
            bg.MotionP.Target = BG_STARTING_POS;

        }

        protected override void InitBadPixels()
        {
            BadPixel bp = new BadPixel();
            bp.MotionP.Target = PIXIE_STARTING_POS + new Vector2(5f, 0f);
            bp.MotionP.TargetSpeed = 18.0f; // TODO
            Add(bp);
        }

        protected override void InitToys()
        {
            Toy test = new Toy();
            test.MotionP.Target = PIXIE_STARTING_POS + new Vector2(10f, 0f);
            test.MotionP.TargetSpeed = 18.0f; // TODO
            Add(test);
        }

        protected override void InitLevelSpecific()
        {
            // just testing
            SubtitleText t = new SubtitleText("Hi! I'm Pixie.");
            t.StartTime = 2.0f;
            t.Duration = 4.0f;
            Add(t);

        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

        }
    }
}
