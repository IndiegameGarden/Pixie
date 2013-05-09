using System;
using TTengine.Core;
using Microsoft.Xna.Framework;

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

        public AttackBehavior(Thing leader)
        {
            Leader = leader;
        }

        public void TriggerAttack() {
            if (!IsAttacking)
            {
                if (Level.Current.Subtitles.Children.Count == 0)
                    Level.Current.Subtitles.Show(4, "Attack!", 1.5f);
                IsAttacking = true;
                CurrentAttackDuration = 0f;
            }
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            if (IsAttacking) {
                CurrentAttackDuration += p.Dt;
                if (CurrentAttackDuration > AttackDuration) {
                    IsAttacking = false;
                    CurrentAttackDuration = 0f;
                }
                AllowNextMove();
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
