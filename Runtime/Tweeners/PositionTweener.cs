using TweenerSystem.AbstractTweeners;
using UnityEngine;

namespace TweenerSystem.Tweeners
{
    public class PositionTweener : TransformTweener
    {
        protected override void Animate(float t)
        {
            Transform.position = Vector3.Lerp(From, To, t);
        }
    }
}