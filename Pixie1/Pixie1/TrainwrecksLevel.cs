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
        int numberOfZoomOuts = 0;

        public TrainwrecksLevel(): base()
        {
            SCREEN_MOTION_SPEED = 1.0f;
            DEFAULT_SCALE = 100f;
            PIXIE_STARTING_POS = new Vector2(192f, 146f); // in pixels        
            //BG_STARTING_POS = new Vector2(172f, 1300f); // in pixels; bg=background            
            BG_STARTING_POS = new Vector2(192f, 146f); // in pixels; bg=background            
        }

        protected override void InitLevel()
        {
            base.InitLevel();

            // select bitmap bg
            bg = new LevelBackground("bg2045", SCREEN_MOTION_SPEED);
            Add(bg);
            bg.Target = PIXIE_STARTING_POS;
            bg.Position = BG_STARTING_POS;

        }

        protected override void InitBadPixels()
        {
            base.InitBadPixels();

            BadPixel bp = new BadPixel();
            bp.Target = PIXIE_STARTING_POS + new Vector2(5f, 0f);
            bp.TargetSpeed = 18.0f; // TODO
            Add(bp);
        }

        protected override void InitToys()
        {
            base.InitToys();

            Toy test = new Toy();
            test.Target = PIXIE_STARTING_POS + new Vector2(10f, 0f);
            test.TargetSpeed = 18.0f; // TODO
            Add(test);
        }

        protected override void InitLevelSpecific()
        {
            SubtitleText t = new SubtitleText();
            t.StartTime = 2f;
            //t.Motion.Scale = 1 / DEFAULT_SCALE;
            t.AddText("Oh no.", 3f);
            t.AddText("", 3f);
            t.AddText("Where am I?", 3f);
            t.AddText("I'm lost.", 3f);
            t.AddText("Can you help me\nget back home?", 3f);
            Parent.Add(t);
        }

        protected override bool ScreenBorderHit()
        {
            if (numberOfZoomOuts < 0)
            {
                numberOfZoomOuts++;
                Motion.Scale /= 2.0f;
                return false;
            }
            return true;
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

        }
    }
}
