using System;
using Microsoft.Xna.Framework;

namespace Pixie1.Toys
{
    public class PaintToy: Toy
    {
        LevelBackground bg;

        public PaintToy()
        {
            UsesLeft = 256;
            UsedUponPickup = false;
            UseTimeMax = 8f;
            SetColors(1.9f, Color.LightSkyBlue, Color.MediumVioletRed);
            bg = Level.Current.Background;
        }

        public override string ToyName()
        {
            return "PIXEL PAINT\n(Covers 16x16)";
        }

        public override void StartUsing()
        {
            base.StartUsing();
            ShowToyMsg("Pixeeels! " + UsesLeftMsg(), 2f);
        }

        public override void StopUsing()
        {
            base.StopUsing();
        }

        protected override void OnUpdate(ref TTengine.Core.UpdateParams p)
        {
            base.OnUpdate(ref p);

            // check for trigger key to see whether still used
            if (!IsTriggered)
            {
                StopUsing();
            }

            // if used, spray pixel paint
            if (IsUsed)
            {
                Vector2 usePos = ParentThing.Position;
                Color bgCol = bg.SamplePixel(usePos);

                if (bgCol.Equals(bg.ForegroundColor))
                {
                    Color paintColor = new Color(RandomMath.RandomUnit(), RandomMath.RandomUnit(), RandomMath.RandomUnit());
                    bg.SetPixel(usePos, paintColor);
                    UsesLeft--;
                }
            }
        }

    }
}
