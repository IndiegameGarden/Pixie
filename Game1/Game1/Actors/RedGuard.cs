using System;
using TTengine.Core;
using TTengine.Comps;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Game1.Core;
using Game1.Comps;
using Game1.Behaviors;
using TreeSharp;
using Artemis;
using Artemis.Interface;

namespace Game1.Actors
{
    public class RedGuard
    {
        protected static string[] attackString = new string[] { "Take this, golden villain!", "We hurt him!", "He bleeds!", "Our swords struck true!",
            "He bleeds!", "To the grave, golden traitor!", "Die, golden scum!" , "He stumbles!"};

        /// <summary>
        /// Factory method to create new Red Guard
        /// </summary>
        /// <returns></returns>
        public static Entity Create()
        {
            ChaseBehavior  ChasingHero;
            //ChaseBehavior ChasingCompanions;
            AlwaysTurnRightBehavior Turning;
            RandomWanderBehavior Wandering;
            AttackEnemyBehavior Attacking;

            var e = GameFactory.CreateThing(ThingType.RED_GUARD,true);
            var ai = new BTAIComp();
            e.AddComponent(ai);
            var sub = new PrioritySelector();
            ai.rootNode = sub;

            var tc = e.GetComponent<ThingComp>();
            tc.IsCollisionFree = false;
            tc.Color = new Color(255, 10, 4);
            tc.Faction = Faction.EVIL;

            var rwc = new RandomWanderComp();
            e.AddComponent(rwc);
            rwc.MinDirectionChangeTime = 2.7;
            rwc.MaxDirectionChangeTime = 11.3;

            // attack hero or companions
            Attacking = new AttackEnemyBehavior(attackString);

            // chase companions that are very close
            /*
            ChasingCompanions = new ChaseBehavior(typeof(Companion));
            ChasingCompanions.DeltaTimeBetweenMoves = RandomMath.RandomBetween(0.43f, 0.65f);
            ChasingCompanions.ChaseRange = 1.5f; // RandomMath.RandomBetween(12f, 40f);
            sub.AddChild(ChasingCompanions);
            */

            // chase hero
            ChasingHero = new ChaseBehavior(Level.Current.Hero);
            ChasingHero.DeltaTimeBetweenMoves = RandomMath.RandomBetween(0.47f, 0.75f);
            ChasingHero.ChaseRange = 15f; // RandomMath.RandomBetween(12f, 40f);
            sub.AddChild(ChasingHero);

            Turning = new AlwaysTurnRightBehavior(); // patrolling
            Turning.DeltaTimeBetweenMoves = ChasingHero.DeltaTimeBetweenMoves; //RandomMath.RandomBetween(0.57f, 1.05f);
            Turning.DeltaTimeBetweenMoves = 0.7f;
            sub.AddChild(Turning);

            Wandering = new RandomWanderBehavior();
            Wandering.DeltaTimeBetweenMoves = 0.7f;
            sub.AddChild(Wandering);

            e.Refresh();
            return e;
        }

    }
}
