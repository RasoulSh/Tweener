using System.Collections;
using TweenerSystem.AbstractTweeners;
using TweenerSystem.Enums;
using UnityEngine;

namespace TweenerSystem.Tweeners
{
    [RequireComponent(typeof(RectTransform))]
    public class UIHeightTweener : UITweener
    {
        [field: SerializeField] public float From { get; set; }
        [field: SerializeField] public float To { get; set; }

        protected override void Animate(float t)
        {
            var newHeight = Mathf.Lerp(From, To, t);
            ThisRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
        }
    }
}