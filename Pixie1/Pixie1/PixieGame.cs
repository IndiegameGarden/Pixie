using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TTengine.Core;
using TTengine.Util;
using TTengine.Modifiers;


namespace Pixie1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PixieGame : Game
    {
        public static Gamelet TreeRoot;        

        GraphicsDeviceManager graphics;
        int myWindowWidth = 1024; 
        int myWindowHeight = 768; 
        PixieScreenlet mainScreenlet;

        public PixieGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = myWindowHeight;
            graphics.PreferredBackBufferWidth = myWindowWidth;
            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            TTengineMaster.Create(this);

            // from here on, main screen
            mainScreenlet = new PixieScreenlet(myWindowWidth, myWindowHeight);
            TTengineMaster.ActiveScreen = mainScreenlet;
            TreeRoot = new FixedTimestepPhysics();
            TreeRoot.Add(mainScreenlet);
            
            // finally call base to enumnerate all (gfx) Game components to init
            base.Initialize();
        }

        protected override void LoadContent()
        {
            mainScreenlet.Add(new FrameRateCounter(1.0f, 0f)); // TODO
            mainScreenlet.Add(new ScreenZoomer()); // TODO remove

            //level
            Gamelet level = new TrainwrecksLevel();
            mainScreenlet.Add(level);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            // update params, and call the root gamelet to do all.
            TTengineMaster.Update(gameTime, TreeRoot);

            // update any other XNA components
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // draw all my gamelet items
            TTengineMaster.Draw(gameTime, TreeRoot);

            // then draw other (if any) XNA game components on the screen
            base.Draw(gameTime);
        }

    }
}
