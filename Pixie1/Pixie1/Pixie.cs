using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;

namespace Pixie1
{
    public class Pixie: Thing
    {
        
        public Pixie()
            : base("shape2x2")
        {
            DrawInfo.DrawColor = Level.PIXIE_COLOR;
            //DrawInfo.DrawColor = Color.White;
        }

        protected override void OnDraw(ref DrawParams p)
        {
            base.OnDraw(ref p);
        }
    }
}
