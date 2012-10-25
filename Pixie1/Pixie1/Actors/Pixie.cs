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
            : base("pixie")   //base("shape2x2")
        {
            DrawInfo.DrawColor = Level.PIXIE_COLOR;
            //DrawInfo.DrawColor = Color.White;            
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
