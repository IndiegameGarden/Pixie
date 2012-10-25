using System;
using Microsoft.Xna.Framework;

namespace Pixie1.Toys
{
    /**
     * a Toy that simply shows a message when picked up 
     */
    public class ShowMessageToy: Toy
    {
        public int Priority;
        public SubtitleText Message = new SubtitleText();

        public ShowMessageToy(int priority, string msg, float duration)
        {
            this.Priority = priority;
            Message = new SubtitleText(msg);
            Message.Duration = duration;
            DrawInfo.DrawColor = Color.Transparent;
        }

        public ShowMessageToy(int priority, SubtitleText msg)
        {
            this.Priority = priority;
            Message = msg;
            DrawInfo.DrawColor = Color.Transparent;
        }

        public override string ToyName()
        {
            return "";
        }

        protected override void StartUsing()
        {
            base.StartUsing();
            Level.Current.Subtitles.Show(Priority, Message);
        }
    }
}
