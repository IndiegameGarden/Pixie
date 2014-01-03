﻿
using System;
using Microsoft.Xna.Framework;

using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Game1.Comps;

namespace Game1.Systems
{
    /// <summary>Scrolling of Screen.ZoomCenter system.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.ScrollingSystem)]
    public class ScrollingSystem : EntityComponentProcessingSystem<ScrollingComp>
    {
        public override void Process(Entity entity, ScrollingComp sc)
        {
            sc.Scrolling.IsActive = true; // FIXME debug
            sc.Scrolling.OnUpdate(Dt);
            sc.ScreenToScroll.ZoomCenter = sc.Scrolling.Current;
            sc.Scrolling.Target = sc.PositionToTrack.Position;
        }
    }

}
