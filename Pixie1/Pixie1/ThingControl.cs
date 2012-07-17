using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using Microsoft.Xna.Framework;
using TTengine.Util;

namespace Pixie1
{
    public class ThingControl: Gamelet
    {
        public Thing ParentThing;

        protected override void OnNewParent()
        {
            base.OnNewParent();
            ParentThing = Parent as Thing;
        }
    }
}
