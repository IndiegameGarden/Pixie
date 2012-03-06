using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pixie1
{
    public class TitleScreen: Drawlet
    {
        public MotionBehavior MotionB;
        SubtitleText cuteText, helpText;
        float timeEscDown = 0f;
        const float DEFAULT_SCALE = 1f;
        PixieLogo pixieLogo;
        Pixie pixie;
        GameMusic gameMusic;
        Spritelet ttLogo;

        public TitleScreen()
        {
            pixie = new Pixie();
            pixie.Motion.Position = new Vector2(0.666f, 0.0f);
            pixie.Motion.Scale = 20.0f;
            Add(pixie);

            cuteText = new SubtitleText(new string[] { "  She's pink,", "  she's cute,", "she's all pixel!" }, 
                                        new float[]{ 1.5f, 3.5f, 5.5f }, 
                                        false );
            cuteText.Motion.Position = new Vector2(0.35f, 0.6f);
            cuteText.Duration = 12.5f;
            Add(cuteText);

            helpText = new SubtitleText("SPACE  Play!\n  C         Credits\n ESC       Exit");
            helpText.StartTime = 13.5f;
            helpText.Motion.Scale = 0.7f;
            helpText.DrawInfo.DrawColor = Color.AntiqueWhite;
            helpText.Motion.Position = new Vector2(0.2f, 0.6f);
            Add(helpText);


            pixieLogo = new PixieLogo();
            pixieLogo.Motion.Scale = 20.0f;
            pixieLogo.Motion.Position = new Vector2(0.65f, 0.4f);
            pixieLogo.StartTime = 8.9f;
            Add(pixieLogo);

            MotionB = new MotionBehavior();
            Add(MotionB);

            gameMusic = new GameMusic();
            Add(gameMusic);

            ttLogo = new Spritelet("tt-logo-4");
            ttLogo.Motion.Position = new Vector2(1.05f, 0.02f);
            Add(ttLogo);
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            LevelKeyControl(ref p);
        }

        /// check keys specific for level
        protected virtual void LevelKeyControl(ref UpdateParams p)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                timeEscDown += p.Dt;
                MotionB.ScaleTarget = 0.7f * DEFAULT_SCALE;
                MotionB.ScaleSpeed = 0.003f;
                gameMusic.Volume = (1f - timeEscDown);
            }
            else
            {
                timeEscDown = 0f;
                MotionB.ScaleTarget = DEFAULT_SCALE; // TODO
                MotionB.ScaleSpeed = 0.0045f;
                gameMusic.Volume = 1.0f;
            }
            if (timeEscDown > 1.0f)
                TTengineMaster.ActiveGame.Exit();

        }


    }
}
