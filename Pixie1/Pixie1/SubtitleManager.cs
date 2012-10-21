using System;
using TTengine.Core;

namespace Pixie1
{
    /**
     * Displays subtitle text, manages conflicts
     */
    public class SubtitleManager: Drawlet
    {
        public SubtitleManager()
        {
        }

        public void ShowNow(string txt, float duration)
        {
            SubtitleText st = new SubtitleText(txt);
            st.Duration = duration;
            Parent.Parent.AddNextUpdate(st);
        }


    }
}
