using System;
using TTengine.Core;
using Microsoft.Xna.Framework;
using Pixie1.Actors;

namespace Pixie1.Behaviors
{
    /**
     * when enabled, moves into the direction the leader of attack is facing
     */
    public class AttackBehavior: ThingControl
    {
        public Thing Leader = null;
        public float AttackDuration = 4f;
        public bool IsAttacking = false;
        public float CurrentAttackDuration = 0f;

        protected string[] attackString = new string[] { "Attack!", "Forward, men!", "Go!", "Companions!", "To arms!", "Attack!", 
            "Kill them!", "Cover me!", "Engage!", "Forward, companions!", "", "ATTACK!!", "Get me his head!", "Swords!", "Drive them back!",
            "Begone, thou knave!", "Red traitors, die!", "Easy... not too fast.", "Strike now!", "STRIKE!" ,
            "A thousand battles,\na thousand victories!", "ATTAAAAAACK!", "We shall prevail!", "Princess, all to your service!",
            "Squash the red vermin.", "Blue and Gold! For honor!", "For honor! For justice!"};

        public AttackBehavior(Thing leader)
        {
            Leader = leader;
        }

        public void TriggerAttack() {
            if (!IsAttacking)
            {
                if (Level.Current.Subtitles.Children.Count <= 3)
                    Level.Current.Subtitles.Show(0, attackString[RandomMath.RandomIntBetween(0,attackString.Length-1)], 1.5f);
                IsAttacking = true;
                CurrentAttackDuration = 0f;
            }
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            if (IsAttacking)
            {
                (ParentThing as Companion).ChasingHero.SatisfiedRange = 11f; // more range allowed during attack
                CurrentAttackDuration += p.Dt;
                if (CurrentAttackDuration > AttackDuration)
                {
                    IsAttacking = false;
                    CurrentAttackDuration = 0f;
                }
            }
            else
            {
                (ParentThing as Companion).ChasingHero.SatisfiedRange = 6f; // more range allowed during attack
            }
        }

        protected override void OnNextMove() 
        {
            base.OnNextMove();

            // only attack if not blocked there.
            if (IsAttacking && !ParentThing.CollidesWithBackground(Leader.FacingDirection))
            {
                TargetMove = Leader.FacingDirection;
                IsTargetMoveDefined = true;                
            }
        }

    }
}
