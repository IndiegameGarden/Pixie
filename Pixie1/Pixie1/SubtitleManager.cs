using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TTengine.Core;

namespace Pixie1
{
    /**
     * Displays subtitle text, manages conflicts
     */
    public class SubtitleManager: Drawlet
    {
        /// <summary>
        /// Helper class to manage the queue of subtitle text items
        /// </summary>
        class QueuedText
        {
            public QueuedText(int priority, SubtitleText text)
            {
                this.Priority = priority;
                this.Text = text;
            }

            public SubtitleText Text;
            public int Priority = 0;
        }

        List<QueuedText> q = new List<QueuedText>();
        QueuedText currentItem = null;

        public SubtitleManager()
        {            
        }

        public SubtitleText Show(int priority, SubtitleText st)
        {
            q.Add(new QueuedText(priority, st));
            st.Active = false;
            Add(st);
            return st;
        }

        public SubtitleText Show(int priority, string txt, float duration, Color color)
        {
            SubtitleText st = new SubtitleText(txt);
            st.DrawInfo.DrawColor = color;
            st.Duration = duration;
            q.Add(new QueuedText(priority,st));
            st.Active = false;
            Add(st);
            return st;
        }

        public SubtitleText Show(int priority, string txt, float duration)
        {
            return Show(priority, txt, duration, Color.WhiteSmoke);
        }

        protected override void OnNewParent()
        {
            base.OnNewParent();
            // make sure we don't inherit the scaling (extreme zooming) from the level itself.
            Motion.MotionParent = Parent.Parent.Motion;
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            // compensate for the level's scaling.            
            //Motion.Scale = 1f / Parent.Motion.Scale;

            if (q.Count > 0)
            {
                // clean up q - items that should be removed
                List<QueuedText> toRemove = new List<QueuedText>();
                foreach (QueuedText t in q)
                {
                    if (t.Text.Delete == true)
                        toRemove.Add(t);
                }
                foreach (QueuedText t in toRemove)
                {
                    q.Remove(t);
                }

                // check which item should be displayed
                // the highest priority one which is earliest in queue
                int highestPri = -32000;
                QueuedText winnerItem = null;
                foreach (QueuedText t in q)
                {
                    // by default, text is not active. Only the winning text will be.
                    t.Text.Active = false; 
                    if (t.Priority > highestPri && t.Text.StartTime <= SimTime )
                    {
                        highestPri = t.Priority;
                        winnerItem = t;
                    }
                }

                // activate only the winning item
                if (winnerItem != null )
                {
                    currentItem = winnerItem;
                    currentItem.Text.Active = true;
                }

            }
        }
    }
}
