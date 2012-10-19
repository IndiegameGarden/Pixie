using System;
using System.Collections.Generic;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pixie1;
using Pixie1.Actors;
using Pixie1.Toys;

namespace Pixie1.Levels
{
    public class AmazingLevel : Level
    {
        int numberOfZoomOuts = 0;

        public AmazingLevel()
            : base()
        {
            Current = this; // pointer to level instance singleton

            // Level settings
            SCREEN_MOTION_SPEED = 8.0f;
            DEFAULT_SCALE = 30f;
            PIXIE_STARTING_POS = new Vector2(20f, 20f); // in pixels        
            BG_STARTING_POS = new Vector2(20f, 20f); // in pixels; bg=background            
        }

        protected override void InitLevel()
        {
            base.InitLevel();

            // select bitmap bg
            Background = new LevelBackground("maze4.png");
            Background.TargetSpeed = SCREEN_MOTION_SPEED;
            Add(Background);
            Background.Target = PIXIE_STARTING_POS;
            Background.Position = BG_STARTING_POS;

        }

        protected override void InitBadPixels()
        {
            base.InitBadPixels();

            BadPixel bp = BadPixel.Create(); // Cloaky();
            bp.PositionAndTarget = new Vector2(72f, 34f);
            //bp.TargetSpeed = 18.0f; // TODO
            Add(bp);

            bp = BadPixel.Create();
            bp.PositionAndTarget = new Vector2(37f, 44f);
            Add(bp);
        }

        protected override void InitToys()
        {
            base.InitToys();

            for (int i = 0; i < 20; i++)
            {
                Toy test = new SpeedModifyToy(2f);
                test.PositionAndTarget = PIXIE_STARTING_POS + new Vector2(RandomMath.RandomBetween(10f, 50f), RandomMath.RandomBetween(-40f, 40f));
                Add(test);
            }

            Toy invisToy = new InvisibilityToy();
            invisToy.PositionAndTarget = PIXIE_STARTING_POS + new Vector2(20f, 0f);
            Add(invisToy);

            // attach test
            //test.AttachmentPosition = new Vector2(3f, 0f);
            //pixie.Add(test);
        }

        protected override void InitLevelSpecific()
        {
            Music = new GameMusic();
            //Add(Music);

            SubtitleText t = new SubtitleText();
            t.StartTime = 2f;
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
                //Motion.ScaleTarget /= 2.0f;
                //Motion.ScaleSpeed = 0.2f;
                return false;
            }
            return true;
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            // adapt scroll speed to how fast pixie goes
            Background.TargetSpeed = SCREEN_MOTION_SPEED * pixie.Velocity;
        }
    }
}
