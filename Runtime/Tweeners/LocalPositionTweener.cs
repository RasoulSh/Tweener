using TweenerSystem.AbstractTweeners;
using UnityEngine;

namespace TweenerSystem.Tweeners
{
    public class LocalPositionTweener : TransformTweener
    {
        protected override void Animate(float t)
        {
            Transform.localPosition = Vector3.Lerp(From, To, t);
        }
    }
}