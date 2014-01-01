using System;
using System.Collections.Generic;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Game1.Core;
using Game1.Behaviors;
using Game1.Comps;
using Artemis;

namespace Game1.Actors
{
    public class Pixie
    {
        public static Entity Create()
        {
            var e = GameFactory.Instance.CreateThing(ThingType.HERO,true);
            var tc = e.GetComponent<ThingComp>();
            var tcc = e.GetComponent<ControlComp>();

            tc.IsCollisionFree = false;
            tc.Velocity = 1.5f;            
            tcc.PushForce = 10f; // force higher than companions.
            return e;
        }

    }
}
