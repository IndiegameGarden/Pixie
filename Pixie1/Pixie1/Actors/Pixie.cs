using System;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Pixie1;

namespace Pixie1.Actors
{
    public class Pixie: Thing
    {

        public Pixie()
            : base()   //base("shape2x2")
        {
            DrawInfo.DrawColor = Level.PIXIE_COLOR;
        }

        protected override void OnDraw(ref DrawParams p)
        {
            base.OnDraw(ref p);
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
        }

    }
}
