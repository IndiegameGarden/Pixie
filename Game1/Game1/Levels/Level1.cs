
using System;
using Microsoft.Xna.Framework;

using Game1.Core;

namespace Game1.Levels
{
    public class Level1: PixieLevel
    {
        public Level1()
        {
            LevelBitmapFile = "Level1";
            PIXIE_STARTING_POS = new Vector2(380f,200f); // in pixels        
            BG_STARTING_POS = new Vector2(280f, 200f);    // in pixels; bg=background
            DEFAULT_SCALE = 5f;
        }

        protected override void InitLevelSpecific()
        {
            //
        }
    }
}
