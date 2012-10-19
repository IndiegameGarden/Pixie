using System;
using TTengine.Core;

namespace Pixie1.Behaviors
{
    /**
     * Subsumption-like architecture that activates the first child ThingControl which produces
     * output/control-signals.
     */
    public class SubsumptionBehavior: ThingControl
    {
        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            
            foreach (Gamelet c in Children)
            {
                if (c is ThingControl)
                {
                    ThingControl childControl = c as ThingControl;
                    if (childControl.IsTargetMoveDefined)
                    {
                        IsTargetMoveDefined = true;
                        TargetMove = childControl.TargetMove;
                        TargetMoveMultiplier = childControl.TargetMoveMultiplier;
                        break;
                    }
                }
            }
        }
    }
}
