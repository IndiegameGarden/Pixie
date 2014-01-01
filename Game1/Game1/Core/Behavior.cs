using System;
using System.Collections.Generic;
using TreeSharp;

namespace Game1.Core
{
    /// <summary>
    /// Base class for all Behaviors, which are Behavior-Tree AI nodes that can be arranged in
    /// a BT (AI) to control an Entity.
    /// </summary>
    public abstract class Behavior: TreeNode
    {
        /// <summary>The time (seconds) between two successive moves for this Behavior</summary>
        public double DeltaTimeBetweenMoves = 0.2;
    }
}
