using System;
using TTengine.Core;
using Microsoft.Xna.Framework;

namespace Pixie1.Toys
{
    /// <summary>
    /// lets the display zoom out for a while for strategic planning
    /// </summary>
    public class ZoomOutToy: Toy
    {
        float zoomFactor, zoomSpeed;

        public ZoomOutToy()
        {
            zoomFactor = 0.5f;
            zoomSpeed = 0.001f;
            UseTimeMax = 30f;
            SetColors(2.32f, Color.Azure, Color.LightCoral);
        }

        public override string ToyName()
        {
            return "Potion of Grand Vision";
        }

        public override void StartUsing()
        {
            base.StartUsing();
            Motion m = Level.Current.Motion;
            m.ZoomTarget *= zoomFactor;
            m.ZoomSpeed = zoomSpeed;
            m.ZoomCenterTarget = Level.Current.pixie.Motion;
            Level.Current.Subtitles.Show(1, "(gulk, gulk)", 3f);
        }

        public override void StopUsing()
        {
            base.StopUsing();
            Motion m = Level.Current.Motion;
            m.ZoomTarget /= zoomFactor;
            m.ZoomSpeed = zoomSpeed;
            m.ZoomCenterTarget = Level.Current.pixie.Motion;
            //Level.Current.Subtitles.Show(0, "The Potion", 3f);
        }

    }
}
