﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Util;

namespace Game1
{
    /// <summary>
    /// base class for all levels (common functions)
    /// </summary>
    public abstract class PixieLevel: IScript
    {
        // some default colors and settings that may be changed by Level subclasses
        public static Color PIXIE_COLOR = new Color(251, 101, 159); // pink
        public float DEFAULT_SCALE = 20.0f;
        public float SCREEN_MOTION_SPEED = 15.0f;
        public float PIXIE_TARGETSPEED = 5.0f;
        public int DefaultPassableIntensityThreshold = 380;
        public Vector2 PIXIE_STARTING_POS = Vector2.Zero; // in pixels        
        public Vector2 BG_STARTING_POS = Vector2.Zero;    // in pixels; bg=background

        /// <summary>
        /// scrolling screen trigger boundaries (in pixels)
        /// </summary>
        public bool isBackgroundScrollingOn = true;
        public float BOUND_X = 30f;
        public float BOUND_Y = 30f;

        /// <summary>
        /// default color of the background (e.g. for areas not covered by the bg bitmap)
        /// </summary>
        public Color BackgroundColor = Color.White;

        /// <summary>
        /// level music
        /// </summary> 
        //public GameMusic Music;

        /// <summary>
        /// background bitmap
        /// </summary>
        //public LevelBackground Background;

        /// <summary>
        /// our heroine Pixie
        /// </summary>
        //public Pixie pixie;

        //public SubtitleManager Subtitles;

        public PixieLevel(): base()
        {
            //Subtitles = new SubtitleManager();
        }

        /// <summary>
        /// Init: the scrolling level itself. First Init method that is called
        /// </summary>
        protected virtual void InitLevel()
        {
            //Motion.Scale = DEFAULT_SCALE;
            //Motion.ScaleTarget = DEFAULT_SCALE;
            //MySpriteBatch = new TTSpriteBatch(Screen.graphicsDevice, SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
        }

        /// <summary>
        /// Init: pixie herself (a default implementation is in Level)
        /// </summary>
        protected virtual void InitPixie()
        {
            //pixie = new Pixie();      
            //pixie.PositionAndTarget = PIXIE_STARTING_POS;
            //pixie.TargetSpeed = PIXIE_TARGETSPEED;
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

        public void Init()
        {
            InitLevel();
            InitPixie();
            InitBadPixels();
            InitToys();
            InitLevelSpecific();
        }

        /// check keys specific for level
        protected virtual void LevelKeyControl(/*ref UpdateParams p*/)
        {
            /*
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

            // FIXME remove debug keys
            if (st.IsKeyDown(Keys.PageUp) && Motion.Zoom == Motion.ZoomTarget)
            {
                Motion.ZoomTarget *= 2.0f;
                Motion.ZoomSpeed = 0.02f;
            }

            if (st.IsKeyDown(Keys.PageDown) && Motion.Zoom == Motion.ZoomTarget)
            {
                Motion.ZoomTarget *= 0.5f;
                Motion.ZoomSpeed = 0.02f;
            }

            if (st.IsKeyDown(Keys.RightControl))
                pixie.IsCollisionFree = true;
            else
                pixie.IsCollisionFree = false;

             */
        }

        // scroll the level background to match pixie
        /*
        protected virtual void ScrollBackground(ref UpdateParams p)
        {
            // scrolling background at borders
            Vector2 pixiePos = pixie.Motion.PositionAbs;

            if (pixiePos.X < BOUND_X || pixiePos.X > (Screen.Width - BOUND_X) ||
                pixiePos.Y < BOUND_Y || pixiePos.Y > (Screen.Height - BOUND_Y))
            {
                if (ScreenBorderHit())
                    Background.Target = pixie.Position;
            }
        }
        */

        /// <summary>
        /// can be overridden with custom functions if screen border is hit by pixie
        /// </summary>
        protected virtual bool ScreenBorderHit()
        {
            return true;
        }

        public void OnUpdate(ScriptContext ctx)
        {
            /*
            base.OnUpdate(ref p);

            // important: reflect the global viewpos (for sprites to use)
            Thing.ViewPos = Background.Position;

            // do some level tasks
            LevelKeyControl(ref p);
            if (isBackgroundScrollingOn)
                ScrollBackground(ref p);

            debugMsg.Text = "Pixie: trg=" + pixie.Target +", pos=" + pixie.Position;
            // DEBUG sample pixel
            Color c= Background.SamplePixel(pixie.Target);
            debugMsg.Text += "Color: " + c.R + "," + c.G + "," + c.B + "," + c.A;
            */
        }

        public void OnDraw(ScriptContext ctx)
        {
        }
    }
}
