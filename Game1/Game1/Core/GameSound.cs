using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

using TTengine.Core;
using TTengine.Comps;
using TTMusicEngine;
using TTMusicEngine.Soundevents;

namespace Game1.Core
{
    /**
     * plays game sounds
     */
    public class GameSound: IScript
    {
        float vol = 1.0f;
        double SimTime = 0;
        SoundEvent soundScript = new SoundEvent();
        RenderParams rp = new RenderParams();
        SoundEvent[] soundsBank = new SoundEvent[20];

        public GameSound()
        {
            MusicEngine.GetInstance();

            soundsBank[0] = new SampleSoundEvent("sword-unsheathe.wav");
            soundsBank[1] = new SampleSoundEvent("swing.wav");
            soundsBank[2] = new SampleSoundEvent("swing2.wav");
            soundsBank[3] = new SampleSoundEvent("swing3.wav");
            soundsBank[4] = new SampleSoundEvent("hit_1.wav");
            soundsBank[5] = new SampleSoundEvent("hit_2.wav");
            soundsBank[6] = new SampleSoundEvent("hit_3.wav");
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

                rp.Ampl = vol;
            }
        }

        public void OnUpdate(ScriptContext p)
        {
            SimTime = p.ScriptComp.SimTime;
            rp.Time = SimTime; 
            MusicEngine.GetInstance().Render(soundScript, rp);

        }

        public void OnDraw(ScriptContext p) 
        {
        }

        protected void Play(int effect, float volume)
        {
            SoundEvent sev = new SoundEvent();
            sev.AddEvent(0f,soundsBank[effect]);
            sev.Amplitude = volume;
            soundScript.AddEvent(SimTime + 0.020f, sev);
        }

        public void PlaySound(int effect, float volumeMin, float volumeMax)
        {
            Play(effect, RandomMath.RandomBetween(volumeMin, volumeMax));
        }

        public void PlayRandomCombatSound(float volumeMin, float volumeMax)
        {
            int n = RandomMath.RandomIntBetween(1,6);
            Play(n, RandomMath.RandomBetween(volumeMin, volumeMax));
        }

    }
}
