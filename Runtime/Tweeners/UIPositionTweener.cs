using TweenerSystem.AbstractTweeners;
using UnityEngine;

namespace TweenerSystem.Tweeners
{
    public class UIPositionTweener  : UITweener
    {
        [field: SerializeField] public Vector2 From { get; set; } 
        [field: SerializeField] public Vector2 To { get; set; }
        protected override void Animate(float t)
        {
            var newPos = Vector2.Lerp(From, To, t);
            ThisRect.anchoredPosition = newPos;
        }
    }
}