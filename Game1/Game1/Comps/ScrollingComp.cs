
using Artemis.Interface;
using TTengine.Core;

namespace Game1.Comps
{
    /// <summary>
    /// Scrolls the Position of an Entity to track a given Vector
    /// </summary>
    public class ScrollingComp: IComponent
    {
        TargetVector Target;
    }
}
