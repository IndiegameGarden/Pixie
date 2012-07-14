using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using Microsoft.Xna.Framework;
using TTengine.Util;

namespace Pixie1
{
    public class PixieControl: Gamelet
    {
        public PixieSpritelet ParentPixie;

        /// <summary>
        /// target move delta for pixie or another controlled character
        /// </summary>
        public Vector2 TargetMove;

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

        }

        protected override void OnNewParent()
        {
            base.OnNewParent();
            ParentPixie = Parent as PixieSpritelet;
        }
    }
}
