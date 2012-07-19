using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;

namespace Pixie1
{
    public class Toy: Thing
    {
        /// <summary>
        /// if my Parent is a Thing, this holds it. If null, there is no Parent which is a Thing
        /// </summary>
        public Thing ParentThing = null;

        /// <summary>
        /// if true this Toy is immediately used (once) by ParentThing upon pickup
        /// </summary>
        public bool UsedUponPickup = true;

        /// <summary>
        /// whether the Toy is being used right now (true) or not (false)
        /// </summary>
        public bool IsUsed = false;

        /// <summary>
        /// time this Toy can be used at most (remains active) in a single usage
        /// </summary>
        public float UseTimeMax = 5f;

        /// <summary>
        /// how many times using this Toy has still left (e.g. 'shots')
        /// </summary>
        public int UsesLeft = 1;

        float cyclePeriod;
        float useTime;

        public Toy()
            : base("pixie")
        {            
            cyclePeriod = RandomMath.RandomBetween(0.2f, 1.82f);
            ColorCycleBehavior cycl = new ColorCycleBehavior(cyclePeriod);
            cycl.minColor = new Color(RandomMath.RandomBetween(0.3f, 0.5734f), RandomMath.RandomBetween(0.7f, 1f), RandomMath.RandomBetween(0.1f, 0.54f));
            cycl.maxColor = new Color(0.9434f, 0.9f, RandomMath.RandomBetween(0.8f,1.0f) );
            cycl.timePeriodR = cyclePeriod * RandomMath.RandomBetween(1.02f, 1.537f); ;
            cycl.timePeriodG = cyclePeriod * RandomMath.RandomBetween(0.7f, 0.93f); ;
            Add(cycl);        
        }

        /// <summary>
        /// a ParentThing starts using the Toy
        /// </summary>
        protected virtual void StartUsing()
        {
            UsesLeft--;
        }

        /// <summary>
        /// a ParentThing stops using the Toy (or Toy times out)
        /// </summary>
        protected virtual void StopUsing()
        {
        }

        protected override void  OnNewParent()
        {
 	         base.OnNewParent();
             if (Parent is Thing)
             {
                 ParentThing = Parent as Thing;                 
                 if (UsedUponPickup)
                 {
                     useTime = 0f;
                     IsUsed = true;
                     StartUsing();
                 }
             }
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            if(IsUsed)
                useTime += p.Dt;    
            if (useTime > UseTimeMax)
            {
                IsUsed = false;
                StopUsing();
                useTime = 0f;                
            }

            // collision = pickup
            Thing pixie = Level.Current.pixie;
            if (ParentThing==null && Collides(pixie))
            {
                pixie.Add(this);
                Visible = false;
            }
        }
    }
}
