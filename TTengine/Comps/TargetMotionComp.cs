// (c) 2010-2013 TranceTrance.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Artemis.Interface;
using TTengine.Core;

namespace TTengine.Comps
{
    /// <summary>
    /// Let an entity move its position towards a set target
    /// </summary>
    public class TargetMotionComp : IComponent
    {
        public TargetVector Target = new TargetVector();
    }
}
