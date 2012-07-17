
using TTengine.Core;

namespace Pixie1.Behaviors
{
    public class ReverseControlBehavior: ThingControl
    {
        public ReverseControlBehavior()
            : base()
        {
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            ParentThing.TargetMove = -ParentThing.TargetMove;
        }
    }
}
