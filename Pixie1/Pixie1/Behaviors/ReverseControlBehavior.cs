
using TTengine.Core;

namespace Pixie1.Behaviors
{
    public class ReverseControlBehavior: ThingControl
    {
        public ReverseControlBehavior()
            : base()
        {
        }

        protected override void OnUpdate(UpdateParams p)
        {
            base.OnUpdate(ref p);
            ParentThing.TargetMove = -ParentThing.TargetMove;
        }
    }
}
