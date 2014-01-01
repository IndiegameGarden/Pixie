// (c) 2010-2013 TranceTrance.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Behaviors;
using TTengine.Modifiers;
using TTengine.Util;
using Artemis;
using Artemis.Interface;
using TreeSharp;

using Game1.Core;
using Game1.Levels;

namespace Game1
{
    /// <summary>
    /// Pixie2 using TTengine-5
    /// </summary>
    public class PixieGame : TTGame
    {
        public PixieGame()
        {
            Instance = this;
            GraphicsMgr.IsFullScreen = false;
            GraphicsMgr.PreferredBackBufferWidth = 640; 
            GraphicsMgr.PreferredBackBufferHeight = 640;
            IsAudio = true;
        }

        public static new PixieGame Instance;
        public Channel GameChannel;
        public PixieLevel Level;

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            // create a default Channel
            GameChannel = TTFactory.CreateChannel(Color.White, false);
            // PointClamp to let all grahics be sharp and blocky (non-interpolated pixels)
            GameChannel.Screen.SpriteBatch.samplerState = SamplerState.PointClamp;
            ChannelMgr.AddChannel(GameChannel);
            GameChannel.ZapTo(); 

            // add framerate counter
            FrameRateCounter.Create(Color.Black);

            // level
            Level = new Level1();
            Level.Init();
            TTFactory.CreateScriptlet(Level);

            // inits based on level
            GameChannel.Screen.BackgroundColor = Level.BackgroundColor;
         
        }       

    }
}
