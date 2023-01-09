using TweenerSystem.AbstractTweeners;
using UnityEngine;

namespace TweenerSystem.Tweeners
{
    public class LocalRotationTweener : TransformTweener
    {
        protected override void Animate(float t)
        {
            Transform.localEulerAngles = Vector3.Lerp(From, To, t);
        }
    }
}