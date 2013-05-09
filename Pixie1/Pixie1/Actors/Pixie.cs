using System;
using System.Collections.Generic;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Pixie1;
using Pixie1.Behaviors;

namespace Pixie1.Actors
{
    public class Pixie: Thing
    {

        public List<Companion> Companions = new List<Companion>();

        public Pixie()
            : base("pixie")
        {            
            IsCollisionFree = false;
            SetColors(4f, Color.DarkGoldenrod, new Color(230, 210, 10));
            Velocity = 1.5f;

            //SubsumptionBehavior sub = new SubsumptionBehavior();
            //Add(sub);

            Pushing.Force = 10f; // force higher than companions.

        }

        public void LeadAttack()
        {
            foreach (Companion c in Companions)
                c.Attacking.TriggerAttack();
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
