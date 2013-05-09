using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTengine.Core;
using Microsoft.Xna.Framework.Media;
using TTMusicEngine;
using TTMusicEngine.Soundevents;

namespace Pixie1
{
    /**
     * plays game sounds
     */
    public class GameSound: Gamelet
    {
        float vol = 1.0f;
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

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            rp.Time = SimTime; // gameTime.ElapsedGameTime.TotalSeconds;
            MusicEngine.GetInstance().Render(soundScript, rp);

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
