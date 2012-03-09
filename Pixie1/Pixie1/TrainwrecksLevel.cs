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
            PIXIE_STARTING_POS = new Vector2(192f, 146f); // in pixels        
            BG_STARTING_POS = new Vector2(142f, 30f); // in pixels; bg=background

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
            SubtitleText t = new SubtitleText(new string[]{"Hi! I'm Pixie.", "ARROWS move me.","SPACE to use my special gear."},new float[]{0f,3f,6f},true);
            t.StartTime = 3.0f;
            t.Duration = 9.5f;
            t.ScaleVector = new Vector2(1.5f, 1f);
            //t.Motion.Position = new Vector2(
            Parent.Add(t);

        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

        }
    }
}
