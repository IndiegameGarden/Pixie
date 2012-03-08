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
    public class PixieSpritelet: Spritelet
    {
        public PixieMotionBehavior MotionP;

        public PixieSpritelet(string bitmapFile)
            : base()
        {
            if (bitmapFile.Contains("."))
                LoadBitmap(bitmapFile);
            this.fileName = bitmapFile;
            InitTextures();

            MotionP = new PixieMotionBehavior();
            Add(MotionP);

        }

        protected void LoadBitmap(string fn)
        {
            // load texture
            FileStream fs = null;
            try
            {
                fs = new FileStream(TTengineMaster.ActiveGame.Content.RootDirectory + "\\" + fn, FileMode.Open);
                Texture2D t = Texture2D.FromStream(Screen.graphicsDevice, fs);
                Texture = t;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }            
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            //PixieGame.ViewPos in pixels
            Motion.Position = Screen.Center + Motion.ScaleAbs * (  FromPixels( MotionP.Position - Level.ViewPos)); // TODO ViewPos smoothing using Draw cache

            /* JUST DOC
                            // calculate Position from Motion
            Vector2 mp = ToPixels((-Motion.PositionAbs + Screen.Center) / Motion.ScaleAbs) - HALF_PIXEL_OFFSET;
            MotionP.Position = mp;
            
            // move towards target
            MotionB.Target = Screen.Center - Motion.ScaleAbs * FromPixels(MotionP.Target + HALF_PIXEL_OFFSET);
            */
        }

    }
}
