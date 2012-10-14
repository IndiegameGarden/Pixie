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
            SetColors(1.9f, Color.LightSkyBlue, new Color(1f,1f,1f,0f));
        }

        protected override void StartUsing()
        {
            base.StartUsing();
            ParentThing.Visible = false;
        }

        protected override void StopUsing()
        {
            base.StopUsing();
            ParentThing.Visible = true;
        }
    }
}
