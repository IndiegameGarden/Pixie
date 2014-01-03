
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
        public ScrollingComp(PositionComp positionToTrack, ScreenComp screenToScroll)
        {
            this.PositionToTrack = positionToTrack;
            this.ScreenToScroll = screenToScroll;
            Scrolling = new TargetVector(positionToTrack.Position);
        }

        public TargetVector Scrolling;
        public PositionComp PositionToTrack;
        public ScreenComp ScreenToScroll;
    }
}
