using TweenerSystem.AbstractTweeners;
using UnityEngine;

namespace TweenerSystem.Tweeners
{
    public class RotationTweener : TransformTweener
    {
        protected override void Animate(float t)
        {
            Transform.eulerAngles = Vector3.Lerp(From, To, t);
        }
    }
}