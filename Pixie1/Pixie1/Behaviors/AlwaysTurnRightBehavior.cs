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
        Vector2 currentDirection = new Vector2(1f, 0f);

        // waiting time before a next move is taken
        float wTime = 0f;
        bool didSeeWall = false;

        /// <summary>
        /// relative speed of chasing target
        /// </summary>
        public float MoveSpeed = 1.0f;

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            wTime += p.Dt;
            if (wTime >= 0.2f / MoveSpeed)
                wTime = 0f;

            Vector2 rightHandDirection = RotateVector2(currentDirection, MathHelper.PiOver2);
            Vector2 leftHandDirection = RotateVector2(currentDirection, -MathHelper.PiOver2);
            bool isRightHandFree = !ParentThing.CollidesWithBackground(rightHandDirection);
            bool isLeftHandFree = !ParentThing.CollidesWithBackground(leftHandDirection);
            bool isFrontFree = !ParentThing.CollidesWithBackground(currentDirection);

            if (wTime == 0f)
            {
                // change direction to righthand if that's free
                if (didSeeWall && isRightHandFree)
                {
                    currentDirection = rightHandDirection;
                    didSeeWall = false;
                    TargetMove = currentDirection;
                    IsTargetMoveDefined = true; 
                }

                else if (!isFrontFree)
                {
                    // turn left if the way is blocked
                    currentDirection = leftHandDirection;
                    didSeeWall = true;
                }
                else if (didSeeWall || !isRightHandFree || !isLeftHandFree || !isFrontFree)
                {
                    TargetMove = currentDirection;
                    IsTargetMoveDefined = true; 
                }

                if (!isRightHandFree)
                    didSeeWall = true;
            }
            // in case that it's not time to make a move, keep this ThingControl active if near walls
            else if (didSeeWall || !isRightHandFree || !isLeftHandFree || !isFrontFree )
                IsTargetMoveDefined = true; 
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
