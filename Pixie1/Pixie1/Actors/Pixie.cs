using System;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Pixie1;
using Pixie1.Behaviors;

namespace Pixie1.Actors
{
    public class Pixie: Thing
    {

        public Pixie()
            : base("pixie")
        {            
            IsCollisionFree = false;
            SetColors(4f, Color.DarkGoldenrod, new Color(230, 210, 10));
            Velocity = 1.5f;

            SubsumptionBehavior sub = new SubsumptionBehavior();
            Add(sub);

            Pushing.Force = 10f; // force higher than companions.

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
