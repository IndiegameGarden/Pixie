using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using Microsoft.Xna.Framework;

namespace Pixie1
{
    public class PixieControl: Gamelet
    {
        /// <summary>
        /// target move delta for pixie or another controlled character, constructed by reading user input keys
        /// </summary>
        public Vector2 TargetMove;

    }
}
