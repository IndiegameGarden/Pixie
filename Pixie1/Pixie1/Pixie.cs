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
            : base("pixie")
        {
            DrawInfo.DrawColor = Level.PIXIE_COLOR;
        }

    }
}
