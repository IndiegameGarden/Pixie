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
        public Vector2 ShadowVector = new Vector2(1f, 1f);

        protected string text;
        protected SpriteFont spriteFont;

        public SubtitleText( string initialText)
            : base()
        {
            text = initialText;
            DrawInfo.DrawColor = Color.White;
            spriteFont = TTengineMaster.ActiveGame.Content.Load<SpriteFont>("Subtitles1");
            AutoPosition();
        }

        public string Text        
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                AutoPosition();
            }
        }

        protected void AutoPosition()
        {
            Motion.Position = new Vector2(0.1f, 0.8f);
        }

        protected override void OnDraw(ref DrawParams p)
        {
            Vector2 pos = DrawInfo.DrawPosition;
            MySpriteBatch.DrawString(spriteFont, text, pos + ShadowVector, Color.Black, 0f, Vector2.Zero, ScaleVector, SpriteEffects.None, DrawInfo.LayerDepth + 0.001f);
            MySpriteBatch.DrawString(spriteFont, text, pos - ShadowVector, Color.DarkGray, 0f, Vector2.Zero, ScaleVector, SpriteEffects.None, DrawInfo.LayerDepth + 0.001f); 
            MySpriteBatch.DrawString(spriteFont, text, pos, DrawInfo.DrawColor, 0f, Vector2.Zero, ScaleVector, SpriteEffects.None, DrawInfo.LayerDepth);
        }
    }
}
