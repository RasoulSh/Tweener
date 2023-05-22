using UnityEngine;

namespace TweenerSystem.AbstractTweeners
{
    public abstract class UITweener : SingleTweener
    {
        private RectTransform thisRect;
        protected RectTransform ThisRect => thisRect ??= GetComponent<RectTransform>();
    }
}