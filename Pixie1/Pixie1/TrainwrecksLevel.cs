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
    public class TrainwrecksLevel: Drawlet
    {
        //public const float DEFAULT_ZOOM = 1.0f; 
        public const float DEFAULT_SCALE = 20.0f;
        public const float SCREEN_MOTION_SPEED = 1.0f;

        public Vector2 PIXIE_STARTING_POS = new Vector2(430f, 800f); // in pixels        
        public Vector2 BG_STARTING_POS = new Vector2(20f,800f); // in pixels; bg=background

        LevelBackground bg;
        Pixie pixie;
        PixieControl keyControl; // for pixie
        float timeEscDown = 0f;
        MotionBehavior MotionB;
        DebugMessage debugMsg;

        public TrainwrecksLevel(): base()
        {
            // scale all level objects! pixely effect
            Motion.Scale = DEFAULT_SCALE;

            // select bitmap bg
            bg = new LevelBackground("bg2045", SCREEN_MOTION_SPEED);
            Add(bg);
            bg.MotionP.Target = BG_STARTING_POS;

            MotionB = new MotionBehavior();
            Add(MotionB);

        }

        void InitPixie()
        {
            pixie = new Pixie();
            //pixie.Motion.Scale = DEFAULT_SCALE;            
            pixie.MotionP.Target = PIXIE_STARTING_POS;
            pixie.MotionP.TargetSpeed = 18.0f; // TODO 
            Add(pixie);

            keyControl = new PixieKeyControl();
            Add(keyControl);

        }

        void InitBadPixels()
        {
            BadPixel bp = new BadPixel();
            bp.MotionP.Target = PIXIE_STARTING_POS + new Vector2(5f, 0f);
            bp.MotionP.TargetSpeed = 18.0f; // TODO
            Add(bp);
        }

        void InitToys()
        {
            Toy test = new Toy();
            test.MotionP.Target = PIXIE_STARTING_POS + new Vector2(10f, 0f);
            test.MotionP.TargetSpeed = 18.0f; // TODO
            Add(test);
        }

        protected override void OnNewParent()
        {
            base.OnNewParent();

            InitPixie();
            InitBadPixels();
            InitToys();

            // just testing
            SubtitleText t = new SubtitleText("Hi! I'm Pixie.");
            t.StartTime = 2.0f;
            t.Duration = 4.0f;
            Add(t);

            debugMsg = new DebugMessage("debug");
            debugMsg.DrawInfo.DrawColor = Color.CornflowerBlue;
            Parent.Add(debugMsg);

        }

        /// check keys specific for level
        void LevelKeyControl(ref UpdateParams p)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                timeEscDown += p.Dt;
                MotionB.ScaleTarget = 3f*DEFAULT_SCALE;
                MotionB.ScaleSpeed = 0.01f;
                //Motion.RotateModifier = timeEscDown * 0.05f;
            }
            else
            {
                timeEscDown = 0f;
                MotionB.ScaleTarget = DEFAULT_SCALE; // TODO
                //MotionB.ZoomSpeed = 0.0018f;
            }
            if (timeEscDown > 1.0f)
                TTengineMaster.ActiveGame.Exit();

        }

        // scroll the level background to match pixie
        void ScrollBackground(ref UpdateParams p)
        {
            // scrolling background at borders
            Vector2 pixiePos = pixie.Motion.PositionAbs;
            const float BOUND_X = 0.3f;
            const float BOUND_Y = 0.3f;

            if (pixiePos.X < BOUND_X || pixiePos.X > (Screen.Width - BOUND_X) ||
                pixiePos.Y < BOUND_Y || pixiePos.Y > (Screen.Height - BOUND_Y))
            {
                bg.MotionP.Target = pixie.MotionP.Position;
            }
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            // important: reflect the global viewpos (for sprites to use)
            PixieGame.ViewPos = bg.MotionP.Position;

            // do some level tasks
            LevelKeyControl(ref p);
            ScrollBackground(ref p);

            // take steering input and move pixie
            Vector2 newPos = pixie.MotionP.Target + keyControl.TargetMove;
            bool isWalkable = bg.IsWalkable(newPos);
            if (isWalkable)
                pixie.MotionP.Target += keyControl.TargetMove;
            TTutil.Round(pixie.MotionP.Target);
            if (keyControl.TargetMove.Length() > 0)
            {
                //test DEBUG
                int a = 3;
            }

            // sample pixel
            Color c= bg.SamplePixel(pixie.MotionP.Target);
            debugMsg.Text = "Color: " + c.R + "," + c.G + "," + c.B + "," + c.A;

        }
    }
}
