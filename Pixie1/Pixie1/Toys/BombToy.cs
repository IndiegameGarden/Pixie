using System;
using Pixie1.Actors;
using Microsoft.Xna.Framework;

namespace Pixie1.Toys
{
    /// <summary>
    /// a simple missile/bomb that drops down as if there was gravity
    /// </summary>
    public class BombToy: Toy
    {
        public BombToy()
        {
            UsedUponPickup = false;
        }

        public override string ToyName()
        {
            return "A BOMB";
        }

        public override void StartUsing()
        {
            base.StartUsing();
            BombMissile b = new BombMissile();
            Level.Current.AddNextUpdate(b);
            b.PositionAndTarget = Level.Current.pixie.Position + new Vector2(0f,1f);
            
        }

    }
}
