using System;
using System.Collections.Generic;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Pixie1;
using Pixie1.Behaviors;

namespace Pixie1.Actors
{
    /**
     * the pixel princess
     */
    public class Princess: Thing
    {
        public bool isFollowHero = false;
        public ChaseBehavior Following;

        public Princess()
            : base("pixie")
        {            
            IsCollisionFree = false;
            DrawInfo.DrawColor = Color.Pink;
            Following = new ChaseBehavior(Level.Current.pixie);
            Following.Active = false;
            Following.SatisfiedRange = 3f;
            Following.ChaseRange = 20f;
        }

        protected override void OnNewParent()
        {
            base.OnNewParent();
            Add(Following);
        }
        
        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            // enable the following behavior
            if (isFollowHero)
            {
                Following.Active = true;
            }

            // check when to start following our rescuer
            float dist = (Level.Current.pixie.Position - Position).Length();
            if (dist < 1.5f)
            {
                Level.Current.FoundPrincess();
                // start following hero
                if (!isFollowHero)
                    isFollowHero = true;
            }

            // check win position
            if (Position.X <= 54f)
            {
                Level.Current.WinLevel();
            }
        }

    }
}
