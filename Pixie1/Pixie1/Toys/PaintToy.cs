using System;
using Microsoft.Xna.Framework;

namespace Pixie1.Toys
{
    public class PaintToy: Toy
    {
        public PaintToy()
        {
            UsesLeft = 256;
            UsedUponPickup = false;
            UseTimeMax = 8f;
            SetColors(1.9f, Color.LightSkyBlue, Color.MediumVioletRed);
        }

        public override string ToyName()
        {
            return "PIXELS 16x16";
        }

        public override void StartUsing()
        {
            base.StartUsing();
            ShowToyMsg("Pixeeels! " + UsesLeftMsg(), 2f);
            Color paintColor = new Color(RandomMath.RandomUnit(), RandomMath.RandomUnit(), RandomMath.RandomUnit());
            Level.Current.Background.SetPixel(ParentThing.Target, paintColor);
        }

        public override void StopUsing()
        {
            base.StopUsing();
        }

    }
}
