
using Microsoft.Xna.Framework;
using TTengine.Core;

namespace Pixie1.Behaviors
{
    /**
     * a control that reverses the direction computed (by other controls) for the Thing.
     */
    public class ReverseControlBehavior: ThingControl
    {
        public ReverseControlBehavior()
            : base()
        {
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            TargetMoveMultiplier = new Vector2(-1f, -1f);
            IsTargetMoveDefined = true;
        }
    }
}
