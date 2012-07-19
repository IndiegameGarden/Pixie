using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixie1.Toys
{
    /// <summary>
    /// a Toy that temporarily modifies speed of a Thing, faster or slower
    /// </summary>
    public class SpeedModifyToy: Toy
    {
        /// <summary>
        /// speedfactor can be modified on the fly
        /// </summary>
        public float SpeedFactor = 1f;

        public SpeedModifyToy()
        {
        }

        public SpeedModifyToy(float initialSpeedFactor)
        {
            this.SpeedFactor = initialSpeedFactor;
        }

        protected override void StartUsing()
        {
            base.StartUsing();
            ParentThing.Velocity *= SpeedFactor;
        }

        protected override void StopUsing()
        {
            base.StopUsing();
            ParentThing.Velocity /= SpeedFactor;
        }
    }
}
