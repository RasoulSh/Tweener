using TweenerSystem.AbstractTweeners;
using UnityEngine;

namespace TweenerSystem.Tweeners
{
    public class LocalScaleTweener : TransformTweener
    {
        protected override void Animate(float t)
        {
            Transform.localScale = Vector3.Lerp(From, To, t);
        }
    }
}