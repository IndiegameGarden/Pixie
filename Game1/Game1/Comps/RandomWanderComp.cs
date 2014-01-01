using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TTengine.Core;
using TTengine.Util;

namespace Game1.Comps
{
    public class RandomWanderComp: ThingControlComp
    {
        public RandomWanderComp()
        {
        }

        /// <summary>current random direction of wandering - may be changed by an external object</summary>
        public Vector2 CurrentDirection = Vector2.Zero;

        /// <summary>The max for the random time it takes to change direction</summary>
        public double MaxDirectionChangeTime = 10;

        /// <summary>The min for the random time it takes to change direction</summary>
        public double MinDirectionChangeTime = 2;

        /// <summary>The remaining time it will take, counted from this moment on, before doing a new change of direction</summary>
        public double CurrentDirectionChangeTime = 0;


    }
}
