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
    /// <summary>
    /// a level featuring a big maze
    /// </summary>
    public class QuestLevel : Level
    {
        Vector2 WINNING_POSITION = new Vector2(73f, 7f);
        Color LEVEL_FOREGROUND_COLOR = new Color(231, 231, 248);
        float timeInWinningPos = 0f;
        bool hasWon = false;
        int numberOfZoomOuts = 0;

        public QuestLevel()
            : base()
        {           
            // Level settings
            SCREEN_MOTION_SPEED = 8.0f;
            DEFAULT_SCALE = 15f;// 15f;
            PIXIE_STARTING_POS = new Vector2(42f, 155f); // in pixels        
            //PIXIE_STARTING_POS = new Vector2(73f, 10f); // in pixels        
            BG_STARTING_POS = new Vector2(30f, 155f); // in pixels; bg=background            
            //PIXIE_STARTING_POS = new Vector2(188f, 0f); // close to win pos
            //BG_STARTING_POS = new Vector2(188f, 0f); 
        }

        protected override void InitLevel()
        {
            base.InitLevel();

            // select bitmap bg
            Background = new LevelBackground("Level1.png");
            Background.ForegroundColor = LEVEL_FOREGROUND_COLOR;
            Background.TargetSpeed = SCREEN_MOTION_SPEED;
            Add(Background);
            Background.Target = PIXIE_STARTING_POS;
            Background.Position = BG_STARTING_POS;
        }

        protected override void InitBadPixels()
        {
            base.InitBadPixels();

            for (int i = 0; i < 99; i++)
            {
                BadPixel bp = BadPixel.Create(); // Cloaky();
                bp.PositionAndTarget = new Vector2(RandomMath.RandomBetween(0f,740f), RandomMath.RandomBetween(0f,300f) );
                //bp.TargetSpeed = 18.0f; // TODO
                Add(bp);
                //FindWalkableGround(bp);
            }

            for (int i = 0; i < 14; i++)
            {
                Companion cp = Companion.Create(); // Cloaky();
                cp.PositionAndTarget = new Vector2(RandomMath.RandomBetween(PIXIE_STARTING_POS.X - 10f, PIXIE_STARTING_POS.X + 10f), RandomMath.RandomBetween(PIXIE_STARTING_POS.Y - 6f, PIXIE_STARTING_POS.Y + 6f));
                //bp.TargetSpeed = 18.0f; // TODO
                Add(cp);
                //FindWalkableGround(cp);
            }

        }

        protected void FindWalkableGround(Thing t)
        {
            // move down until on walkable area
            while(t.CollidesWithSomething(Vector2.Zero)) {
                t.PositionY += 1;
                // when unit goes off-screen, delete.
                if (t.PositionY > this.Background.Texture.Height)
                {
                    t.Delete = true;
                    break;
                }
            }
        }

        protected override void InitToys()
        {
            base.InitToys();
            Vector2 p;
            Toy t;

            t = new ZoomOutToy(); p = new Vector2(70f,48f); t.PositionAndTarget = p; Add(t);
            t = new ZoomOutToy(); p = new Vector2(93f, 9f); t.PositionAndTarget = p; Add(t);            
            t = new ZoomOutToy(); p = new Vector2(17f, 13f); t.PositionAndTarget = p; Add(t);
            t = new ZoomOutToy(); p = new Vector2(117f, 29f); t.PositionAndTarget = p; Add(t);
            t = new ZoomOutToy(); p = new Vector2(73f, 86f); t.PositionAndTarget = p; Add(t);
            t = new SpeedModifyToy(2f); p = new Vector2(61f, 9f); t.PositionAndTarget = p; Add(t);
            t = new SpeedModifyToy(2f); p = new Vector2(109f, 65f); t.PositionAndTarget = p; Add(t);
            t = new ZoomOutToy(); p = new Vector2(121f, 13f); t.PositionAndTarget = p; Add(t);
            t = new SpeedModifyToy(2f); p = new Vector2(46f, 30f); t.PositionAndTarget = p; Add(t);            
            t = new SpeedModifyToy(2f); p = new Vector2(13f, 93f); t.PositionAndTarget = p; Add(t);
            t = new SpeedModifyToy(2f); p = new Vector2(121f, 94f); t.PositionAndTarget = p; Add(t);
            t = new ZoomOutToy(); p = new Vector2(29f, 113f); t.PositionAndTarget = p; Add(t);
            t = new SpeedModifyToy(2f); p = new Vector2(53f, 117f); t.PositionAndTarget = p; Add(t);
            t = new ZoomOutToy(); p = new Vector2(105f, 98f); t.PositionAndTarget = p; Add(t);
        }

        protected override void InitLevelSpecific()
        {
            Music = new GameMusic();
            Add(Music);

            SubtitleText t = new SubtitleText();
            t.AddText("QUEST FOR THE\nPIXEL PRINCESS XIV", 6f);
            t.AddText("You, The Golden Knight, must\nrescue the princess...", 5f);
            t.AddText("...once more...", 5f);
            t.AddText("...from the clutches of\nthe evil Red Guard.", 5f);
            //t.AddText("...from the clutches of\nthe evil Red Guard.", 5f);
            Subtitles.Show(0, t);

            t = new SubtitleText("My ears! Hark the drums\nof the Red Guard!");
            t.StartTime = 64.0f;
            t.Duration = 10.0f;
            Subtitles.Show(0, t);

            t = new SubtitleText();
            t.AddText("Music 'The Never Ending Quest For The\nPrincess II' by Producer Snafu!\nCC-BY-NC  producersnafu.bandcamp.com", 10f);
            t.AddText("FMOD Audio engine\n(c) Firelight Technologies Pty, Ltd. 2004-2009.", 6f);
            //t.Duration = 10f;
            Parent.Add(t);
            t.ScaleVector = new Vector2(1f, 1f);
            t.Motion.Scale = 0.4f ;
            t.Motion.Position = new Vector2(0.35f,0.25f);
            //t.DrawInfo.Center = Vector2.Zero;
            t.StartTime = 17f;
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
            t.AddText("YOU WIN!", 5f);
            t.AddText("The princess\nis rescued.", 4f);
            t.AddText("", 2f);
            t.AddText("But wait a minute...", 3f);
            t.AddText("How do we get out??", 3f);
            t.AddText("*THE END*", 3f);
            Subtitles.Show(6, t);
            hasWon = true;            
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            // adapt scroll speed to how fast pixie goes
            Background.TargetSpeed = SCREEN_MOTION_SPEED * pixie.Velocity;

            if (pixie.Target.Equals(WINNING_POSITION))
            {
                timeInWinningPos += p.Dt;

                if (timeInWinningPos > 0.2f && !hasWon)
                {
                    PixieHasWon();
                }
            }else{
                timeInWinningPos = 0f;
            }
        }
    }
}
