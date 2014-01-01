using System;
using System.Collections.Generic;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Game1.Core;
using Game1.Behaviors;

namespace Game1.Actors
{
    /**
     * a block that can be pushed around
     */
    public class Block: ThingComp
    {

        public Block()
            : base("pixie")
        {            
            IsCollisionFree = false;
            DrawInfo.DrawColor = Color.Brown;
        }

        public static Block Create() {
            return new Block();
        }
    }
}
