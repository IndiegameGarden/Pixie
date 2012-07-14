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
        /// <summary>
        /// target move delta for pixie or another controlled character
        /// </summary>
        public Vector2 TargetMove;

        /// <summary>
        /// if true can pass anything
        /// </summary>
        public bool IsGodMode = false;

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            // take steering input and move pixie
            PixieSpritelet pixie = (Parent as PixieSpritelet);
            Vector2 newPos = pixie.Target + TargetMove;
            bool isWalkable = Level.Current.bg.IsWalkable(newPos) || IsGodMode;
            if (isWalkable)
                pixie.Target += TargetMove;
            TTutil.Round(pixie.Target);

        }
    }
}
