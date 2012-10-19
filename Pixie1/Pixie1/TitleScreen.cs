using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pixie1.Actors;

namespace Pixie1
{
    public class TitleScreen: Drawlet
    {
        public MotionBehavior MotionB;
        SubtitleText cuteText;
        Spritelet helpText;
        Spritelet bg;
        Thing creditsScreen;
        float timeEscDown = 0f;
        float timeCDown = 0f;
        float timeSpaceDown = 0f;
        const float DEFAULT_SCALE = 1f;
        const float TIME_LOGO_APPEAR = 9.09f;
        PixieLogo pixieLogo;
        Pixie pixie;
        GameMusic gameMusic;

        public TitleScreen()
        {
            bg = new RotatingBackground("psych");
            bg.Motion.Scale = 20.0f;
            bg.StartTime = 0f;// 9.09f;
            Add(bg);

            pixie = new Pixie();
            pixie.Motion.Position = new Vector2(0.666f, 0.0f);
            pixie.Motion.Scale = 20.0f;
            pixie.Duration = TIME_LOGO_APPEAR;
            Add(pixie);

            cuteText = new SubtitleText(new string[] { "She's pink...", "   She's cute...", "      She's all square!" }, 
                                        new float[]{ 1.5f, 3.5f, 5.5f }, 
                                        false );
            cuteText.Motion.Position = new Vector2(0.26f, 0.62f);
            cuteText.Duration = TIME_LOGO_APPEAR;
            Add(cuteText);

            /*
            helpText = new SubtitleText("SPACE  Play!\n  C         Credits\n ESC       Exit");
            helpText.StartTime = 13.5f;
            helpText.Motion.Scale = 0.7f;
            helpText.DrawInfo.DrawColor = Color.AntiqueWhite;
            helpText.Motion.Position = new Vector2(0.2f, 0.6f);
            Add(helpText);
             */
            helpText = new Spritelet("showControls");
            helpText.StartTime = TIME_LOGO_APPEAR;
            helpText.Motion.Scale = 1f;
            helpText.Motion.Position = new Vector2(0.666f, 0.85f);
            Add(helpText);


            pixieLogo = new PixieLogo("pixielogo");
            pixieLogo.Motion.Scale = 20.0f;
            pixieLogo.Motion.Position = new Vector2(0.666f, 0.35f);
            pixieLogo.StartTime = TIME_LOGO_APPEAR;
            Add(pixieLogo);

            MotionB = new MotionBehavior();
            Add(MotionB);

            creditsScreen = new Thing("credits.png");
            Add(creditsScreen);
            creditsScreen.Visible = false;

            /*
            ttLogo = new Spritelet("tt-logo-4");
            ttLogo.Motion.Position = new Vector2(1.05f, 0.02f);
            Add(ttLogo);
             */
        }

        protected override void OnNewParent()
        {
            base.OnNewParent();

            Parent.Add(gameMusic);
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
                if (creditsScreen.Visible)
                {
                    if (timeEscDown > 0.05f)
                    {
                        creditsScreen.Visible = false;
                        pixieLogo.Visible = true;
                        helpText.Visible = true;
                        timeEscDown = 0f;
                    }
                }
                if (!creditsScreen.Visible)
                {
                    MotionB.ScaleTarget = 0.7f * DEFAULT_SCALE;
                    MotionB.ScaleSpeed = 0.003f;
                    gameMusic.Volume = (1f - timeEscDown);
                }
            }
            else
            {
                timeEscDown = 0f;
                MotionB.ScaleTarget = DEFAULT_SCALE; // TODO
                MotionB.ScaleSpeed = 0.0045f;
                gameMusic.Volume = 1.0f;
            }
            if (timeEscDown > 1.0f && !creditsScreen.Visible)
                TTengineMaster.ActiveGame.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                timeCDown += p.Dt;
            }
            else if (timeCDown > 0.0f)
            {
                creditsScreen.Visible = !creditsScreen.Visible;
                pixieLogo.Visible = !pixieLogo.Visible;
                helpText.Visible = !helpText.Visible;
                timeCDown = 0f;
            }
            else
            {
                timeCDown = 0f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && SimTime > 0f*TIME_LOGO_APPEAR) // FIXME
            {
                timeSpaceDown += p.Dt;
            }
            else
            {
                if (timeSpaceDown > 0.0f)
                {
                    PixieGame.Instance.StartPlay();
                }
                timeSpaceDown = 0f;
            }

        }


    }
}
