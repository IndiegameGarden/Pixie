using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixie1.Toys
{
    /// <summary>
    /// makes you invisible (this can trigger a chasing Thing to lose its trail)
    /// </summary>
    public class InvisibilityToy: Toy
    {
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
