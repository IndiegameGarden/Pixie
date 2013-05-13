using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pixie1.Toys
{
    /// <summary>
    /// makes you invisible (this can trigger a chasing Thing to lose its trail)
    /// </summary>
    public class InvisibilityToy: Toy
    {
        public InvisibilityToy(): base()
        {
            UseTimeMax = 16f;
            SetColors(1.9f, Color.LightSkyBlue, new Color(1f,1f,1f,0f));
        }

        public override string ToyName()
        {
            return "Clock of Transparaence";
        }

        public override void StartUsing()
        {
            base.StartUsing();
            ParentThing.Visible = false;
            Level.Current.Subtitles.Show(2, "Hey! Where am I?", 3f);
        }

        public override void StopUsing()
        {
            base.StopUsing();
            ParentThing.Visible = true;
            Level.Current.Subtitles.Show(0, "...and back!", 3f);
        }
    }
}
