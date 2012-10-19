
using Microsoft.Xna.Framework;
using TTengine.Core;

namespace Pixie1.Behaviors
{
    /**
     * a simple ThingControl that reverses the direction computed for the Thing.
     * Only works if called last in a sequence of ThingControls. Acts directly
     * on ParentThing move vector.
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
