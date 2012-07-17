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
        public Thing ParentPixie;

        protected override void OnNewParent()
        {
            base.OnNewParent();
            ParentPixie = Parent as Thing;
        }
    }
}
