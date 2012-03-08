using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pixie1
{
    public class FinalScreen: Drawlet
    {
        SubtitleText finalText;
        PixieLogo pixieLogo;
        Spritelet bg;

        string[] TXT = new string[] {   "Thanks  to  you,   Pixie  found  her  family.\n\n\n" ,
                                        "\nWatch  the  net  for  more  great  Pixie  games!\n",
                                         "    Pixie  Redrawn",
    "          Pixie:  Lost  in  the  Game  Art  Graveyard",
    "                Pixie  and  the  Shader  Legions",
    "                      Pixie  and  the  City"
                                    };
        float[] TXT_TIMINGS = new float[] { 3f, 5f, 7f, 9f, 11f, 13f, 15f};

        public FinalScreen()
        {
            finalText = new SubtitleText(TXT,TXT_TIMINGS, false);
            finalText.SubtitleFont = TTengineMaster.ActiveGame.Content.Load<SpriteFont>("m06_quadra");
            finalText.Motion.Position = new Vector2(0.1f, 0.32f);
            finalText.ScaleVector = new Vector2(1f, 1f);
            finalText.Shadow = false;
            Add(finalText);

            pixieLogo = new PixieLogo("finallogo");
            Add(pixieLogo);
            pixieLogo.Motion.Scale = 1f;
            //pixieLogo.bg.Motion.Scale = 2f;
            pixieLogo.Motion.Position = new Vector2(0.666f, 0.1f);
            pixieLogo.StartTime = 0f;

            bg = new RotatingBackground("psych");
            bg.Motion.Scale = 20.0f;
            Add(bg);

            this.Duration = 74f;
        }
    }
}
