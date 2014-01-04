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
        /// <summary>
        /// Create with Vector3.Zero initial values
        /// </summary>
        public TargetVector()
        {
        }

        /// <summary>
        /// Create with given initial values
        /// </summary>
        /// <param name="initialValue">Value for Target and Current</param>
        public TargetVector(Vector3 initialValue)
        {
            Target = initialValue;
            Current = initialValue;
        }

        public bool     IsActive = true;
        public Vector3  Target  = Vector3.Zero;
        public Vector3  Current = Vector3.Zero;
        public double   Speed   = 10;

        public void AddToTarget(Vector2 v)
        {
            Target.X += v.X;
            Target.Y += v.Y;
        }

        public void AddToTarget(Vector3 v)
        {
            Target += v;
        }

        public Vector2 Target2D
        {
            get { return new Vector2(Target.X, Target.Y); }
            set
            {
                Target.X = value.X;
                Target.Y = value.Y;
            }
        }

        public Vector2 Current2D
        {
            get { return new Vector2(Current.X, Current.Y); }
            set
            {
                Current.X = value.X;
                Current.Y = value.Y;
            }
        }

        public void OnUpdate(double dt, double simTime = 0)
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
