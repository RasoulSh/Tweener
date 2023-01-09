using System;
using System.Collections;
using UnityEngine;

namespace TweenerSystem.Common.Utilities
{
    public static class AnimUtilities
    {
        public static IEnumerator AnimationRoutine(float delay,
            float duration, Action<float> tAction, bool realTime = false)
        {
            if (realTime) { yield return new WaitForSecondsRealtime(delay); }
            else { yield return new WaitForSeconds(delay); }
            var startTime = realTime ? Time.unscaledTime : Time.time;
            var t = duration == 0 ? 1f : 0f;
            while (t < 1f)
            {
                t = ((realTime ? Time.unscaledTime : Time.time) - startTime) / duration;
                tAction(t);
                yield return null;
            }
            tAction(1f);
        }
    }
}
