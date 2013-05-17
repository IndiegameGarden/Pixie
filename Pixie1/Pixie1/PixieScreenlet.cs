using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using TTengine.Util;

namespace Pixie1
{
    public class PixieScreenlet: Screenlet
    {
        public DebugMessage CpuLoadInfo;

        long maxTime = 0;

        public PixieScreenlet(int x, int y)
            : base(x, y)
        {
        }

        protected override void OnNewParent()
        {
            base.OnNewParent();
            CpuLoadInfo = new DebugMessage();
            Add(CpuLoadInfo);
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
            long tm = (PixieGame.Instance.TreeRoot as FixedTimestepPhysics).LastUpdateDurationMs;
            if (tm > maxTime)
                maxTime = tm;
            CpuLoadInfo.Text = "CPU ms: " + maxTime;
        }
    }
}
