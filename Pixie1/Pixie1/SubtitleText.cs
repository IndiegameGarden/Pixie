// (c) 2010-2011 TranceTrance.com. Distributed under the FreeBSD license in LICENSE.txt
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TTengine.Core;

namespace Pixie1
{
    /**
     * Displays a subtitle on screen for a specified time (no rotation or scale at this moment)
     * It auto-positions.
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

        public bool Shadow = true;

        protected string[] text;
        protected float[] timings;
        public SpriteFont SubtitleFont;
        bool doReplace;

        public SubtitleText( string initialText)
            : base()
        {
            text = new string[] { initialText };
            timings = new float[] { 0f };
            doReplace = false;
            DrawInfo.DrawColor = Color.White;
            SubtitleFont = TTengineMaster.ActiveGame.Content.Load<SpriteFont>("Subtitles1");
            AutoPosition();
        }

        public SubtitleText(string[] multiText, float[] timings, bool doReplace)
        {
            this.text = multiText;
            this.timings = timings;
            this.doReplace = doReplace;
            DrawInfo.DrawColor = Color.White;
            SubtitleFont = TTengineMaster.ActiveGame.Content.Load<SpriteFont>("Subtitles1");
            AutoPosition();
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

        protected void AutoPosition()
        {
            Motion.Position = new Vector2(0.05f, 0.8f);
        }

        protected override void OnDraw(ref DrawParams p)
        {
            Vector2 pos = DrawInfo.DrawPosition;
            String curText = "";
            if (text.Length > 0)
            {
                for (int i = 0; i < text.Length; i++ )
                {
                    if (timings[i] <= SimTime)
                    {
                        if (doReplace)
                            curText = text[i];
                        else
                            curText += "\n" + text[i];
                    }
                }
            }
            float sc = Motion.ScaleAbs;
            Vector2 origin = Vector2.Zero; // new Vector2(((float)curText.Length) / 40f, 0f);
            if (Shadow)
            {
                MySpriteBatch.DrawString(SubtitleFont, curText, pos + ShadowVector, Color.Black, 0f, origin, ScaleVector * sc, SpriteEffects.None, DrawInfo.LayerDepth + 0.0001f);
                MySpriteBatch.DrawString(SubtitleFont, curText, pos - ShadowVector, Color.DarkGray, 0f, origin, ScaleVector * sc, SpriteEffects.None, DrawInfo.LayerDepth + 0.0002f);
            }
            MySpriteBatch.DrawString(SubtitleFont, curText, pos, DrawInfo.DrawColor, 0f, origin, ScaleVector * sc, SpriteEffects.None, DrawInfo.LayerDepth);
        }
    }
}
