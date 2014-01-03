using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using TTengine.Comps;
using TTMusicEngine;

using Artemis;
using Artemis.Manager;
using Artemis.Attributes;
using Artemis.System;

namespace TTengine.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = SystemsSchedule.AudioSystem)]
    public class AudioSystem : EntityComponentProcessingSystem<AudioComp>
    {
        RenderParams rp = new RenderParams();
        MusicEngine audioEngine = null;

        protected override bool CheckProcessing()
        {
            // disable this system if the MusicEngine is not enabled.
            if (!TTGame.Instance.IsAudio)
                IsEnabled = false;
            
            return base.CheckProcessing();
        }

        protected override void Begin()
        {
            audioEngine = TTGame.Instance.AudioEngine;     
            audioEngine.Update(); // to be called once every frame
        }

        public override void Process(Entity entity, AudioComp ac)
        {
            ac.OnUpdate(this.Dt);
            rp.Time = ac.SimTime;
            rp.Ampl = ac.Ampl;
            audioEngine.Render(ac.AudioScript, rp);
        }

    }

}
