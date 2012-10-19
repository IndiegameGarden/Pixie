using System;
using System.Collections.Generic;
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
using TTMusicEngine;
using TTMusicEngine.Soundevents;
using Pixie1.Levels;
using Pixie1.Actors;

namespace Pixie1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PixieGame : Game
    {
        public Gamelet TreeRoot;
        private static PixieGame instance = null;
        GraphicsDeviceManager graphics;
        int myWindowWidth = 1024;
        int myWindowHeight = 768; 
        PixieScreenlet mainScreenlet;
        MusicEngine musicEngine;
        Level level;

        public PixieGame()
        {
            instance = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = myWindowHeight;
            graphics.PreferredBackBufferWidth = myWindowWidth;
            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;
        }

        public static PixieGame Instance
        {
            get
            {
                return instance;
            }
        }

        protected override void Initialize()
        {
            TTengineMaster.Create(this);

            // open the TTMusicEngine
            musicEngine = MusicEngine.GetInstance();
            musicEngine.AudioPath = "Content";
            if (!musicEngine.Initialize())
                throw new Exception(musicEngine.StatusMsg);

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
            //mainScreenlet.Add(new FrameRateCounter(1.0f, 0f)); // TODO

            //level = new TrainwrecksLevel();
            level = new AmazingLevel();

            mainScreenlet.Add(level);

            base.LoadContent();
        }

        public void StartPlay()
        {
        }

        public void StopPlay()
        {
        }

        public void WinGame()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            TTengineMaster.Update(gameTime, TreeRoot);

            // update any other XNA components
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            TTengineMaster.Draw(gameTime, TreeRoot);

            // then draw other (if any) XNA game components on the screen
            base.Draw(gameTime);
        }

    }
}
