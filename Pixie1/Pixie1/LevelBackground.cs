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
    public class LevelBackground: PixieSpritelet
    {
        Vector2 HALF_PIXEL_OFFSET = new Vector2(0.5f, 0.45f);
        SpriteBatch spriteBatch;

        public LevelBackground(string bitmapFileName, float motionSpeed)
            : base(bitmapFileName)
        {
            spriteBatch = new SpriteBatch(Screen.graphicsDevice);
            TargetSpeed = motionSpeed;
        }

        public Color SamplePixel(Vector2 pos)
        {
            if (pos.X < 0f || pos.X > (Texture.Width - 1) ||
                pos.Y < 0f || pos.Y > (Texture.Height - 1))
            {
                return Color.Black; // TODO bg color configurable
            }
            Color[] data = new Color[1];
            Texture.GetData<Color>(0, new Rectangle((int)pos.X, (int)pos.Y, 1, 1), data, 0, 1);
            return data[0];
        }

        public bool IsWalkable(Vector2 pos)
        {
            Color c = SamplePixel(pos);
            float intensity = ((float)(c.R + c.G + c.B)) / (3.0f * 255.0f);
            if (intensity > 0.39f) // TODO make configurable of course (+ inheritance possible)
                return true;
            else
                return false;
        }

        // 2 juni ; augustus laatste week;
        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            // update my on-screen position (shifting the big bitmap around to match middle point set)
            Motion.Position = Screen.Center - Motion.ScaleAbs * FromPixels(Position + HALF_PIXEL_OFFSET);            
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
