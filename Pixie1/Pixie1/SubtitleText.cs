// (c) 2010-2011 TranceTrance.com. Distributed under the FreeBSD license in LICENSE.txt
﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TTengine.Core;
using TTengine.Util;

namespace Pixie1
{
    /**
     * Displays a subtitle on screen for a specified time (no rotation or scale at this moment)
     * Text strings may contain Enter char (backslash n). It auto-positions.
     */
    public class SubtitleText : Drawlet
    {
        /// <summary>
        /// scaling vector for subtitles text (horizontal scale, vertical scale)
        /// </summary>
        public Vector2 ScaleVector = new Vector2(2f, 1.5f);

        /// <summary>
        /// displacement (in pixels) of the shadow below subtitles
        /// </summary>
        public Vector2 ShadowVector = new Vector2(2f, 2f);

        /// <summary>
        /// if true uses shadow effect for better visibility of subtitles
        /// </summary>
        public bool Shadow = true;

        /// <summary>
        /// which font to use for drawing text (can be changed)
        /// </summary>
        public SpriteFont SubtitleFont;

        /// <summary>
        /// vertical spacing between lines for multi-line subtitles
        /// </summary>
        public float VerticalLineSpacing = 0.05f;

        // the default font - used if SubtitleFont not specified
        protected static SpriteFont DefaultSubtitleFont;

        // vars internal
        protected string[] text;
        protected float[] timings;
        Vector2 totalTextSize;
        bool doReplace;
        float nextTextStartTime = 0f;

        /// <summary>
        /// create new, empty SubtitleText
        /// </summary>
        public SubtitleText()
            : this("")
        {
            // nothing here: see constructor SubtitleText( string initialText)
        }

        /// <summary>
        /// create new SubtitleText from initial string
        /// </summary>
        /// <param name="initialText"></param>
        public SubtitleText( string initialText)
            : base()
        {
            text = new string[] { initialText };
            timings = new float[] { 0f };
            doReplace = true;
            DrawInfo.DrawColor = Color.White;
            InitFont();
        }

        /// <summary>
        /// create a new multitext, with multiple strings showing at indicated times.
        /// </summary>
        /// <param name="multiText"></param>
        /// <param name="timings">per multiText item, number of seconds that this item should display</param>
        /// <param name="doReplace">if true, only latest item from multiText is displayed. 
        ///                         If false, each item is added to the previous one.</param>
        public SubtitleText(string[] multiText, float[] timings, bool doReplace)
        {
            this.text = multiText;
            this.timings = timings;
            this.doReplace = doReplace;
            DrawInfo.DrawColor = Color.White;
            InitFont();
        }

        private void InitFont()
        {
            if (DefaultSubtitleFont==null)
                DefaultSubtitleFont = TTengineMaster.ActiveGame.Content.Load<SpriteFont>("Subtitles1");
            SubtitleFont = DefaultSubtitleFont;
        }

        public string Text        
        {
            get
            {
                return text[0];
            }
            set
            {
                text[0] = value;
                timings = new float[] { 0f };
                AutoPosition();
            }
        }

        public void AddText(string txt, float duration)
        {
            SubtitleText st = new SubtitleText(txt);
            st.Duration = duration - 0.1f;
            st.StartTime = nextTextStartTime;
            AddNextUpdate(st);
            nextTextStartTime += duration;
            Duration = nextTextStartTime;
        }

        protected void AutoPosition()
        {
            int lines = TTutil.LineCount(text[0]);
            float yOffset = -((float)(lines - 1)) * VerticalLineSpacing - VerticalLineSpacing;

            if (Parent != null && Parent is SubtitleText)
            {
                Motion.Position = new Vector2(0f,yOffset);
                // Use entire text size for calculating middle offsets 
                totalTextSize = SubtitleFont.MeasureString(text[0]); 
            }
            else
            {
                Motion.Position = new Vector2(Screen.Center.X, 1f+yOffset); // TODO move up for long texts
                // Use entire text size for calculating middle offsets 
                totalTextSize = SubtitleFont.MeasureString(text[0]);
            }
        }

        protected override void OnNewParent()
        {
            base.OnNewParent();
            AutoPosition();
        }

        protected override void OnDraw(ref DrawParams p)
        {
            Vector2 pos = DrawInfo.DrawPosition;
            String curText = "";
            if (text.Length > 0 && text[0].Length > 0)
            {
                float t = 0f;
                for (int i = 0; i < text.Length; i++ )
                {
                    if (t <= SimTime)
                    {
                        if (doReplace)
                            curText = text[i];
                        else
                        {
                            if (curText.Length > 0)
                                curText += "\n";
                            curText += text[i];
                        }
                    }
                    t += timings[i];
                }
            }

            if (curText.Length > 0)
            {
                float sc = Motion.ScaleAbs;
                Vector2 origin = totalTextSize / 2f; // Vector2.Zero; // new Vector2(((float)curText.Length) / 40f, 0f);
                if (Shadow)
                {
                    MySpriteBatch.DrawString(SubtitleFont, curText, pos + ShadowVector, Color.Black, 0f, origin, ScaleVector * sc, SpriteEffects.None, DrawInfo.LayerDepth + 0.0001f);
                    MySpriteBatch.DrawString(SubtitleFont, curText, pos - ShadowVector, Color.DarkGray, 0f, origin, ScaleVector * sc, SpriteEffects.None, DrawInfo.LayerDepth + 0.0002f);
                }
                MySpriteBatch.DrawString(SubtitleFont, curText, pos, DrawInfo.DrawColor, 0f, origin, ScaleVector * sc, SpriteEffects.None, DrawInfo.LayerDepth);
            }
        }
    }
}
