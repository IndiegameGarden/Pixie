using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using TTengine.Modifiers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pixie1
{
    public class PixieLogo: Spritelet
    {
        Spritelet bg;
        SpriteBatch spriteBatch;
        float bgColMultiplier = 0.3f;

        public PixieLogo(): base("pixielogo")
        {
            bg = new Spritelet("psych");
            bg.DrawInfo.DrawColor = Color.White * bgColMultiplier;
            bg.Motion.Scale = 2.0f;
            Add(bg);
            bg.Visible = false; // do draw ourselves
            spriteBatch = new SpriteBatch(Screen.graphicsDevice);

        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            // color
            bgColMultiplier += RandomMath.RandomBetween(-0.0005f, +0.0005f);
            bg.DrawInfo.DrawColor = Color.White * bgColMultiplier;
            if (bgColMultiplier <= 0.08f)
                bgColMultiplier += RandomMath.RandomBetween(0f, +0.0008f);
            if (bgColMultiplier > 0.5432f)
                bgColMultiplier -= RandomMath.RandomBetween(-0.0008f,-0.0002f);


            //
            float f = (SimTime % 4.0f) / 4.0f ;
            //if (bg.MotionP.Target.X < bg.MotionP.Position.X)
            //    bg.MotionP.Position.X = bg.MotionP.Target.X;
            float g = (SimTime % 20.0f)/20.0f;
            //Vector2 v = new Vector2(bg.Texture.Width * (0.2f + 0.7f * f), bg.Texture.Height * (0.1f + 0.8f * g));
            float freq = 0.0123f;
            Vector2 v = new Vector2( (float) Math.Cos(MathHelper.TwoPi * freq * SimTime), (float) Math.Sin(MathHelper.TwoPi * freq * SimTime) );
            v = new Vector2(0.5f, 0.5f) + 0.33f * v;
            bg.Motion.Position = Screen.Center - Motion.ScaleAbs * v;
            
        }

        protected override void OnDraw(ref DrawParams p)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null,null);

            spriteBatch.Draw(bg.Texture, bg.DrawInfo.DrawPosition, null, bg.DrawInfo.DrawColor,
                   bg.Motion.RotateAbs, /*MotionP.Position*/ Vector2.Zero, bg.DrawInfo.DrawScale, SpriteEffects.None, 0.9f);
            spriteBatch.Draw(Texture, DrawInfo.DrawPosition, null, DrawInfo.DrawColor,
               Motion.RotateAbs, DrawInfo.DrawCenter, DrawInfo.DrawScale, SpriteEffects.None, 0.1f);
            spriteBatch.End();

            
        }

    }
}
