using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Pixie1
{
    public class GameMusic: Gamelet
    {
        SoundEffect track;
        SoundEffectInstance trackInst;
        float vol = 1.0f;

        public GameMusic()
        {
            track = TTengineMaster.ActiveGame.Content.Load<SoundEffect>("A01");    
        }

        public float Volume
        {
            get
            {
                return vol;
            }
            set
            {
                vol = value;
                if (vol < 0f) vol = 0f;
                if (vol > 1f) vol = 1f;

                if (trackInst != null)
                    trackInst.Volume = vol;
            }
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            if (trackInst == null)
            {
                trackInst = track.CreateInstance();
                trackInst.Volume = vol;
                trackInst.Play();
            }
            else
            {
                if (trackInst.State == SoundState.Stopped)
                {
                    int a = 3; //debug
                }
                else if (trackInst.State == SoundState.Playing)
                {
                }
            }


        }

    }
}
