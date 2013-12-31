
using Microsoft.Xna.Framework;

using Artemis.Interface;

namespace Game1.Comps
{
    public class ControlComp: IComponent
    {
        public bool IsSteering = false;
        public Vector2 Move;
    }
}
