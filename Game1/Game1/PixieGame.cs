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

namespace Game1
{
    /// <summary>
    /// Pixie2 using TTengine-5
    /// </summary>
    public class PixieGame : TTGame
    {
        public PixieGame()
        {
            GraphicsMgr.IsFullScreen = false;
            GraphicsMgr.PreferredBackBufferWidth = 640; 
            GraphicsMgr.PreferredBackBufferHeight = 640;
            IsAudio = true;
        }

        public PixieFactory Factory;
        public Channel GameChannel;
        public PixieLevel Level;

        protected override void Initialize()
        {
            Factory = PixieFactory.Instance;
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Space))
            {
                //
            }
            else
            {
                //
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            GameChannel = TTFactory.CreateChannel(Color.White, false);
            ChannelMgr.AddChannel(GameChannel);
            GameChannel.ZapTo(); 

            // add framerate counter
            FrameRateCounter.Create(Color.Black);

            // level
            Level = new TrainwrecksLevel();
            Level.Init();
         
        }       

    }
}
