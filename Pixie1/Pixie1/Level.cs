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
    /// <summary>
    /// base class for all levels (common functions)
    /// </summary>
    public class Level: Drawlet
    {
        // some default colors, not to be changed
        public static Color PIXIE_COLOR = new Color(251, 101, 159); // pink
        public static Color DEFAULT_BG_COLOR = Color.Black;

        public float DEFAULT_SCALE = 20.0f;
        public float SCREEN_MOTION_SPEED = 1.0f;

        public Vector2 PIXIE_STARTING_POS ; // in pixels        
        public Vector2 BG_STARTING_POS ; // in pixels; bg=background

        /// <summary>
        /// public exposed position of the scrolling level (where are we viewing now)
        /// </summary>
        public static Vector2 ViewPos;
        /// <summary>
        /// draw color of the background
        /// </summary>
        public Color BackgroundColor = DEFAULT_BG_COLOR;
        public MotionBehavior MotionB;

        protected LevelBackground bg;
        protected Pixie pixie;
        protected PixieControl keyControl; // for pixie
        protected DebugMessage debugMsg;

        float timeEscDown = 0f;
        

        public Level(): base()
        {
            MotionB = new MotionBehavior();
            Add(MotionB);
        }

        /// <summary>
        /// Init: the scrolling level itself
        /// </summary>
        protected virtual void InitLevel()
        {
        }

        /// <summary>
        /// Init: pixie herself (a default implementation is in Level)
        /// </summary>
        protected virtual void InitPixie()
        {
            pixie = new Pixie();      
            pixie.MotionP.Target = PIXIE_STARTING_POS;
            pixie.MotionP.TargetSpeed = 18.0f; // TODO 
            Add(pixie);

            keyControl = new PixieKeyControl();
            Add(keyControl);
        }

        /// <summary>
        /// Init: bad pixels (enemies)
        /// </summary>
        protected virtual void InitBadPixels()
        {
        }

        /// <summary>
        /// Init: toys (=weapons)
        /// </summary>
        protected virtual void InitToys()
        {
        }

        /// <summary>
        /// Init: level-specific items (not fitting in the existing init categories) to be initialized by subclasses
        /// </summary>
        protected virtual void InitLevelSpecific()
        {
        }

        protected override void OnNewParent()
        {
            base.OnNewParent();

            debugMsg = new DebugMessage("debug");
            debugMsg.DrawInfo.DrawColor = Color.CornflowerBlue;
            Parent.Add(debugMsg);

            Motion.Scale = DEFAULT_SCALE;
            InitLevel();
            InitPixie();
            InitBadPixels();
            InitToys();
            InitLevelSpecific();

        }

        /// check keys specific for level
        protected virtual void LevelKeyControl(ref UpdateParams p)
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
        protected virtual void ScrollBackground(ref UpdateParams p)
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
            ViewPos = bg.MotionP.Position;

            // do some level tasks
            LevelKeyControl(ref p);
            ScrollBackground(ref p);

            // take steering input and move pixie
            Vector2 newPos = pixie.MotionP.Target + keyControl.TargetMove;
            bool isWalkable = bg.IsWalkable(newPos);
            if (isWalkable)
                pixie.MotionP.Target += keyControl.TargetMove;
            TTutil.Round(pixie.MotionP.Target);

            // DEBUG sample pixel
            Color c= bg.SamplePixel(pixie.MotionP.Target);
            debugMsg.Text = "Color: " + c.R + "," + c.G + "," + c.B + "," + c.A;

        }
    }
}
