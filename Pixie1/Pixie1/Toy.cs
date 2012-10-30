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

        public bool CanBePickedUp = true;

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
        SubtitleText currentToyMsg = null;

        public Toy()
            : base()
        {          
            // nothing yet.
        }

        public abstract string ToyName();

        /// <summary>
        /// a ParentThing starts using the Toy
        /// </summary>
        public virtual void StartUsing()
        {
            UsesLeft--;
        }

        protected String UsesLeftMsg()
        {
            string s = "";
            if (UsesLeft > 0)
                s = "(" + UsesLeft + " more)";
            return s;
        }

        protected void ShowToyMsg(string text, float duration)
        {
            if (currentToyMsg != null)
                currentToyMsg.Delete = true;
            currentToyMsg = Level.Current.Subtitles.Show(1, text, duration);
        }

        /// <summary>
        /// a ParentThing stops using the Toy (or Toy times out)
        /// </summary>
        public virtual void StopUsing()
        {
        }

        protected override void  OnNewParent()
        {
 	         base.OnNewParent();
             if (Parent is Thing)
             {
                 ParentThing = Parent as Thing;
                 ParentThing.ToyActive = this;
                 if (UsedUponPickup && UsesLeft > 0)
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
            bool pixieFacingToy = CollidesWhenOtherMoves(pixie, pixie.FacingDirection);
            if (toyExplanationMessage != null && toyExplanationMessage.Delete && !pixieFacingToy)
            {
                toyExplanationMessage = null;
            }

            // pixie facing this Toy == print info message
            if (ParentThing == null && toyExplanationMessage==null && pixieFacingToy)
            {
                toyExplanationMessage = Level.Current.Subtitles.Show(5,SayToyName(), 2.1f);
            }

            // collision with pixie = pickup            
            if (ParentThing==null && Collides(pixie))
            {
                if (CanBePickedUp)
                {
                    pixie.Add(this);
                    Visible = false;
                }
                else
                {
                    if (UsesLeft > 0)
                    {
                        useTime = 0f;
                        IsUsed = true;
                        StartUsing();
                    }
                }
            }
        }

        public void SetColors(float cyclePeriod, Color minColor, Color maxColor)
        {
            ColorCycleBehavior cycl = new ColorCycleBehavior(cyclePeriod);
            cycl.minColor = minColor;
            cycl.maxColor = maxColor;
            //cycl.timePeriodR = cyclePeriod * RandomMath.RandomBetween(1.02f, 1.537f); ;
            //cycl.timePeriodG = cyclePeriod * RandomMath.RandomBetween(0.7f, 0.93f); ;
            Add(cycl);        
        }

        protected virtual string SayToyName()
        {
            string tname = ToyName();
            if (tname.Length > 0)
                return "It says:" + (tname.Length > 16 ? "\n" : " ") + "\"" + tname + "\"";
            else
                return "";
        }

    }
}
