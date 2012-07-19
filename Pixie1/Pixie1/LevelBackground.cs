using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pixie1
{
    public class LevelBackground: Thing
    {
        public Color BackgroundColor = Color.Black;

        SpriteBatch spriteBatch;

        public LevelBackground(string bitmapFileName)
            : base(bitmapFileName)
        {
            spriteBatch = new SpriteBatch(Screen.graphicsDevice);
        }
        
        public Color SamplePixel(Vector2 pos)
        {
            if (pos.X < 0f || pos.X > (Texture.Width - 1) ||
                pos.Y < 0f || pos.Y > (Texture.Height - 1))
            {
                return BackgroundColor;
            }
            Color[] data = new Color[1];
            Texture.GetData<Color>(0, new Rectangle((int)Math.Round(pos.X), (int)Math.Round(pos.Y), 1, 1), data, 0, 1);
            return data[0];
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            // update my on-screen position (shifting the big bitmap around to match middle point set)
            Motion.Position = Screen.Center - Motion.ScaleAbs * FromPixels(Position);            
        }

        protected override void OnDraw(ref DrawParams p)
        {
            if (Texture != null)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

                spriteBatch.Draw(Texture, DrawInfo.DrawPosition, null, DrawInfo.DrawColor,
                       Motion.RotateAbs, Vector2.Zero, DrawInfo.DrawScale, SpriteEffects.None, DrawInfo.LayerDepth);

                spriteBatch.End();
            }
            
        }

    }
}
