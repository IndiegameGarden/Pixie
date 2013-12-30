using System;
using Microsoft.Xna.Framework;

namespace TTengine.Core
{
    /// <summary>
    /// A Vector3 (Current) that progresses towards a set Target with a set Speed
    /// TODO add modes for different motion patterns (linear, rel, fly, etc)
    /// </summary>
    public class TargetVector: IUpdate
    {
        public bool     IsActive = false;
        public Vector3  Target  = Vector3.Zero;
        public Vector3  Current = Vector3.Zero;
        public double   Speed   = 1;

        public void OnUpdate(double dt, double simTime=0)
        {
            if (!IsActive)
                return;
            Vector3 v = Target - Current;
            if (v.LengthSquared() > 0)
            {
                Vector3 vm = v;
                vm.Normalize();
                vm *= (float)(Speed * dt);
                if (vm.LengthSquared() > v.LengthSquared())
                {
                    // target reached
                    Current = Target;
                }
                else
                {
                    Current += vm;
                }
            }
        }

    }
}
