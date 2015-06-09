
using System;
using Microsoft.Xna.Framework;

using Game1.Core;
using PXengine.Core;

namespace Game1.Levels
{
    public class Level1: PXLevel
    {
        public Level1()
        {
            LevelBitmapFile = "Level1";
            PIXIE_STARTING_POS = new Vector2(380f,200f); // in pixels        
            BG_STARTING_POS = new Vector2(360f, 200f);    // in pixels; bg=background
            DEFAULT_SCALE = 15f;
        }

        protected override void InitLevelSpecific()
        {
            //
        }
    }
}
