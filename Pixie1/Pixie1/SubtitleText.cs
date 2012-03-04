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
            MySpriteBatch.DrawString(spriteFont, text, pos + new Vector2(1f,1f), Color.Black, 0f, Vector2.Zero, 2f, SpriteEffects.None, DrawInfo.LayerDepth + 0.001f);
            MySpriteBatch.DrawString(spriteFont, text, pos + new Vector2(-1f, -1f), Color.DarkGray, 0f, Vector2.Zero, 2f, SpriteEffects.None, DrawInfo.LayerDepth + 0.001f); 
            MySpriteBatch.DrawString(spriteFont, text, pos, DrawInfo.DrawColor, 0f, Vector2.Zero, 2f, SpriteEffects.None, DrawInfo.LayerDepth);
        }
    }
}
