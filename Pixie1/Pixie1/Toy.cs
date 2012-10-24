using System;

using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Pixie1.Behaviors;

namespace Pixie1
{
    /// <summary>
    /// base class for a Toy that can be picked up, left behind, used (and then it is active for a while), sometimes with 
    /// multiple shots before it is expended. Typically some special power, effect or weapon.
    /// </summary>
    public abstract class Toy: Thing
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

        float useTime;
        SubtitleText toyExplanationMessage = null;

        public Toy()
            : base("pixie")
        {            
        }

        public abstract string ToyName();

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

            // check if the last message, if any, about this toy is already done and new instance can be started
            Thing pixie = Level.Current.pixie;
            bool pixieFacingToy = Collides(pixie, pixie.FacingDirection);
            if (toyExplanationMessage != null && toyExplanationMessage.Delete && !pixieFacingToy)
            {
                toyExplanationMessage = null;
            }

            // pixie facing this Toy == print info message
            if (ParentThing == null && toyExplanationMessage==null && pixieFacingToy)
            {
                toyExplanationMessage = Level.Current.Subtitles.Show(5,SayToyName(), 3f);
            }

            // collision with pixie = pickup            
            if (ParentThing==null && Collides(pixie))
            {
                pixie.Add(this);
                Visible = false;
            }
        }

        protected void SetColors(float cyclePeriod, Color minColor, Color maxColor)
        {
            ColorCycleBehavior cycl = new ColorCycleBehavior(cyclePeriod);
            cycl.minColor = minColor;
            cycl.maxColor = maxColor;
            //cycl.timePeriodR = cyclePeriod * RandomMath.RandomBetween(1.02f, 1.537f); ;
            //cycl.timePeriodG = cyclePeriod * RandomMath.RandomBetween(0.7f, 0.93f); ;
            Add(cycl);        
        }

        protected string SayToyName()
        {
            string tname = ToyName();
            return "It says:"+ (tname.Length > 16? "\n":" ") +"\"" + tname + "\"";
        }

    }
}
