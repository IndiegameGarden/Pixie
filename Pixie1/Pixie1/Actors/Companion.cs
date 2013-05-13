using System;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Pixie1;
using Pixie1.Behaviors;

namespace Pixie1.Actors
{
    /**
     * companion of the hero that helps him
     */
    public class Companion: Thing
    {
        public ChaseBehavior  Chasing;
        public CombatBehavior Combat;     
        public RandomWanderBehavior Wandering;
        public AttackBehavior Attacking;

        public static Companion Create()
        {
            return new Companion();
        }
        
        public Companion()
            : base("pixie")
        {
            IsCollisionFree = false;
            
            SetColors(4f, new Color(38, 30, 240), new Color(150, 150, 255));

            Pushing.Force = RandomMath.RandomBetween(1f, 1.5f);

            SubsumptionBehavior sub = new SubsumptionBehavior();
            Add(sub);

            Combat = new CombatBehavior(typeof(RedGuard));
            sub.Add(Combat);

            Chasing = new ChaseBehavior(Level.Current.pixie);
            Chasing.ChaseRange = 370f;
            Chasing.SatisfiedRange = 6f;
            Chasing.MoveSpeed = RandomMath.RandomBetween(1.2f, 1.5f);
            sub.Add(Chasing);

            Chasing = new ChaseBehavior(typeof(RedGuard));
            Chasing.ChaseRange = 20f;
            Chasing.MoveSpeed = RandomMath.RandomBetween(1.1f, 1.5f);
            sub.Add(Chasing);

            Attacking = new AttackBehavior(Level.Current.pixie);
            Attacking.AttackDuration = RandomMath.RandomBetween(1.5f, 2.8f);
            sub.Add(Attacking);

            Wandering = new RandomWanderBehavior(2.7f, 11.3f);
            Wandering.MoveSpeed = RandomMath.RandomBetween(0.09f, 0.25f);
            sub.Add(Wandering);
            
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
        }
    }
}
