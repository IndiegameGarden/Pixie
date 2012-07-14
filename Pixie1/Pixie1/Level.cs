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
    public abstract class Level: Drawlet
    {
        public static Level Current = null;

        // some default colors, not to be changed
        public static Color PIXIE_COLOR = new Color(251, 101, 159); // pink
        public static Color DEFAULT_BG_COLOR = Color.Black;

        public float DEFAULT_SCALE = 20.0f;
        public float SCREEN_MOTION_SPEED = 15.0f;
        public float PIXIE_TARGETSPEED = 18.0f;

        public Vector2 PIXIE_STARTING_POS ; // in pixels        
        public Vector2 BG_STARTING_POS ; // in pixels; bg=background

        public float BOUND_X = 0.3f;
        public float BOUND_Y = 0.3f;

        /// <summary>
        /// draw color of the background
        /// </summary>
        public Color BackgroundColor = DEFAULT_BG_COLOR;
        public MotionBehavior MotionB;

        public GameMusic gameMusic;

        public LevelBackground bg;
        protected Pixie pixie;
        protected PixieControl keyControl; // for pixie
        protected DebugMessage debugMsg;
        protected SubtitleText subTitles;

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
            Motion.Scale = DEFAULT_SCALE;
            Motion.ScaleTarget = DEFAULT_SCALE;
        }

        /// <summary>
        /// Init: pixie herself (a default implementation is in Level)
        /// </summary>
        protected virtual void InitPixie()
        {
            pixie = new Pixie();      
            pixie.PositionAndTarget = PIXIE_STARTING_POS;
            pixie.TargetSpeed = PIXIE_TARGETSPEED;
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
        protected abstract void InitLevelSpecific();

        protected override void OnNewParent()
        {
            base.OnNewParent();
            debugMsg = new DebugMessage();
            Parent.Add(debugMsg);
            
            InitLevel();
            InitPixie();
            InitBadPixels();
            InitToys();
            InitLevelSpecific();
        }

        /// check keys specific for level
        protected virtual void LevelKeyControl(ref UpdateParams p)
        {
            KeyboardState st = Keyboard.GetState();
            if (st.IsKeyDown(Keys.Escape))
            {
                timeEscDown += p.Dt;
                MotionB.ScaleTarget = 1.5f*DEFAULT_SCALE;
                MotionB.ScaleSpeed = 0.0004f;
                //Motion.RotateModifier = timeEscDown * 0.05f;
                PixieGame.Instance.Exit();
            }
            else
            {
                timeEscDown = 0f;
                MotionB.ScaleTarget = DEFAULT_SCALE; // TODO
            }
            if (timeEscDown > 1.0f)
            {
                PixieGame.Instance.StopPlay();
            }

            if (st.IsKeyDown(Keys.PageUp) && Motion.Zoom == Motion.ZoomTarget)
            {
                Motion.ZoomTarget *= 2.0f;
                Motion.ZoomSpeed = 0.1f;
            }

            if (st.IsKeyDown(Keys.PageDown) && Motion.Zoom == Motion.ZoomTarget)
            {
                Motion.ZoomTarget *= 0.5f;
                Motion.ZoomSpeed = 0.1f;
            }

            if (st.IsKeyDown(Keys.LeftControl))
                keyControl.IsGodMode = true;
            else
                keyControl.IsGodMode = false;

        }

        // scroll the level background to match pixie
        protected virtual void ScrollBackground(ref UpdateParams p)
        {
            // scrolling background at borders
            Vector2 pixiePos = pixie.Motion.PositionAbs;

            if (pixiePos.X < BOUND_X || pixiePos.X > (Screen.Width - BOUND_X) ||
                pixiePos.Y < BOUND_Y || pixiePos.Y > (Screen.Height - BOUND_Y))
            {
                if (ScreenBorderHit())
                    bg.Target = pixie.Position;
            }
        }

        /// <summary>
        /// can be overridden with custom functions if screen border is hit by pixie
        /// </summary>
        protected virtual bool ScreenBorderHit()
        {
            return true;
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            // important: reflect the global viewpos (for sprites to use)
            PixieSpritelet.ViewPos = bg.Position;

            // do some level tasks
            LevelKeyControl(ref p);
            ScrollBackground(ref p);

            // take steering input and move pixie
            Vector2 newPos = pixie.Target + keyControl.TargetMove;
            bool isWalkable = bg.IsWalkable(newPos) || keyControl.IsGodMode ;
            if (isWalkable)
                pixie.Target += keyControl.TargetMove;
            TTutil.Round(pixie.Target);

            debugMsg.Text = "Pixie: trg=" + pixie.Target +", pos=" + pixie.Position+", m.pos="+pixie.Motion.Position;
            // DEBUG sample pixel
            Color c= bg.SamplePixel(pixie.Target);
            debugMsg.Text += "Color: " + c.R + "," + c.G + "," + c.B + "," + c.A;

        }
    }
}
