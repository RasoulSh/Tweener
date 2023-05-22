using System.Collections;
using TweenerSystem.AbstractTweeners;
using TweenerSystem.Enums;
using UnityEngine;

namespace TweenerSystem.Tweeners
{
    [RequireComponent(typeof(RectTransform))]
    public class UIWidthTweener : UITweener
    {
        [field: SerializeField] public float From { get; set; }
        [field: SerializeField] public float To { get; set; }

        protected override void Animate(float t)
        {
            var newWidth = Mathf.Lerp(From, To, t);
            ThisRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        }
    }
}