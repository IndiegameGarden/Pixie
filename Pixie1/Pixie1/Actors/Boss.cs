using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pixie1.Behaviors;
using Microsoft.Xna.Framework;

namespace Pixie1.Actors
{
    public class Boss: Thing
    {
        public ChaseBehavior Chasing;

        public Boss(): base("boss")
        {
            PositionAndTarget = new Microsoft.Xna.Framework.Vector2(520f, 290f);

            // chase hero
            Chasing = new ChaseBehavior(Level.Current.pixie);
            Chasing.MoveSpeed = RandomMath.RandomBetween(0.18f, 0.22f);
            Chasing.ChaseRange = 26f; // RandomMath.RandomBetween(12f, 40f);
            Add(Chasing);

            DrawInfo.Center = new Microsoft.Xna.Framework.Vector2(0.5f, 0.5f);

        }

        protected override void OnUpdate(ref TTengine.Core.UpdateParams p)
        {
            base.OnUpdate(ref p);
            Pixie hero = Level.Current.pixie;
            Vector2 dif = (hero.Position - Position);
            if (dif.Length() < 8.5f)
            {
                dif.Normalize();
                Vector2 smiteVector = dif* p.Dt * 25f;
                hero.Target += smiteVector;
            }
        }

    }
}
