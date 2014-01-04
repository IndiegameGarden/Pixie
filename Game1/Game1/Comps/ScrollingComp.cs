
using Microsoft.Xna.Framework;

using Artemis.Interface;
using TTengine.Core;
using TTengine.Comps;

namespace Game1.Comps
{
    /// <summary>
    /// Scrolls the ZoomCenter of a Screen to track a Position of given Entity
    /// </summary>
    public class ScrollingComp: IComponent
    {
        public ScrollingComp(Vector2 initialPosition)
        {
            Scrolling = new TargetVector(initialPosition);
        }

        public TargetVector Scrolling;

    }
}
