using System;
using Microsoft.Xna.Framework;

using Artemis;
using TTengine.Core;
using TTengine.Util;

using Game1.Core;
using Game1.Behaviors;

namespace Game1.Actors
{
    /// <summary>
    /// a block that can be pushed around
    /// </summary>
    public class Block
    {

        public static Entity Create()
        {
            Entity e = GameFactory.Instance.CreateThing(Comps.ThingType.OTHER, true);
            return e;
        }

    }
}
