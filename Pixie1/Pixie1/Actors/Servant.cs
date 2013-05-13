using System;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Pixie1;
using Pixie1.Behaviors;

namespace Pixie1.Actors
{
    public class Servant: Thing
    {
        public ChaseBehavior Avoiding;
        public RandomWanderBehavior Wandering;
        public ReverseControlBehavior Reverse;

        public static Servant Create()
        {
            return new Servant();
        }

        public Servant()
            : base("pixie")
        {
            IsCollisionFree = false;
            Pushing.Force = 0f; // servants cant push others.
            DrawInfo.DrawColor = Color.Yellow;

            // avoid other things
            Avoiding = new ChaseBehavior(typeof(Companion));
            Avoiding.MoveSpeed = RandomMath.RandomBetween(0.43f, 0.65f);
            Avoiding.ChaseRange = 11f; // RandomMath.RandomBetween(12f, 40f);
            Add(Avoiding);

            // avoid other things
            Avoiding = new ChaseBehavior(typeof(Pixie));
            Avoiding.MoveSpeed = RandomMath.RandomBetween(0.43f, 0.65f);
            Avoiding.ChaseRange = 11f; // RandomMath.RandomBetween(12f, 40f);
            //Avoiding.Avoidance = true;
            Add(Avoiding);

            Wandering = new RandomWanderBehavior(9.7f, 14.3f);
            Wandering.MoveSpeed = 0.3f;
            Add(Wandering);

            Reverse = new ReverseControlBehavior();
            Add(Reverse);
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            /*
            if (TargetMove.LengthSquared() > 0)
            {
                if (CollidesWhenThisMoves(Level.Current.pixie, TargetMove))
                {
                    if (Level.Current.Subtitles.Children.Count <= 4)
                    {
                        Level.Current.Subtitles.Show(3, attackString[RandomMath.RandomIntBetween(0, attackString.Length - 1)], 3.5f, Color.IndianRed);
                    }
                }
            }
             */
        }
    }
}
