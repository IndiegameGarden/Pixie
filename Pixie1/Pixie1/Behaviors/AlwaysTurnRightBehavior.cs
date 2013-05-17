using System;
using TTengine.Core;
using Microsoft.Xna.Framework;

namespace Pixie1.Behaviors
{
    /**
     * always go forward and turn right when I can
     */
    public class AlwaysTurnRightBehavior: ThingControl
    {
        /// <summary>
        /// current direction of motion (e.g. along wall turning right). May be modified on the fly for sazzy effect.
        /// </summary>
        public Vector2 CurrentDirection = new Vector2(1f, 0f);

        // keep track of wall last seen
        protected bool didSeeWall = false;

        protected override void OnNextMove() 
        {
            base.OnNextMove();

            Vector2 rightHandDirection = RotateVector2(CurrentDirection, MathHelper.PiOver2);
            Vector2 leftHandDirection = RotateVector2(CurrentDirection, -MathHelper.PiOver2);
            bool isRightHandFree = !ParentThing.CollidesWithSomething(rightHandDirection);
            bool isLeftHandFree = !ParentThing.CollidesWithSomething(leftHandDirection);
            bool isFrontFree = !ParentThing.CollidesWithSomething(CurrentDirection);

            // change direction to righthand if that's free
            if (didSeeWall && isRightHandFree)
            {
                CurrentDirection = rightHandDirection;
                didSeeWall = false;
                TargetMove = CurrentDirection;
                IsTargetMoveDefined = true; 
            }

            else if (!isFrontFree)
            {
                // turn left if the way is blocked
                CurrentDirection = leftHandDirection;
                didSeeWall = true;
            }
            else if (didSeeWall || !isRightHandFree || !isLeftHandFree || !isFrontFree)
            {
                TargetMove = CurrentDirection;
                IsTargetMoveDefined = true; 
            }

            if (!isRightHandFree)
                didSeeWall = true;
        }

        public static Vector2 RotateVector2(Vector2 point, float radians)
        {
            float cosRadians = (float)Math.Cos(radians);
            float sinRadians = (float)Math.Sin(radians);

            return new Vector2(
                point.X * cosRadians - point.Y * sinRadians,
                point.X * sinRadians + point.Y * cosRadians);
        }

    }
}
