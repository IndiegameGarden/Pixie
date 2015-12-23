using System;
using PXengine.Core;
using TTengine.Core;
using Game1.Levels;

namespace Game1.Core
{
    public class PixieGame: PXGame
    {
        protected override void LoadContent()
        {
            // level
            Level = new Level1();
            Level.Init();
            TTFactory.CreateScriptlet(Level);

            base.LoadContent();
        }
    }
}
