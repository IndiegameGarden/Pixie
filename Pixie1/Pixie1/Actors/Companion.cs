using System;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Pixie1;
using Pixie1.Behaviors;

namespace Pixie1.Actors
{
    public class Companion: Thing
    {
        public BlinkBehavior Blinking;
        public ChaseBehavior  Chasing;
        public AlwaysTurnRightBehavior Turning;
        public RandomWanderBehavior Wandering;

        public static Companion Create()
        {
            return new Companion();
        }

        bool isCloaky = false;

        public Companion()
            : base("pixie")
        {
            IsCollisionFree = false;
            
            //DrawInfo.DrawColor = new Color(220, 200, 14);
            SetColors(4f, new Color(38, 30, 240), new Color(150, 150, 255));

            Pushing.Force = RandomMath.RandomBetween(1f, 1.5f);

            SubsumptionBehavior sub = new SubsumptionBehavior();
            Add(sub);

            Chasing = new ChaseBehavior(Level.Current.pixie);
            Chasing.ChaseRange = 70f;
            Chasing.SatisfiedRange = 11f;
            Chasing.MoveSpeed = RandomMath.RandomBetween(1.2f, 1.5f);
            sub.Add(Chasing);

            Chasing = new ChaseBehavior(typeof(BadPixel));
            Chasing.ChaseRange = 20f;
            Chasing.MoveSpeed = RandomMath.RandomBetween(0.95f, 1.3f);
            sub.Add(Chasing);

            Wandering = new RandomWanderBehavior(2.7f, 11.3f);
            Wandering.MoveSpeed = 0.05f;
            //sub.Add(Wandering);
            
        }

        /// <summary>
        /// set 'cloaky' status, a cloaky is a hardly visible bad pixel
        /// </summary>
        public bool IsCloaky
        {
            get
            {
                return isCloaky;
            }
            set
            {
                if (IsCloaky == value)
                    return;
                // if change - swap dutycycle
                Blinking.DutyCycle = 1f - Blinking.DutyCycle;
                isCloaky = value;                
            }
        }
        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            if (TargetMove.LengthSquared() > 0)
            {
                if (CollidesWhenThisMoves(Level.Current.pixie, TargetMove))
                {
                    if (Level.Current.Subtitles.Children.Count <= 2)
                    {
                        Level.Current.Subtitles.Show(3, "HALT! Thou shalt\n   not pass.", 3.5f);
                    }
                }
            }
        }
    }
}
