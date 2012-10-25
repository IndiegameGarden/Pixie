using System;
using System.Collections.Generic;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pixie1;
using Pixie1.Actors;
using Pixie1.Toys;

namespace Pixie1.Levels
{
    public class AmazingLevel : Level
    {
        int numberOfZoomOuts = 0;
        Vector2 WINNING_POSITION = new Vector2(192f, 2f);
        float timeInWinningPos = 0f;
        bool hasWon = false;

        public AmazingLevel()
            : base()
        {           
            // Level settings
            SCREEN_MOTION_SPEED = 8.0f;
            DEFAULT_SCALE = 20f;
            PIXIE_STARTING_POS = new Vector2(3f, 51f); // in pixels        
            BG_STARTING_POS = new Vector2(3f, 51f); // in pixels; bg=background            
            //PIXIE_STARTING_POS = new Vector2(188f, 0f); // close to win pos
            //BG_STARTING_POS = new Vector2(188f, 0f); 
        }

        class MusicCreditsToy : ShowMessageToy
        {
            public MusicCreditsToy(): base(1,new SubtitleText())
            {                
                Message.AddText("Music: Nostalgika by\nTorley! CC BY-NC-SA.\nmusic.torley.com", 5f);
                Message.AddText("and also...", 3f);
                Message.AddText("B01 from the album \"mini\"\nby BERTIN! CC BY-NC-SA.\nbertin.bandcamp.com", 5f);
                Message.AddText("I hope the artists aren't\n     mad at me, now.", 4f);
                SetColors(3.34f, Color.DarkGoldenrod, Color.LightYellow);
            }

        }

        protected override void InitLevel()
        {
            base.InitLevel();

            // select bitmap bg
            Background = new LevelBackground("amazing1.png");
            Background.TargetSpeed = SCREEN_MOTION_SPEED;
            Add(Background);
            Background.Target = PIXIE_STARTING_POS;
            Background.Position = BG_STARTING_POS;

        }

        protected override void InitBadPixels()
        {
            base.InitBadPixels();

            BadPixel bp = BadPixel.Create(); // Cloaky();
            bp.PositionAndTarget = new Vector2(72f, 34f);
            //bp.TargetSpeed = 18.0f; // TODO
            Add(bp);

            bp = BadPixel.Create();
            bp.PositionAndTarget = new Vector2(37f, 44f);
            Add(bp);
        }

        protected override void InitToys()
        {
            base.InitToys();

            for (int i = 0; i < 20; i++)
            {
                Toy test = new SpeedModifyToy(2f);
                Vector2 p = PIXIE_STARTING_POS + new Vector2(RandomMath.RandomBetween(10f, 50f), RandomMath.RandomBetween(-40f, 40f));
                test.PositionAndTarget = p;
                Add(test);
            }

            // music credits object
            Toy musicMsg = new MusicCreditsToy();
            musicMsg.PositionAndTarget = new Vector2(2f, 36f);            
            Add(musicMsg);

            Toy invisToy = new InvisibilityToy();
            invisToy.PositionAndTarget = PIXIE_STARTING_POS + new Vector2(20f, 0f);
            Add(invisToy);

            // attach test
            //test.AttachmentPosition = new Vector2(3f, 0f);
            //pixie.Add(test);
        }

        protected override void InitLevelSpecific()
        {
            Music = new GameMusic();
            Add(Music);

            SubtitleText t = new SubtitleText();
            t.AddText("Don't you just hate\ntitle screens?", 4f);
            t.AddText("Me too.", 3f);
            t.AddText("", 2f);
            t.AddText("So, let's get started now!", 3f);
            t.AddText("Push the arrows to\nhelp me a bit.", 4f);
            t.StartTime = 2f;
            Subtitles.Show(0, t);
        }

        protected override bool ScreenBorderHit()
        {
            if (numberOfZoomOuts < 0)
            {
                numberOfZoomOuts++;
                Motion.Scale /= 2.0f;
                //Motion.ScaleTarget /= 2.0f;
                //Motion.ScaleSpeed = 0.2f;
                return false;
            }
            return true;
        }

        /// <summary>
        /// called when player wins game
        /// </summary>
        protected void PixieHasWon()
        {
            SubtitleText t = new SubtitleText();

            t.AddText("We DID IT!", 3f);
            t.AddText("I found my friends!", 3f);
            t.AddText("Hi, Trixie!", 3f);
            t.AddText("Hi, Dixie!", 3f);
            t.AddText("Hi, everyone!", 3f);
            t.AddText("This is...", 3f);
            t.AddText("it's...", 2f);
            t.AddText("AMAZING!!!", 9f);
            t.AddText("", 9f);             
            t.AddText("And now, bring...", 3f);
            t.AddText("...me back to the entrance!", 4f);
            t.AddText("", 1f);
            t.AddText("JUST KIDDING :-)",3f);
            t.AddText("", 7f);
            t.AddText("See you in another\ngreat Pixie game:", 3f);
             
            const float showTitleDuration = 5.5f;
            t.AddText("Pixie: Escape from the\n  Game Art Graveyard", showTitleDuration);
            t.AddText("         Pixie vs.\nThe Shader Legions", showTitleDuration);
            t.AddText("Pixie Goes Retro", showTitleDuration);
            t.AddText(" Pixie's Journey\nto the Cuda Core", showTitleDuration);
            t.AddText("Pixie and the City", showTitleDuration);
            Subtitles.Show(10, t);
            //Motion.ScaleTarget = 3f;
            //Motion.ScaleSpeed = 0.04f;
            Motion.ZoomTarget = 0.15f;
            Motion.ZoomSpeed = 0.0007f;
            Motion.ZoomCenterTarget = pixie.Motion;
            //Motion.ZoomCenter = Screen.Center;
            Background.Target = new Vector2(Background.Texture.Width / 2, Background.Texture.Height / 2);
            Background.TargetSpeed = 0.5f;
            Background.Velocity = 0.51f;
            hasWon = true;
            isBackgroundScrollingOn = false;
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            // adapt scroll speed to how fast pixie goes
            Background.TargetSpeed = SCREEN_MOTION_SPEED * pixie.Velocity;

            if (pixie.Target.Equals(WINNING_POSITION))
            {
                timeInWinningPos += p.Dt;

                if (timeInWinningPos > 2f && !hasWon)
                {
                    PixieHasWon();
                }
            }else{
                timeInWinningPos = 0f;
            }
        }
    }
}
