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
            IsMoveActive = false;

            foreach (Gamelet c in Children)
            {
                if (c is ThingControl)
                {
                    ThingControl childControl = c as ThingControl;
                    if (childControl.IsTargetMoveDefined)
                    {
                        IsTargetMoveDefined = true;
                        IsMoveActive = true;
                        TargetMove = childControl.TargetMove;
                        TargetMoveMultiplier = childControl.TargetMoveMultiplier;
                        break;
                    }
                    // if a THingControl is still active (due to a previous TargetMove), then keep to the input
                    // of that control, until the 'lock' is released from it.
                    if (childControl.IsMoveActive)
                    {
                        IsMoveActive = true;
                        break;
                    }
                }
            }
        }
    }
}
