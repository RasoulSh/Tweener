using System.Collections;
using TweenerSystem.Enums;
using UnityEngine;

namespace TweenerSystem.Tweeners
{
    [RequireComponent(typeof(RectTransform))]
    public class UIHeightTweener : SingleTweener
    {
        [field: SerializeField] public float From { get; set; }
        [field: SerializeField] public float To { get; set; }
        private RectTransform _thisRect;
        private RectTransform ThisRect => _thisRect ??= GetComponent<RectTransform>();

        protected override void Animate(float t)
        {
            var newHeight = Mathf.Lerp(From, To, t);
            ThisRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
        }
    }
}