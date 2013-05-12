using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pixie1.Behaviors;

namespace Pixie1.Actors
{
    public class Boss: Thing
    {
        public ChaseBehavior Chasing;

        public Boss(): base("boss")
        {
            PositionAndTarget = new Microsoft.Xna.Framework.Vector2(523f, 298f);

            // chase hero
            Chasing = new ChaseBehavior(Level.Current.pixie);
            Chasing.MoveSpeed = RandomMath.RandomBetween(0.2f, 0.25f);
            Chasing.ChaseRange = 25f; // RandomMath.RandomBetween(12f, 40f);
            Add(Chasing);

            DrawInfo.Center = new Microsoft.Xna.Framework.Vector2(0.5f, 0.5f);

        }

    }
}
