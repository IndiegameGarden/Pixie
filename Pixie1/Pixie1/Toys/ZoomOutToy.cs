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
            return "HI-RES";
        }

        public override void StartUsing()
        {
            base.StartUsing();
            Motion m = Level.Current.Motion;
            m.ZoomTarget *= zoomFactor;
            m.ZoomSpeed = zoomSpeed;
            m.ZoomCenterTarget = Level.Current.pixie.Motion;
            Level.Current.Subtitles.Show(1, "Wow! I can see\nmuch clearer now.", 4f);
        }

        public override void StopUsing()
        {
            base.StopUsing();
            Motion m = Level.Current.Motion;
            m.ZoomTarget /= zoomFactor;
            m.ZoomSpeed = zoomSpeed;
            m.ZoomCenterTarget = Level.Current.pixie.Motion;
            Level.Current.Subtitles.Show(0, "Oh. Back to lo-res.", 3f);
        }

    }
}
